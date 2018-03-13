using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Fire.Controllers.Resources
{
    public class WeaponResource
    {
        public int Id { get; set; }
        // public string Name { get; set; }
        // public string Email { get; set; }
        // public string Phone { get; set; }
        public int ModelId { get; set; }        
        public ContactResource Contact { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<int> Features { get; set; }
        public WeaponResource()
        {
            Features = new Collection<int>();
        }
    }
}