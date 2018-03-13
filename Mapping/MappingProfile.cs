using System.Linq;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Models;

namespace Fire.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Weapon, WeaponResource>()
            .ForMember(wr => wr.Contact, opt => opt.MapFrom(w =>
                new ContactResource { Name = w.ContactName, Email = w.ContactEmail, Phone = w.ContactPhone }))
            .ForMember(wr => wr.Features, opt => opt.MapFrom(w => w.Features.Select(wf => wf.Id)));

            // API Resource to Domain
            CreateMap<WeaponResource, Weapon>() // opt = operation
            .ForMember(w => w.ContactName, opt => opt.MapFrom(wr => wr.Contact.Name))
            .ForMember(w => w.ContactEmail, opt => opt.MapFrom(wr => wr.Contact.Email))
            .ForMember(w => w.ContactPhone, opt => opt.MapFrom(wr => wr.Contact.Phone))
            .ForMember(w => w.Features, opt => opt.MapFrom(wr => wr.Features.Select(id => new WeaponFeature { FeatureId = id })));


        }
    }
}