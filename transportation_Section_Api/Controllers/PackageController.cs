using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transportation_Section_Api.Model;
using transportation_Section_Api.Model.DTOs;

namespace transportation_Section_Api.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly TravelDbContext _context;

        public PackageController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: api/Package
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageDto>>> GetPackages()
        {
            var packages = await _context.Packages
                .Select(p => new PackageDto
                {
                    PackageId = p.PackageId,
                    PackageName = p.Name
                }).ToListAsync();

            return Ok(packages); // Returns a list of PackageDTOs
        }

        // GET: api/Package/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageDto>> GetPackage(int id)
        {
            var package = await _context.Packages
                .Where(p => p.PackageId == id)
                .Select(p => new PackageDto
                {
                    PackageId = p.PackageId,
                    PackageName = p.Name
                }).FirstOrDefaultAsync();

            if (package == null)
            {
                return NotFound();
            }

            return Ok(package); // Returns a single PackageDTO
        }

        // POST: api/Package
        [HttpPost]
        public async Task<ActionResult<PackageDto>> CreatePackage([FromBody] PackageDto packageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var package = new Package
            {
                Name = packageDto.PackageName
            };

            _context.Packages.Add(package);
            await _context.SaveChangesAsync();

            packageDto.PackageId = package.PackageId;

            return CreatedAtAction(nameof(GetPackage), new { id = packageDto.PackageId }, packageDto);
        }

        // PUT: api/Package/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(int id, [FromBody] PackageDto packageDto)
        {
            if (id != packageDto.PackageId)
            {
                return BadRequest("Package ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var package = await _context.Packages.FindAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            package.Name = packageDto.PackageName;

            _context.Entry(package).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Package/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _context.Packages.FindAsync(id);

            if (package == null)
            {
                return NotFound();
            }

            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }





    }
}
