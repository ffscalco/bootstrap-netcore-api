using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.Helpers;
using Api.ViewModels;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Api.Validators;
using Services.Helpers;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Api.Handlers.Authentication
{
    public class LoginHandler : IRequestHandler<AuthenticateVM, LoggedUserVM>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginHandler(UserManager<User> userManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<LoggedUserVM> Handle(AuthenticateVM model, CancellationToken cancellationToken)
        {
            var validator = new AuthenticateValidator();
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
                throw new ValidationException(JsonConvert.SerializeObject(validationResult.Errors));

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                LoggedUserVM userResource = _mapper.Map<User, LoggedUserVM>(user);

                userResource.Token = new JwtSecurityTokenHandler().WriteToken(token);
                userResource.Expiration = token.ValidTo;

                return userResource;
            }

            throw new AppException("Username or password is incorrect");
        }
    }
}
