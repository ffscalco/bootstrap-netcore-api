using System;
using Api.ViewModels;
using AutoMapper;
using Core.Models;

namespace Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoggedUserVM>()
                .ForMember(x => x.Token, opt => opt.Ignore())
                .ForMember(x => x.Expiration, opt => opt.Ignore());
        }
    }
}
