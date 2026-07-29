using AutoMapper;
using GestionNotes.Core.Models;
using GestionNotes.Infrastructure.Entities;

namespace GestionNotes.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>();
    }
}
