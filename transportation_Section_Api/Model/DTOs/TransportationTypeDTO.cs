using System.ComponentModel.DataAnnotations;

namespace transportation_Section_Api.Model.DTOs
{
    public class TransportationTypeDTO
    {
        public int TransportationTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        // Collection of Transportations (use DTOs to avoid exposing entity classes)
        public List<TransportationDTO> Transportations { get; set; }
    }
}