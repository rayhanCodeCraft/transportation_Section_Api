using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transportation_Section_Api.Model;
using transportation_Section_Api.Model.DTOs;

namespace transportation_Section_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransportationController : ControllerBase
    {
        private readonly TravelDbContext _context;

        public TransportationController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: api/Transportation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationDTO>>> GetTransportations()
        {
            var transportations = await _context.Transportations
                .Include(t => t.TransportationType)
                .Include(t => t.Provider)
                .Include(t => t.PackageTransportations)
                .ThenInclude(pt => pt.Package)
                .Select(t => new TransportationDTO
                {
                    TransportationId = t.TransportationId,
                    TransportationTypeId = t.TransportationTypeId,
                    DepartureLocation = t.DepartureLocation,
                    DepartureDate = t.DepartureDate,
                    ArrivalTime = t.ArrivalTime,
                    IsActive = t.IsActive,
                    ProviderId = t.ProviderId,
                    Description = t.Description,
                    Rating = t.Rating,
                    TransportationType = new TransportationTypeDTO
                    {
                        TransportationTypeId = t.TransportationType.TransportationTypeId,
                        TypeName = t.TransportationType.TypeName
                    },
                    Provider = new ProviderDTO
                    {
                        ProviderId = t.Provider.ProviderId,
                        Name = t.Provider.Name,
                        CompanyName = t.Provider.CompanyName,
                        ContactNumber = t.Provider.ContactNumber,
                        Address = t.Provider.Address,
                        IsVerified = t.Provider.IsVerified
                    },
                    PackageTransportations = t.PackageTransportations.Select(pt => new PackageTransportationDTO
                    {
                        PackageTransportationId = pt.PackageTransportationId,
                        PackageId = pt.PackageId,
                        TransportationId = pt.TransportationId,
                        Package = new PackageDto
                        {
                            PackageId = pt.Package.PackageId,
                            PackageName = pt.Package.Name
                        }
                    }).ToList()
                }).ToListAsync();

            return Ok(transportations);
        }

        // GET: api/Transportation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportationDTO>> GetTransportation(int id)
        {
            var transportation = await _context.Transportations
                .Include(t => t.TransportationType)
                .Include(t => t.Provider)
                .Include(t => t.PackageTransportations)
                .ThenInclude(pt => pt.Package)
                .Where(t => t.TransportationId == id)
                .Select(t => new TransportationDTO
                {
                    TransportationId = t.TransportationId,
                    TransportationTypeId = t.TransportationTypeId,
                    DepartureLocation = t.DepartureLocation,
                    DepartureDate = t.DepartureDate,
                    ArrivalTime = t.ArrivalTime,
                    IsActive = t.IsActive,
                    ProviderId = t.ProviderId,
                    Description = t.Description,
                    Rating = t.Rating,
                    TransportationType = new TransportationTypeDTO
                    {
                        TransportationTypeId = t.TransportationType.TransportationTypeId,
                        TypeName = t.TransportationType.TypeName
                    },
                    Provider = new ProviderDTO
                    {
                        ProviderId = t.Provider.ProviderId,
                        Name = t.Provider.Name,
                        CompanyName = t.Provider.CompanyName,
                        ContactNumber = t.Provider.ContactNumber,
                        Address = t.Provider.Address,
                        IsVerified = t.Provider.IsVerified
                    },
                    PackageTransportations = t.PackageTransportations.Select(pt => new PackageTransportationDTO
                    {
                        PackageTransportationId = pt.PackageTransportationId,
                        PackageId = pt.PackageId,
                        TransportationId = pt.TransportationId,
                        Package = new PackageDto
                        {
                            PackageId = pt.Package.PackageId,
                            PackageName = pt.Package.Name
                        }
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (transportation == null)
            {
                return NotFound();
            }

            return Ok(transportation);
        }

        // POST: api/Transportation
        [HttpPost]
        public async Task<ActionResult<TransportationDTO>> CreateTransportation([FromBody] TransportationDTO transportationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportation = new Transportation
            {
                TransportationTypeId = transportationDTO.TransportationTypeId,
                DepartureLocation = transportationDTO.DepartureLocation,
                DepartureDate = transportationDTO.DepartureDate,
                ArrivalTime = transportationDTO.ArrivalTime,
                IsActive = transportationDTO.IsActive,
                ProviderId = transportationDTO.ProviderId,
                Description = transportationDTO.Description,
                Rating = transportationDTO.Rating
            };

            _context.Transportations.Add(transportation);
            await _context.SaveChangesAsync();

            transportationDTO.TransportationId = transportation.TransportationId;
            return CreatedAtAction(nameof(GetTransportation), new { id = transportation.TransportationId }, transportationDTO);


        }
    }
}
