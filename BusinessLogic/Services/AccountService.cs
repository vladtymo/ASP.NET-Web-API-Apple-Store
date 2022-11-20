using Core.DTOs;
using Core.Exceptions;
using Core.Helpers;
using Core.Interfaces;
using Core.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountService(UserManager<IdentityUser> userManager,
                              SignInManager<IdentityUser> signInManager,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task Register(RegisterDTO data)
        {
            var user = new IdentityUser()
            {
                Email = data.Email,
                UserName = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            var result = await userManager.CreateAsync(user, data.Password);

            if (!result.Succeeded)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new HttpException(errors, HttpStatusCode.BadRequest);
            }
        }
        public async Task<LoginResponseDTO> Login(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null || !await userManager.CheckPasswordAsync(user, password))
            {
                throw new HttpException(ErrorMessages.InvalidCredentials, HttpStatusCode.BadRequest);
            }

            await signInManager.SignInAsync(user, true);

            return new LoginResponseDTO()
            {
                Email = email,
                Token = await GenerateTokenAsync(user)
            };
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        private async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            // create claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            };

            //var roles = await userManager.GetRolesAsync(user);
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = jwtOptions.Issuer,
                Expires = DateTime.UtcNow.AddHours(jwtOptions.Lifetime), // TODO: not working - fix
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
