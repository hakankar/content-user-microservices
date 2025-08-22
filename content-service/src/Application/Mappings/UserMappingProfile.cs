using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ContentMappingProfile: Profile
    {
        public ContentMappingProfile()
        {
            CreateMap<Content, ContentDto>();
        }
    }
}
