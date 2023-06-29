using AutoMapper;
using WebjarProj.Models;
using WebjarProj.Models.Requests;

namespace WebjarProj.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAddonRequest, Addon>();
            CreateMap<UpdateAddonRequest, Addon>();
            CreateMap<CreateFeatureRequest, Feature>();
            CreateMap<UpdateFeatureRequest, Feature>();
        }
    }
}