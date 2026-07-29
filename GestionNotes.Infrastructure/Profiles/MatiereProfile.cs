using AutoMapper;
using GestionNotes.Core.Models;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Profiles;

public class MatiereProfile : Profile
{
    public MatiereProfile()
    {
        CreateMap<Matiere, MatiereModel>().ReverseMap();
    }
}
