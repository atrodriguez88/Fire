using System.Collections.Generic;
using System.Threading.Tasks;
using Fire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fire.Controllers
{
    public class MakesController : Controller
    {
        private readonly FireDbContext context;

        public MakesController(FireDbContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<Make>> Get()
        {
            return await context.Makes.Include( m => m.Models).ToListAsync();
        }
    }
}