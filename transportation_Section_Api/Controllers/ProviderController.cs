using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transportation_Section_Api.Model.DTOs;
using transportation_Section_Api.Model;

namespace transportation_Section_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly TravelDbContext _context;

        public ProviderController(TravelDbContext context)
        {
            _context = context;
        }

        // GET: api/Provider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderDTO>>> GetProviders()
        {
            var providers = await _context.Providers
                .Include(p => p.Transportations)
                .Select(p => new ProviderDTO
                {
                    ProviderId = p.ProviderId,
                    Name = p.Name,
                    CompanyName = p.CompanyName,
                    ContactNumber = p.ContactNumber,
                    Address = p.Address,
                    IsVerified = p.IsVerified,
                    Transportations = p.Transportations.Select(t => new TransportationDTO
                    {
                        TransportationId = t.TransportationId,
                        DepartureLocation = t.DepartureLocation,
                        DepartureDate = t.DepartureDate,
                        ArrivalTime = t.ArrivalTime,
                        IsActive = t.IsActive,
                        Description = t.Description,
                        Rating = t.Rating
                    }).ToList()
                }).ToListAsync();

            return Ok(providers);
        }

        // GET: api/Provider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProviderDTO>> GetProvider(int id)
        {
            var provider = await _context.Providers
                .Include(p => p.Transportations)
                .Where(p => p.ProviderId == id)
                .Select(p => new ProviderDTO
                {
                    ProviderId = p.ProviderId,
                    Name = p.Name,
                    CompanyName = p.CompanyName,
                    ContactNumber = p.ContactNumber,
                    Address = p.Address,
                    IsVerified = p.IsVerified,
                    Transportations = p.Transportations.Select(t => new TransportationDTO
                    {
                        TransportationId = t.TransportationId,
                        DepartureLocation = t.DepartureLocation,
                        DepartureDate = t.DepartureDate,
                        ArrivalTime = t.ArrivalTime,
                        IsActive = t.IsActive,
                        Description = t.Description,
                        Rating = t.Rating
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (provider == null)
            {
                return NotFound();
            }

            return Ok(provider);
        }

        // POST: api/Provider
        [HttpPost]
        public async Task<ActionResult<ProviderDTO>> CreateProvider([FromBody] ProviderDTO providerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provider = new Provider
            {
                Name = providerDTO.Name,
                CompanyName = providerDTO.CompanyName,
                ContactNumber = providerDTO.ContactNumber,
                Address = providerDTO.Address,
                IsVerified = providerDTO.IsVerified
            };

            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();

            providerDTO.ProviderId = provider.ProviderId;

            return CreatedAtAction(nameof(GetProvider), new { id = provider.ProviderId }, providerDTO);
        }

        // PUT: api/Provider/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, [FromBody] ProviderDTO providerDTO)
        {
            if (id != providerDTO.ProviderId)
            {
                return BadRequest("Provider ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provider = await _context.Providers.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            provider.Name = providerDTO.Name;
            provider.CompanyName = providerDTO.CompanyName;
            provider.ContactNumber = providerDTO.ContactNumber;
            provider.Address = providerDTO.Address;
            provider.IsVerified = providerDTO.IsVerified;

            _context.Entry(provider).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Provider/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            var provider = await _context.Providers.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
