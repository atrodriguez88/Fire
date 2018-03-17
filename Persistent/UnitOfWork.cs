using System.Threading.Tasks;
using Fire.Core;
using Fire.Persistent.Models;

namespace Fire.Persistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FireDbContext context;
        public UnitOfWork(FireDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}