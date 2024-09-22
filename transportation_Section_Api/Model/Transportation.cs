using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace transportation_Section_Api.Model
{
    public class Transportation
    {
        public int TransportationId { get; set; }

        public int TransportationTypeId { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalTime { get; set; }

        public bool IsActive { get; set; }
        public int ProviderId { get; set; } // New relationship with Provider

        [StringLength(500)]
        public string Description { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        // Navigation properties
        public TransportationType TransportationType { get; set; }
        public Provider Provider { get; set; } // New navigation property

        // Collection - Transportation can be part of multiple packages
        [JsonIgnore]
        public ICollection<PackageTransportation> PackageTransportations { get; set; }
    }
}
