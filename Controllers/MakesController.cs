using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Persistent.Models;
using Fire.Persistent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fire.Controllers
{
    public class MakesController : Controller
    {
        private readonly IMapper mapper;
        private readonly Persistent.FireDbContext context;

        public MakesController(Persistent.FireDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/weapon/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            // return mapper.Map<List<MakeResource>>(makes); It is the same
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}