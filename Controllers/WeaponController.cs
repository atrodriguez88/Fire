using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fire.Controllers
{
    [Route("/api/weapon")]
    public class WeaponController : Controller
    {
        private readonly FireDbContext context;
        private readonly IMapper mapper;
        public WeaponController(FireDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpPost()]
        public async Task<IActionResult> AddWeapon([FromBody] SaveWeaponResource weaponResource)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            // Buissnes Validation
            /*
                if (true)
                {
                    ModelState.AddModelError("key","error");
                    BadRequest(ModelState);
                }
             */

            // If I want to create a public API I need to validate the necessary value (like this) 
            // but in this case I don't need it because it's so fat (code)
            /*
                var model = await context.Models.FindAsync(weaponResource.ModelId);
                if (model == null)
                {
                    ModelState.AddModelError("ModelId","Invalid modelId.");
                    BadRequest(ModelState);
                } 
            */

            var weapon = mapper.Map<SaveWeaponResource, Weapon>(weaponResource);
            weapon.LastUpdate = DateTime.Now;
            context.Weapons.Add(weapon);
            await context.SaveChangesAsync();

            // ***** This code is for return the same type Api (GetWeapon()) *******
            weapon = await context.Weapons
                    .Include(w => w.Features)
                        .ThenInclude(wf => wf.Feature)
                    .Include(w => w.Model)
                        .ThenInclude(m => m.Make)
                    .SingleOrDefaultAsync(w => w.Id == weapon.Id);
            // *********************************************************************

            var result = mapper.Map<Weapon, SaveWeaponResource>(weapon);
            return Ok(result);
        }

        [HttpPut("{id}")] // /api/weapon/id
        public async Task<IActionResult> UpdateWeapon(int id, [FromBody] SaveWeaponResource weaponResource)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var weapon = await context.Weapons.FindAsync(id);

            if (weapon == null)
                return NotFound();

            mapper.Map<SaveWeaponResource, Weapon>(weaponResource, weapon);
            weapon.LastUpdate = DateTime.Now;
            context.Weapons.Add(weapon);
            await context.SaveChangesAsync();

            // ***** This code is for return the same type Api (GetWeapon()) *******
            weapon = await context.Weapons
                    .Include(w => w.Features)
                        .ThenInclude(wf => wf.Feature)
                    .Include(w => w.Model)
                        .ThenInclude(m => m.Make)
                    .SingleOrDefaultAsync(w => w.Id == weapon.Id);
            // *********************************************************************

            var result = mapper.Map<Weapon, SaveWeaponResource>(weapon);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            var weapon = await context.Weapons.FindAsync(id);
            if (weapon == null)
                return NotFound();

            context.Remove(weapon);
            await context.SaveChangesAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeapon(int id)
        {
            var weapon = await context.Weapons
            .Include(w => w.Features)
                .ThenInclude(wf => wf.Feature)
            .Include(w => w.Model)
                .ThenInclude(m => m.Make)
            .SingleOrDefaultAsync(w => w.Id == id);

            if (weapon == null)
                return NotFound();

            var weaponResource = Mapper.Map<Weapon, WeaponResource>(weapon);
            return Ok(weaponResource);
        }
    }
}