using AutoMapper;
using Oblivion.Models;
using Microsoft.AspNetCore.Identity;
using Oblivion.Models.Account;

namespace IdentityByExamples
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email));
        }
    }
}
