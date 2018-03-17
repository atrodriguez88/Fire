using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fire.Persistent.Models
{
    [Table("Weapons")]
    public class Weapon
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [StringLength(255)]
        public string ContactEmail { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime LastUpdate { get; set; }
        public Model Model { get; set; }
        public int ModelId { get; set; }
        public ICollection<WeaponFeature> Features { get; set; }
        public Weapon()
        {
            Features = new Collection<WeaponFeature>();
        }
    }
}