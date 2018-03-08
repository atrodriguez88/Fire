using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Fire.Models
{    
    public class Make
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        // I don't use List<> because i don't access to the collection by index
        public ICollection<Model> Models { get; set; }


        // I can't repeat that code everywhere (make.Models = new Collection<Model>();)
        public Make()
        {
            Models = new Collection<Model>();
        }
    }
}