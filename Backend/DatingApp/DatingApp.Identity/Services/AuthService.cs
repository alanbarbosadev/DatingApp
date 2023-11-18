using DatingApp.Application.Contracts.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Application.Models.Identity;
using DatingApp.Identity.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace DatingApp.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.Email);

            if (user == null)
            {
                throw new Exception("User not Found!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, authRequest.Password, false);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid Credentials!");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            var response = new AuthResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                UserName = registrationRequest.UserName,
                Email = registrationRequest.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Visitor");

                JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

                return new RegistrationResponse()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)

                };
            }
            else
            {
                throw new Exception("Something happened!");              
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var userRoles = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString())
            }.Union(userClaims).Union(userRoles);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }
    }
}
