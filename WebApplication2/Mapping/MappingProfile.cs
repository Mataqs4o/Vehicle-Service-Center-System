using AutoMapper;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ServiceAppointmentViewModel, ServiceRecord>()
            .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.EstimatedCost));
    }
}
