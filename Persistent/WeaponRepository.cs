using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fire.Core;
using Fire.Core.Models;
using Fire.Persistent.Models;
using Microsoft.EntityFrameworkCore;

namespace Fire.Persistent
{
    public class WeaponRepository : IWeaponRepository
    {
        /// <summary>
        /// No usar IQuerable en Repository
        /// </summary>
        private readonly FireDbContext context;
        public WeaponRepository(FireDbContext context)
        {
            this.context = context;
        }
        public async Task<Weapon> GetWeapon(int id, bool? includeRelated = false)
        {
            if (includeRelated.Value)
            {
                return await context.Weapons.FindAsync(id);
            }

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

        public async Task<IEnumerable<Weapon>> GetWeapons(Filters filters)
        {
            var query = context.Weapons
            .Include(w => w.Features)
                .ThenInclude(wf => wf.Feature)
            .Include(w => w.Model)
                .ThenInclude(m => m.Make)
            .AsQueryable();

            if (filters.MakeId.HasValue)
            {
                query = query.Where(w => w.Model.MakeId == filters.MakeId);
            }

            return await query.ToListAsync();
        }

        public void Add(Weapon weapon)
        {
            context.Weapons.Add(weapon);
        }
        public void Remove(Weapon weapon)
        {
            context.Remove(weapon);
        }


    }
}