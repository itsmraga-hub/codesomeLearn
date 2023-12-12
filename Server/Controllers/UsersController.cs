using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using codesome.Server.Data;
using codesome.Shared.Models;
using codesome.Shared.Models.DTOs.requests;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using codesome.Server.Services;
using codesome.Shared.Models.DTOs.responses;
using Newtonsoft.Json;

namespace codesome.Server.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly codesomeServerContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTGenerator _jWTGenerator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(codesomeServerContext context, IMapper mapper, ILogger<UsersController> logger, IJWTGenerator jWTGenerator)
        {
            _context = context;
            _mapper = mapper;
            _jWTGenerator = jWTGenerator;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet("api/Users")]
        public async Task<ActionResult<IEnumerable<CustomUser>>> GetUser()
        {
          if (_context.CustomUser == null)
          {
              return NotFound();
          }
            return await _context.CustomUser.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("api/Users/{id}")]
        public async Task<ActionResult<CustomUser>> GetUser(int id)
        {
          if (_context.CustomUser == null)
          {
              return NotFound();
          }
            var user = await _context.CustomUser.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("api/Users/{id}")]
        public async Task<IActionResult> PutUser(int id, UserRequestDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("api/Users")]
        public async Task<ActionResult<CustomUser>> PostUser(UserRequestDTO user)
        {
          if (_context.CustomUser == null)
          {
              return Problem("Entity set 'codesomeServerContext.CustomUser'  is null.");
          }
            CustomUser newUser = new();
            _context.CustomUser.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("api/Users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.CustomUser == null)
            {
                return NotFound();
            }
            var user = await _context.CustomUser.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.CustomUser.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("api/v1/users/login")]
        public async Task<ActionResult> LoginUser([FromBody] LoginDTO logindto)
        {
            try
            {
                // CHECK IF USER EXISTS
                var userexist = await _context.CustomUser.FirstOrDefaultAsync(_ => _.PhoneNumber == logindto.UserName);
                // var userexist = await _context.CustomUser.FirstOrDefaultAsync(_ => _.PhoneNumber == logindto.UserName || _.Email == logindto.UserName);
                // var userexist2 = await _context.CustomUser.FirstOrDefaultAsync(i => i.Email == logindto.UserName);
                if (userexist == null )
                {
                    return Ok(new RegistrationLoginReponseDTO()
                    {
                        Message = "CustomUser not found",
                        StatusCode = "1",
                        Token = "none",
                        UserType = "none"
                    });
                }
                else
                {
                    try
                    {
                        //VERIFY PASSWORD
                        bool verified = BCrypt.Net.BCrypt.Verify(logindto.Password, userexist.Password);
                        

                        if (verified)
                        {
                            if (userexist.Status == false)
                            {
                                return Ok(new RegistrationLoginReponseDTO()
                                {
                                    Message = "Your account has been locked, contact administrator",
                                    StatusCode = "1",
                                    Token = "none",
                                    UserType = "none"
                                });
                            }
                            else
                            {
                                return Ok(new RegistrationLoginReponseDTO()
                                {
                                    Message = "Login success",
                                    StatusCode = "0",
                                    Token = _jWTGenerator.GetToken(userexist),
                                    // UserType = userexist.UserRoles[0].RoleName,
                                    UserType = "",
                                    PhoneNumber = userexist.PhoneNumber,
                                    profileImageUrl = userexist.ProfileImage,
                                    userId = userexist.Id,
                                    username = userexist.FirstName + "." + userexist.LastName,
                                    email = userexist.Email
                                });
                            }
                        }
                        else
                        {
                            return Ok(new RegistrationLoginReponseDTO()
                            {
                                Message = "Incorrect password",
                                StatusCode = "1",
                                Token = "none",
                                UserType = "none"
                            });
                        }
                    }
                    catch
                    {
                        return Ok(new RegistrationLoginReponseDTO()
                        {
                            Message = "Failed to verify password",
                            StatusCode = "1",
                            Token = "none",
                            UserType = "none"
                        });
                    }
                }
            }
            catch
            {
                return Ok(new RegistrationLoginReponseDTO()
                {
                    Message = "Failed to fetch user",
                    StatusCode = "1",
                    Token = "none",
                    UserType = "none"
                });
            }
        }


        [AllowAnonymous]
        [HttpPost("api/v1/users/register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterWebUserDTO user)
        {
            if (ModelState.IsValid)
            {
                // CHECK IF USER EXISTS
                Expression<Func<CustomUser, bool>> predicate = a => a.PhoneNumber == user.Phone;
                var userexist = await _context.CustomUser.FirstOrDefaultAsync(i => i.PhoneNumber == user.Phone);
                if (userexist != null)
                {
                    return Ok(new RegistrationLoginReponseDTO()
                    {
                        Message = "Phone number already registered",
                        StatusCode = "1",
                        Token = "none"
                    });
                }

                CustomUser userdto = new CustomUser();
                userdto.FirstName = user.FirstName;
                userdto.LastName = user.LastName;
                userdto.UserName = $"{user.FirstName}.{user.LastName}";
                userdto.Email = user.Email;
                userdto.PhoneNumber = user.Phone;
                userdto.CreatedAt = DateTime.Now;
                userdto.LastUpdatedAt = DateTime.Now;
                userdto.RegistrationDate = DateTime.Now;
                // userdto.UserRoles.Add();
                userdto.Status = true;
                userdto.ProfileImage = "";
                // userdto.PrivacyPolicyStatus = true;
                // userdto.Otp = UtilitiesHelper.GenerateNewCode(4);

                if (user.Password != null && user.ConfirmPassword != null)
                {
                    if (user.Password == user.ConfirmPassword)
                    {
                        string xhashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                        userdto.Password = xhashedPassword;

                        _context.CustomUser.Add(userdto);
                        await _context.SaveChangesAsync();

                        return Ok(new RegistrationLoginReponseDTO()
                        {
                            Message = "Registration Success",
                            StatusCode = "0",
                            Token = _jWTGenerator.GetToken(userdto)
                        });
                    }


                }

                return Ok(new RegistrationLoginReponseDTO()
                {
                    Message = "Registration Failed",
                    StatusCode = "0",
                    // Token = _jWTGenerator.GetToken(userdto)
                    Token = "none"
                }); ;

            }
            else
            {
                return Ok(new RegistrationLoginReponseDTO()
                {
                    Message = "Invalid Payload",
                    StatusCode = "1",
                    Token = "none"
                });
            }
        }



        private bool UserExists(int id)
        {
            return (_context.CustomUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
