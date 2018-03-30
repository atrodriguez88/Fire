using System.Linq;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Core.Models;
using Fire.Persistent.Models;

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
            CreateMap<Weapon, SaveWeaponResource>()
            .ForMember(wr => wr.Contact, opt => opt.MapFrom(w =>
                new ContactResource { Name = w.ContactName, Email = w.ContactEmail, Phone = w.ContactPhone }))
            .ForMember(wr => wr.Features, opt => opt.MapFrom(w => w.Features.Select(wf => wf.Id)));
            CreateMap<Weapon, WeaponResource>()
            .ForMember(wr => wr.Contact, opt => opt.MapFrom(w =>
                new ContactResource { Name = w.ContactName, Email = w.ContactEmail, Phone = w.ContactPhone }))
            .ForMember(wr => wr.Model, opt => opt.MapFrom(w => w.Model))
            .ForMember(wr => wr.Make, opt => opt.MapFrom(w => w.Model.Make))
            .ForMember(wr => wr.Features, opt => opt.MapFrom(w => w.Features.Select(wf => new FeatureResource{Id = wf.Feature.Id, Name = wf.Feature.Name})));

            // API Resource to Domain
            CreateMap<FilterResource, Filters>();
            CreateMap<SaveWeaponResource, Weapon>() // opt = operation
            .ForMember(w => w.Id, opt => opt.Ignore())
            .ForMember(w => w.ContactName, opt => opt.MapFrom(wr => wr.Contact.Name))
            .ForMember(w => w.ContactEmail, opt => opt.MapFrom(wr => wr.Contact.Email))
            .ForMember(w => w.ContactPhone, opt => opt.MapFrom(wr => wr.Contact.Phone))
            // .ForMember(w => w.Features, opt => opt.MapFrom(wr => wr.Features.Select(id => new WeaponFeature { FeatureId = id })));

            // Mapping Collection ( for UpdateWeapon() API)
            .ForMember(v => v.Features, opt => opt.Ignore())
              .AfterMap((vr, v) =>
              {
                  // Remove unselected features
                  var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                  foreach (var f in removedFeatures)
                      v.Features.Remove(f);

                  // Add new features
                  var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new WeaponFeature { FeatureId = id }).ToList();
                  foreach (var f in addedFeatures)
                      v.Features.Add(f);
              });



        }
    }
}