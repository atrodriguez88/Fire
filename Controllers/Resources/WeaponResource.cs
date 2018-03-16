using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Fire.Controllers.Resources
{
    public class WeaponResource
    {
        public int Id { get; set; }
        public ModelResource Model { get; set; }
        public MakeResource Make { get; set; }
        public ContactResource Contact { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<FeatureResource> Features { get; set; }
        public WeaponResource()
        {
            Features = new Collection<FeatureResource>();
        }
    }
}