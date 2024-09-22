using System.ComponentModel.DataAnnotations;

namespace transportation_Section_Api.Model.DTOs
{
    public class TransportationDTO
    {
        public int TransportationId { get; set; }
        public int TransportationTypeId { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalTime { get; set; }
        public bool IsActive { get; set; }
        public int ProviderId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        // Navigation properties (use DTOs instead of direct entities)
        public TransportationTypeDTO TransportationType { get; set; }
        public ProviderDTO Provider { get; set; }

        // A transportation can be part of multiple packages (we'll use PackageTransportationDTO)
        public List<PackageTransportationDTO> PackageTransportations { get; set; }
    }
}