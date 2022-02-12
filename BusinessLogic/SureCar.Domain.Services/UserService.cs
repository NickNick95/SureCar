﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SureCar.Entities;
using SureCar.Services.Interface;
using SureCar.Services.Models.UserModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SureCar.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        private const string _loginProvider = "userProvider";
        private const string _tokenName = "jwtToken";

        public UserService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ILogger<UserService> logger,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task InitializeBaseAdminAsync()
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator));

            if (result.Succeeded)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Admin",
                    NormalizedUserName = "Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                };

                var identityResult = await _userManager.CreateAsync(user, "Qwerty!23456");
                if (identityResult.Succeeded)
                {
                    ApplicationUser userExist = await _userManager.FindByNameAsync(user.UserName);
                    if (userExist != null)
                        await _userManager.AddToRoleAsync(user, UserRoles.Administrator);
                }
                else
                {
                    _logger.LogInformation("Cannot create default user");
                    LogError(identityResult.Errors.ToList());
                }
            }
        }

        public async Task<UserLoginResult?> LoginAsync(UserLogin user)
        {
            var applicationUser = await _userManager.FindByNameAsync(user.UserName);

            var login = await _userManager.CheckPasswordAsync(applicationUser, user.Password);

            if (login)
            {
                var token = await GetToken(applicationUser);

                var identityResult = await _userManager
                    .SetAuthenticationTokenAsync(applicationUser, _loginProvider, _tokenName, token);

                if (identityResult.Succeeded)
                {
                    var result = new UserLoginResult();
                    result.User = _mapper.Map<User>(applicationUser);
                    result.Token = token;

                    return result;
                }
                else
                {
                    _logger.LogInformation("Cannot login user");
                    LogError(identityResult.Errors.ToList());
                }
            }

            return null;
        }

        public async Task LogoutAsync(User user)
        {
            var applicationUser = await _userManager.FindByNameAsync(user.UserName);

            await _userManager.RemoveAuthenticationTokenAsync(applicationUser, _loginProvider, _tokenName);
        }

        public async Task<User?> GetUserByNameAsync(string userName)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return null;
            else 
                return _mapper.Map<User>(user);
        }

        public async Task<bool> IsUserAdmin(User user)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            var isAdmin = await _userManager.IsInRoleAsync(applicationUser, UserRoles.Administrator);

            return isAdmin;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;
            else
                return _mapper.Map<User>(user);
        }

        public async Task<List<string>> GetRoleByUser(User user)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            var roles = await _userManager.GetRolesAsync(applicationUser) as List<string>;

            return roles;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
            var identityResult = await _userManager.CreateAsync(applicationUser, user.Password);
            if (identityResult.Succeeded)
            {
                ApplicationUser userExist = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(userExist, UserRoles.User);

                return true;
            }
            else
            {
                _logger.LogInformation("Cannot create user");
                LogError(identityResult.Errors.ToList());

                return false;
            }
        }

        private async Task<String> GetToken(ApplicationUser user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new List<Claim>()
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                if (!string.IsNullOrEmpty(role))
                    claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<String>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: DateTime.Now.AddDays(1),
                audience: _configuration.GetValue<String>("Tokens:Audience"),
                issuer: _configuration.GetValue<String>("Tokens:Issuer")
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        private void LogError(List<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                _logger.LogInformation($"Code: {error.Code}, Description: {error.Description}");
            }
        }

    }
}