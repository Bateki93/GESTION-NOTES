using AutoMapper;
using GestionNotes.Core.Models;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<Note, NoteModel>()
            .ForMember(dest => dest.EleveNom, opt => opt.MapFrom(src => src.Eleve.Nom))
            .ForMember(dest => dest.ElevePrenom, opt => opt.MapFrom(src => src.Eleve.Prenom))
            .ForMember(dest => dest.EleveMatricule, opt => opt.MapFrom(src => src.Eleve.Matricule))
            .ForMember(dest => dest.MatiereLibelle, opt => opt.MapFrom(src => src.Matiere.Libelle));

        CreateMap<NoteModel, Note>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Eleve, opt => opt.Ignore())
            .ForMember(dest => dest.Matiere, opt => opt.Ignore());
    }
}
