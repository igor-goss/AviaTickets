﻿using Identity.Business.DTOs;
using Identity.Data.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Identity.Business.Services.Interfaces
{
    public interface IIdentityService
    {
        public Task<IdentityResult> RegisterNewUserAsync(SignUpDTO signUpDTO);
        public Task<SignInResult> LoginAsync(LoginDTO loginDTO);
        public Task LogoutAsync();
        public Task<UserDTO> GetCurrentUserProfileAsync(ClaimsPrincipal claimsPrincipal);
        public Task<UserDTO> EditCurrentUserProfileAsync(ClaimsPrincipal claimsPrincipal, UserDTO updatedUser);
        public Task<IdentityResult> ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, PasswordChangeDTO passwordChangeDTO);
    }
}
