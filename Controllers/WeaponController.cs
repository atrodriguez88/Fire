using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Core;
using Fire.Core.Models;
using Fire.Persistent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fire.Controllers
{
    [Route("/api/weapon")]
    public class WeaponController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWeaponRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public WeaponController(IMapper mapper, IWeaponRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;

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
            // context.Weapons.Add(weapon);
            repository.Add(weapon);
            // await context.SaveChangesAsync();
            await unitOfWork.CompleteAsync();

            // ***** This code is for return the same type Api (GetWeapon()) *******
            weapon = await repository.GetWeapon(weapon.Id);
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

            var weapon = await repository.GetWeapon(id);

            if (weapon == null)
                return NotFound();

            mapper.Map<SaveWeaponResource, Weapon>(weaponResource, weapon);
            weapon.LastUpdate = DateTime.Now;
            
            // await context.SaveChangesAsync();
            await unitOfWork.CompleteAsync();
            

            // ***** This code is for return the same type Api (GetWeapon()) *******
            weapon = await repository.GetWeapon(weapon.Id);
            // *********************************************************************

            var result = mapper.Map<Weapon, SaveWeaponResource>(weapon);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            var weapon = await repository.GetWeapon(id, includeRelated: false);
            if (weapon == null)
                return NotFound();

            // context.Remove(weapon);
            repository.Remove(weapon);
            // await context.SaveChangesAsync();
            await unitOfWork.CompleteAsync();
            
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeapon(int id)
        {
            var weapon = await repository.GetWeapon(id);

            if (weapon == null)
                return NotFound();

            var weaponResource = Mapper.Map<Weapon, WeaponResource>(weapon);
            return Ok(weaponResource);
        }

        [HttpGet]
        [Route("/api/weapons")]
        public async Task<IActionResult> GetWeapons(FilterResource filterResource)
        {
            var filter = mapper.Map<FilterResource, Filters>(filterResource);
            var weapons = await repository.GetWeapons(filter);

            if (weapons == null)
                return NoContent();

            var weaponsResource = Mapper.Map<IEnumerable<Weapon>, IEnumerable<WeaponResource>>(weapons);
            return Ok(weaponsResource);
        }
    }
}