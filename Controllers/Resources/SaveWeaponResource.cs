using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Fire.Controllers.Resources
{
    public class SaveWeaponResource
    {
        public int Id { get; set; }
        public int ModelId { get; set; }     
        [Required]   
        public ContactResource Contact { get; set; }
        public bool IsRegistered { get; set; }
        public ICollection<int> Features { get; set; }
        public SaveWeaponResource()
        {
            Features = new Collection<int>();
        }
    }
}