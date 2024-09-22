using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transportation_Section_Api.Model;
using transportation_Section_Api.Model.DTOs;

namespace transportation_Section_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackageTransportationController : ControllerBase
    {
        private readonly TravelDbContext _context;

        public PackageTransportationController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: api/PackageTransportation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageTransportationDTO>>> GetPackageTransportations()
        {
            var packageTransportations = await _context.PackageTransportations
                .Include(pt => pt.Package)
                .Include(pt => pt.Transportation)
                .Select(pt => new PackageTransportationDTO
                {
                    PackageTransportationId = pt.PackageTransportationId,
                    PackageId = pt.PackageId,
                    TransportationId = pt.TransportationId,
                    Package = new PackageDto
                    {
                        PackageId = pt.Package.PackageId,
                        PackageName = pt.Package.Name
                    },
                    Transportation = new TransportationDTO
                    {
                        TransportationId = pt.Transportation.TransportationId,
                        DepartureLocation = pt.Transportation.DepartureLocation,
                        DepartureDate = pt.Transportation.DepartureDate,
                        ArrivalTime = pt.Transportation.ArrivalTime,
                        IsActive = pt.Transportation.IsActive,
                        Description = pt.Transportation.Description,
                        Rating = pt.Transportation.Rating
                    }
                }).ToListAsync();

            return Ok(packageTransportations);
        }

        // GET: api/PackageTransportation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PackageTransportationDTO>> GetPackageTransportation(int id)
        {
            var packageTransportation = await _context.PackageTransportations
                .Include(pt => pt.Package)
                .Include(pt => pt.Transportation)
                .Where(pt => pt.PackageTransportationId == id)
                .Select(pt => new PackageTransportationDTO
                {
                    PackageTransportationId = pt.PackageTransportationId,
                    PackageId = pt.PackageId,
                    TransportationId = pt.TransportationId,
                    Package = new PackageDto
                    {
                        PackageId = pt.Package.PackageId,
                        PackageName = pt.Package.Name
                    },
                    Transportation = new TransportationDTO
                    {
                        TransportationId = pt.Transportation.TransportationId,
                        DepartureLocation = pt.Transportation.DepartureLocation,
                        DepartureDate = pt.Transportation.DepartureDate,
                        ArrivalTime = pt.Transportation.ArrivalTime,
                        IsActive = pt.Transportation.IsActive,
                        Description = pt.Transportation.Description,
                        Rating = pt.Transportation.Rating
                    }
                }).FirstOrDefaultAsync();

            if (packageTransportation == null)
            {
                return NotFound();
            }

            return Ok(packageTransportation);
        }

        // POST: api/PackageTransportation
        [HttpPost]
        public async Task<ActionResult<PackageTransportationDTO>> CreatePackageTransportation([FromBody] PackageTransportationDTO packageTransportationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageTransportation = new PackageTransportation
            {
                PackageId = packageTransportationDTO.PackageId,
                TransportationId = packageTransportationDTO.TransportationId
            };

            _context.PackageTransportations.Add(packageTransportation);
            await _context.SaveChangesAsync();

            packageTransportationDTO.PackageTransportationId = packageTransportation.PackageTransportationId;

            return CreatedAtAction(nameof(GetPackageTransportation), new { id = packageTransportation.PackageTransportationId }, packageTransportationDTO);
        }

        // PUT: api/PackageTransportation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackageTransportation(int id, [FromBody] PackageTransportationDTO packageTransportationDTO)
        {
            if (id != packageTransportationDTO.PackageTransportationId)
            {
                return BadRequest("PackageTransportation ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var packageTransportation = await _context.PackageTransportations.FindAsync(id);

            if (packageTransportation == null)
            {
                return NotFound();
            }

            packageTransportation.PackageId = packageTransportationDTO.PackageId;
            packageTransportation.TransportationId = packageTransportationDTO.TransportationId;

            _context.Entry(packageTransportation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PackageTransportation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackageTransportation(int id)
        {
            var packageTransportation = await _context.PackageTransportations.FindAsync(id);

            if (packageTransportation == null)
            {
                return NotFound();
            }

            _context.PackageTransportations.Remove(packageTransportation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
