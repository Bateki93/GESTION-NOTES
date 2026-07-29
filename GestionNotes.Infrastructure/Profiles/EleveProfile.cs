using AutoMapper;
using GestionNotes.Core.Models;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Profiles;

public class EleveProfile : Profile
{
    public EleveProfile()
    {
        CreateMap<Eleve, EleveModel>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

        CreateMap<EleveModel, Eleve>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Notes, opt => opt.Ignore());
    }
}
