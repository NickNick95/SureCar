using AutoMapper;
using entitieModel = SureCar.Entities;
using jsonModel = SureCar.DataManager.Models;

namespace SureCar.API.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<jsonModel.Warehouse, entitieModel.Warehouse>().ReverseMap();
            CreateMap<jsonModel.Car, entitieModel.Car>().ReverseMap();

        }
    }
}
