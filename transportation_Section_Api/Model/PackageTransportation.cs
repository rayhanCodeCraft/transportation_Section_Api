namespace transportation_Section_Api.Model
{
    public class PackageTransportation
    { 
        public  int PackageTransportationId { get; set; }   
        public int PackageId { get; set; }
        public int TransportationId { get; set; }

        // Navigation properties
        public Package Package { get; set; }
        public Transportation Transportation { get; set; }

    }
}
