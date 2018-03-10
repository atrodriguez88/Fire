using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Models;

namespace Fire.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();

            CreateMap<Feature, FeatureResource>();
            
        }
    }
}