using AutoMapper;
using entitieModels = SureCar.Entities;
using jsonModels = SureCar.DataManagers.Models;
using serviceModels = SureCar.Services.Models;

using serviceModel = SureCar.Services.Models;
using SureCar.API.Models.User;
using SureCar.Repositories;

namespace SureCar.API.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<serviceModels.Warehouse, entitieModels.Warehouse>()
                .ForMember(x => x.Name, x => x.MapFrom(z => z.Name))
                .ForMember(x => x.Car, x => x.MapFrom(z => z.Car))
                .ForMember(x => x.Location, x => x.MapFrom(z => z.Location))
                .ReverseMap();
            CreateMap<serviceModels.Car, entitieModels.Car>()
                .ForMember(x => x.Vehicles, x => x.MapFrom(z => z.Vehicles))
                .ReverseMap();
            CreateMap<serviceModels.Location, entitieModels.Location>().ReverseMap();
            CreateMap<serviceModels.Vehicle, entitieModels.Vehicle>().ReverseMap();

            CreateMap<jsonModels.Warehouse, serviceModels.Warehouse>()
                .ForMember(x => x.Name, x => x.MapFrom(z => z.Name))
                .ForMember(x => x.Car, x => x.MapFrom(z => z.Car))
                .ForMember(x => x.Location, x => x.MapFrom(z => z.Location))
                .ReverseMap();

             CreateMap<jsonModels.Car, serviceModels.Car>()
                .ForMember(x => x.Vehicles, x => x.MapFrom(z => z.Vehicles))
                .ReverseMap();
            CreateMap<jsonModels.Location, serviceModels.Location>().ReverseMap();
            CreateMap<jsonModels.Vehicle, serviceModels.Vehicle>().ReverseMap();

            CreateMap<serviceModel.UserLogin, UserLogin>().ReverseMap();

            CreateMap<ApplicationUser, serviceModel.User>()
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.UserName))
                .ForMember(x => x.Email, x => x.MapFrom(z => z.Email))
                .ForMember(x => x.Id, x => x.MapFrom(z => z.Id))
                .ReverseMap();
        }
    }
}
