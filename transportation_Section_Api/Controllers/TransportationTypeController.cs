using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transportation_Section_Api.Model;
using transportation_Section_Api.Model.DTOs;

namespace transportation_Section_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationTypeController : ControllerBase
    {
        private readonly TravelDbContext _context;

        public TransportationTypeController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: api/TransportationType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationTypeDTO>>> GetTransportationTypes()
        {
            var transportationTypes = await _context.TransportationTypes
                .Include(t => t.Transportations)
                .ToListAsync();

            var dtoList = transportationTypes.Select(t => new TransportationTypeDTO
            {
                TransportationTypeId = t.TransportationTypeId,
                TypeName = t.TypeName,
                Transportations = t.Transportations.Select(tr => new TransportationDTO
                {
                    TransportationId = tr.TransportationId,
                    DepartureLocation = tr.DepartureLocation,
                    DepartureDate = tr.DepartureDate,
                    ArrivalTime = tr.ArrivalTime,
                    IsActive = tr.IsActive,
                    ProviderId = tr.ProviderId,
                    Description = tr.Description,
                    Rating = tr.Rating,
                    TransportationTypeId = tr.TransportationTypeId
                }).ToList()
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/TransportationType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransportationTypeDTO>> GetTransportationType(int id)
        {
            var transportationType = await _context.TransportationTypes
                .Include(t => t.Transportations)
                .FirstOrDefaultAsync(t => t.TransportationTypeId == id);

            if (transportationType == null)
            {
                return NotFound();
            }

            var transportationTypeDTO = new TransportationTypeDTO
            {
                TransportationTypeId = transportationType.TransportationTypeId,
                TypeName = transportationType.TypeName,
                Transportations = transportationType.Transportations.Select(tr => new TransportationDTO
                {
                    TransportationId = tr.TransportationId,
                    DepartureLocation = tr.DepartureLocation,
                    DepartureDate = tr.DepartureDate,
                    ArrivalTime = tr.ArrivalTime,
                    IsActive = tr.IsActive,
                    ProviderId = tr.ProviderId,
                    Description = tr.Description,
                    Rating = tr.Rating,
                    TransportationTypeId = tr.TransportationTypeId
                }).ToList()
            };

            return Ok(transportationTypeDTO);
        }

        // POST: api/TransportationType
        [HttpPost]
        public async Task<ActionResult<TransportationTypeDTO>> CreateTransportationType(TransportationTypeDTO transportationTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transportationType = new TransportationType
            {
                TypeName = transportationTypeDTO.TypeName
            };

            _context.TransportationTypes.Add(transportationType);
            await _context.SaveChangesAsync();

            transportationTypeDTO.TransportationTypeId = transportationType.TransportationTypeId;

            return CreatedAtAction(nameof(GetTransportationType), new { id = transportationType.TransportationTypeId }, transportationTypeDTO);
        }

        // PUT: api/TransportationType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransportationType(int id, TransportationTypeDTO transportationTypeDTO)
        {
            if (id != transportationTypeDTO.TransportationTypeId)
            {
                return BadRequest();
            }

            var transportationType = await _context.TransportationTypes.FindAsync(id);
            if (transportationType == null)
            {
                return NotFound();
            }

            transportationType.TypeName = transportationTypeDTO.TypeName;

            _context.Entry(transportationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportationTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TransportationType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransportationType(int id)
        {
            var transportationType = await _context.TransportationTypes.FindAsync(id);
            if (transportationType == null)
            {
                return NotFound();
            }

            _context.TransportationTypes.Remove(transportationType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransportationTypeExists(int id)
        {
            return _context.TransportationTypes.Any(e => e.TransportationTypeId == id);
        }
    }
}
