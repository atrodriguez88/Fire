using Microsoft.EntityFrameworkCore;

namespace Fire.Models
{
    public class FireDbContext : DbContext
    {
        public FireDbContext(DbContextOptions<FireDbContext> options) : base(options)
        {
        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
    }


    /*
        Two ways on MVC 5 for create DbContext

        public WarrioDbContext()
            : base("DefaulConnectionString"){}
        public WarrioDbContext(string connetionString)
            : base(connetionString){}
    */
}