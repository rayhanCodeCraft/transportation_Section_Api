namespace transportation_Section_Api.Model.DTOs
{
    public class PackageTransportationDTO
    {
        public int PackageTransportationId { get; set; }
        public int PackageId { get; set; }
        public int TransportationId { get; set; }

        // Include DTOs instead of entity classes for navigation properties
        public PackageDto Package { get; set; }
        public TransportationDTO Transportation { get; set; }
    }
}
