
using Fire.Core.Models;
using Fire.Persistent.Models;
using Microsoft.EntityFrameworkCore;

namespace Fire.Persistent
{
    public class FireDbContext : DbContext
    {
        public FireDbContext(DbContextOptions<FireDbContext> options) : base(options)
        {
        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }


    /*
        Two ways on MVC 5 for create DbContext

        public WarrioDbContext()
            : base("DefaulConnectionString"){}
        public WarrioDbContext(string connetionString)
            : base(connetionString){}
    */
}