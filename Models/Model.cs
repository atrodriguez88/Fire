using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fire.Models
{
    [Table("Models")]
    public class Model
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public Make Make { get; set; }

        // model.MakeId = 1 (I dont have to load Makes obj to EF)
        public int MakeId { get; set; }

        public ICollection<Weapon> Weapons { get; set; }

        public Model()
        {
            Weapons = new Collection<Weapon>();
        }
    }
}