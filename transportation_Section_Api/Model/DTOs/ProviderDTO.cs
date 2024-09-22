using System.ComponentModel.DataAnnotations;

namespace transportation_Section_Api.Model.DTOs
{
    public class ProviderDTO
    {
        public int ProviderId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string CompanyName { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public bool IsVerified { get; set; }

        // Collection of Transportations (using DTO to avoid exposing the entity directly)
        public List<TransportationDTO> Transportations { get; set; }
    }
}
