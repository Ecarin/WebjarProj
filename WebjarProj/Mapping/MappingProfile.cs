using AutoMapper;
using WebjarProj.Models;
using WebjarProj.Models.Requests;

namespace WebjarProj.Mapping
{
    public class MappingProfile : Profile
    {
        // Mapping makes code more clear and professional
        public MappingProfile()
        {
            // Map AddonRequests to Addon
            CreateMap<CreateAddonRequest, Addon>();
            CreateMap<UpdateAddonRequest, Addon>();

            // Map FeatureRequests to Feature
            CreateMap<CreateFeatureRequest, Feature>();
            CreateMap<UpdateFeatureRequest, Feature>();
            
            // Map ProductRequests to Product
            CreateMap<ProductRequest, Product>();
            CreateMap<ProductRequest, Product>();
        }
    }
}