using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SureCar.Common.Interface.DataManager;
using SureCar.DataManagers;
using SureCar.Entities;
using SureCar.Repositories;
using SureCar.Repositories.Implementions;
using SureCar.Repositories.Interfaces;
using SureCar.Services;
using SureCar.Services.Helpers;
using SureCar.Services.Interface;
using SureCar.Services.Interface.Helpers;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace SureCar.API.Infrastructure
{
    public class Registry
    {
        public static void BuildServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddLogging();
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddAuthentication().AddIdentityServerJwt();

            services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>();

            services.AddSingleton(new MapperConfiguration(mc => mc.AddProfile(new AutoMapperProfile())).CreateMapper());

            #region Transients
           
            services.AddTransient<DbContext, DataContext>();
            services.AddTransient<ICryptoHelper, CryptoHelper>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IJsonDataManager, JsonDataManager>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<IWarehouseService, WherehouseService>();
            services.AddTransient<IWarehouseRepository, WarehouseRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            #endregion

            #region Add Authentication

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                             .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
                         ValidateIssuer = false,
                         ValidateAudience = false
                     };
                 });
            #endregion
            }
    }
}
