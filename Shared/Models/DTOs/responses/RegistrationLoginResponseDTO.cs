using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class RegistrationLoginReponseDTO
    {
        public string Message { get; set; } = string.Empty;
        public string StatusCode { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string profileImageUrl { get; set; } = string.Empty;
        public int userId { get; set; } = 0;
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;

    }
}
