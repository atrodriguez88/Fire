using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fire.Controllers.Resources;
using Fire.Core;
using Fire.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fire.Controllers
{
    [Route("/api/weapons/{weaponId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IWeaponRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public PhotoSettings photoSettings;

        private readonly IMapper mapper;

        public PhotosController(IHostingEnvironment host, IWeaponRepository repository,
                                IUnitOfWork unitOfWork, IMapper mapper,
                                IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.repository = repository;
            this.host = host;
            this.unitOfWork = unitOfWork;
        }
        /*   /api/weapons/1/photos  */
        [HttpPost]
        public async Task<IActionResult> UploadPhoto(int weaponId, IFormFile file)
        {
            var weapon = await repository.GetWeapon(weaponId);
            if (weapon == null)
                return NotFound("Invalid Weapon" + weaponId);
            if (file == null)
                return BadRequest("Null File");
            if (file.Length <= 0)
                return BadRequest("Empty File");
            if (file.Length > photoSettings.MAX_SIZES)
                return BadRequest("Max file size exceeded"); // Max 10 Mb 
            if (!photoSettings.IsSupport(file.FileName))
                return BadRequest("Invalid File Type");

            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");    // c:\wwwroot\...
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            // Create a file name for security razon
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var photo = new Photo { FileName = fileName };
            weapon.Photos.Add(photo);

            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}