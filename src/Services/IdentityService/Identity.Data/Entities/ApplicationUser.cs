﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Identity.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public int DateOfBirth { get; set; }

        public string? CardNo { get; set; } = string.Empty;
        public string? ExpirationDate { get; set; } = string.Empty;

    }
}
