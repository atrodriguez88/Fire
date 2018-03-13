using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Fire.Models
{
    public class Feature
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public ICollection<WeaponFeature> Weapon { get; set; }
        public Feature()
        {
            Weapon = new Collection<WeaponFeature>();
        }
    }
}