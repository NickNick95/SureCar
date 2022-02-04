using AutoMapper;
using SureCar.DataManager;
using SureCar.Repositories;
using SureCar.API.Infrastructure;
using SureCar.Common.Interface.DataManager;

namespace SureCar.API
{
    public class Registry
    {
        public static void BuildServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DataContext>(_ =>
             new DataContext(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton(new MapperConfiguration(mc => mc.AddProfile(new AutoMapperProfile())).CreateMapper());

            services.AddSingleton<IJsonDataManager, JsonDataManager>();
        }
    }
}
