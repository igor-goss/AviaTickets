using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Data.DTOs
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int DateOfBirth { get; set; }

        public string? CardNo { get; set; } = string.Empty;
        public string? ExpirationDate { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
    }
}
