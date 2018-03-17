using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Persistent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fire.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly FireDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(FireDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;

        }

        [HttpGet("/api/features")]
        public async Task<List<FeatureResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();            
            return mapper.Map<List<FeatureResource>>(features);
        }
    }
}