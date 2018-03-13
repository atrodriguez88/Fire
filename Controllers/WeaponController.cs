using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Post([FromBody] WeaponResource weaponResource)
        {
            var weapon = mapper.Map<WeaponResource, Weapon>(weaponResource);
            weapon.LastUpdate = DateTime.Now;
            context.Weapons.Add(weapon);
            await context.SaveChangesAsync();

            var result = mapper.Map<Weapon, WeaponResource>(weapon);
            return Ok(result);
        }
    }
}