using System.Threading.Tasks;
using Fire.Models;
using Microsoft.EntityFrameworkCore;

namespace Fire.Persistent
{
    public class WeaponRepository
    {
        private readonly FireDbContext context;
        public WeaponRepository(FireDbContext context)
        {
            this.context = context;

        }
        public async Task<Weapon> GetWeapon(int id)
        {
            // ***** This code is for return the same type Api (GetWeapon()) *******
            var weapon = await context.Weapons
                    .Include(w => w.Features)
                        .ThenInclude(wf => wf.Feature)
                    .Include(w => w.Model)
                        .ThenInclude(m => m.Make)
                    .SingleOrDefaultAsync(w => w.Id == id);

                    return weapon;
            // *********************************************************************
        }
    }
}