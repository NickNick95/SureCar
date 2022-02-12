using AutoMapper;
using SureCar.API.Models.User;
using SureCar.Entities;
using entitieModels = SureCar.Entities;
using jsonModels = SureCar.DataManagers.Models;
using serviceModel = SureCar.Services.Models;
using apiModel = SureCar.API.Models;

namespace SureCar.API.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<serviceModel.Warehouse, entitieModels.Warehouse>()
                .ForMember(x => x.Name, x => x.MapFrom(z => z.Name))
                .ForMember(x => x.Car, x => x.MapFrom(z => z.Car))
                .ForMember(x => x.Location, x => x.MapFrom(z => z.Location))
                .ReverseMap();
            CreateMap<serviceModel.Car, entitieModels.Car>()
                .ForMember(x => x.Vehicles, x => x.MapFrom(z => z.Vehicles))
                .ReverseMap();
            CreateMap<serviceModel.Location, entitieModels.Location>().ReverseMap();
            CreateMap<serviceModel.Vehicle, entitieModels.Vehicle>().ReverseMap();

            CreateMap<jsonModels.Warehouse, serviceModel.Warehouse>()
                .ForMember(x => x.Name, x => x.MapFrom(z => z.Name))
                .ForMember(x => x.Car, x => x.MapFrom(z => z.Car))
                .ForMember(x => x.Location, x => x.MapFrom(z => z.Location))
                .ReverseMap();

             CreateMap<jsonModels.Car, serviceModel.Car>()
                .ForMember(x => x.Vehicles, x => x.MapFrom(z => z.Vehicles))
                .ReverseMap();
            CreateMap<jsonModels.Location, serviceModel.Location>().ReverseMap();
            CreateMap<jsonModels.Vehicle, serviceModel.Vehicle>().ReverseMap();

            CreateMap<serviceModel.UserLogin, UserLogin>().ReverseMap();

            CreateMap<ApplicationUser, serviceModel.User>()
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.UserName))
                .ForMember(x => x.Email, x => x.MapFrom(z => z.Email))
                .ForMember(x => x.Id, x => x.MapFrom(z => z.Id))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(z => z.PhoneNumber))
                .ForMember(x => x.FirstName, x => x.MapFrom(z => z.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(z => z.LastName))
                .ReverseMap();

            CreateMap<UserForRegistration, serviceModel.User>()
                .ForMember(x => x.UserName, x => x.MapFrom(z => z.UserName))
                .ForMember(x => x.Email, x => x.MapFrom(z => z.Email))
                .ForMember(x => x.PhoneNumber, x => x.MapFrom(z => z.PhoneNumber))
                .ForMember(x => x.FirstName, x => x.MapFrom(z => z.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(z => z.LastName))
                .ReverseMap();

            CreateMap<serviceModel.Order, entitieModels.Order>().ReverseMap();
            CreateMap<apiModel.Order.Order, serviceModel.Order>().ReverseMap();
        }
    }
}
