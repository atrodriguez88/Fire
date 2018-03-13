using System.ComponentModel.DataAnnotations.Schema;

namespace Fire.Models
{
    [Table("WeaponFeatures")]
    public class WeaponFeature
    {
        public int Id { get; set; }
        public Weapon Weapon { get; set; }
        public int WeaponId { get; set; }
        public Feature Feature { get; set; }
        public int FeatureId { get; set; }
    }
}