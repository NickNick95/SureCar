using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SureCar.Entities;
using SureCar.Services.Interface;
using SureCar.Services.Models;
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
            await _roleManager.CreateAsync(new IdentityRole(UserRole.User));
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(UserRole.Administrator));

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
                        await _userManager.AddToRoleAsync(user, UserRole.Administrator);
                }
                else
                {
                    _logger.LogInformation("Cannot create default user");
                    LogError(identityResult.Errors.ToList());
                }
            }
        }

        public async Task<string> LoginAsync(ApplicationUser user, string password)
        {
            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var toke = await GetToken(user);

                var identityResult = await _userManager.SetAuthenticationTokenAsync(user, _loginProvider, _tokenName, toke);

                if (identityResult.Succeeded)
                    return toke;
                else
                {
                    _logger.LogInformation("Cannot login user");
                    LogError(identityResult.Errors.ToList());
                }
            }

            return string.Empty;
        }

        public async Task LogoutAsync(ApplicationUser user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, _loginProvider, _tokenName);
        }

        public async Task<ApplicationUser?> GetUserByNameAsync(string userName)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            return user == null ? null : user;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(id);

            return user == null ? null : user;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var applicationUser = _mapper.Map<ApplicationUser>(user);
           // applicationUser.Id = Guid.NewGuid().ToString();
            var identityResult = await _userManager.CreateAsync(applicationUser, user.Password);
            if (identityResult.Succeeded)
            {
                ApplicationUser userExist = await _userManager.FindByNameAsync(user.UserName);
                await _userManager.AddToRoleAsync(userExist, UserRole.User);

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
            //List<Claim> claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.Role, "Admin")
            //};

            //var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            //    _configuration.GetSection("AppSettings:Token").Value));

            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //var token = new JwtSecurityToken(
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(1),
            //    signingCredentials: creds);

            // var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            var utcNow = DateTime.UtcNow;
            //var role = _userManager.getR

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