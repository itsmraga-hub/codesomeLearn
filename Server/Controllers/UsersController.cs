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

namespace codesome.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly codesomeServerContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTGenerator _jWTGenerator;

        public UsersController(codesomeServerContext context, IMapper mapper, IJWTGenerator jWTGenerator)
        {
            _context = context;
            _mapper = mapper;
            _jWTGenerator = jWTGenerator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRequestDTO user)
        {
          if (_context.User == null)
          {
              return Problem("Entity set 'codesomeServerContext.User'  is null.");
          }
            User newUser = new();
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
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
                var userexist = await _context.User.FirstOrDefaultAsync(_ => _.PhoneNumber == logindto.UserName || _.Email == logindto.UserName);
                // var userexist2 = await _context.User.FirstOrDefaultAsync(i => i.Email == logindto.UserName);
                if (userexist == null )
                {
                    return Ok(new RegistrationLoginReponseDTO()
                    {
                        Message = "User not found",
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
                                    UserType = userexist.UserRoles[0].RoleName,
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
                Expression<Func<User, bool>> predicate = a => a.PhoneNumber == user.Phone;
                var userexist = await _context.User.FirstOrDefaultAsync(i => i.PhoneNumber == user.Phone);
                if (userexist != null)
                {
                    return Ok(new RegistrationLoginReponseDTO()
                    {
                        Message = "Phone number already registered",
                        StatusCode = "1",
                        Token = "none"
                    });
                }

                User userdto = new User();
                userdto.FirstName = user.FirstName;
                userdto.LastName = user.LastName;
                userdto.Email = user.Email;
                userdto.PhoneNumber = user.Phone;
                userdto.CreatedAt = DateTime.Now;
                userdto.LastUpdatedAt = DateTime.Now;
                // userdto.UserRoles.Add();
                userdto.Status = true;
                userdto.ProfileImage = "";
                // userdto.PrivacyPolicyStatus = true;
                // userdto.Otp = UtilitiesHelper.GenerateNewCode(4);

                if (user.Password != null)
                {
                    string xhashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    userdto.Password = xhashedPassword;
                }

                _context.User.Add(userdto);
                await _context.SaveChangesAsync();
                return Ok(new RegistrationLoginReponseDTO()
                {
                    Message = "Registration Success",
                    StatusCode = "0",
                    Token = _jWTGenerator.GetToken(userdto)
                });
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
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
