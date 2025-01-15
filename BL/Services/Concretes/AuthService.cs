using AutoMapper;
using BL.DTOs.AuthDTO;
using BL.Exceptions;
using BL.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Concretes
{
    public class AuthService : IAuthService
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;
        readonly IMapper _mapper;
        public AuthService(IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task RegisterAsync(RegisterDTO user)
        {
            if (user.Password != user.RepeatPassword)
            {
                throw new OperationNotValidException("Passwords does not match.");
            }
            IdentityUser userIdentity = _mapper.Map<IdentityUser>(user);
            await _userManager.CreateAsync(userIdentity, user.Password);
            await _userManager.AddToRoleAsync(userIdentity,"User");
        }
        public async Task LoginAsync(LoginDTO user)
        {
            IdentityUser? userIdentity = await _userManager.FindByNameAsync(user.Username);

            if (userIdentity == null)
            {
                throw new OperationNotValidException("User not found.");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(userIdentity,user.Password,true);
            
            if (!result.Succeeded)
            {
                throw new OperationNotValidException("Login credentials are not correct.");
            }

            await _signInManager.SignInAsync(userIdentity,true);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
