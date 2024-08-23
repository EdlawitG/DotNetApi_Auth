using AutoMapper; // Ensure you have the AutoMapper namespace
using authApi.Models;
using authApi.DTos;
using authApi.Dtos;

namespace authApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<LoginRequest, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CreateProRequest, Product>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<UpdateProRequest, Product>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
