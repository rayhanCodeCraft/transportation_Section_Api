using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace transportation_Section_Api.Model
{
    public class TransportationType
    {
        public int TransportationTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string TypeName { get; set; }

        // Collection - TransportationType can have multiple Transportations
        [JsonIgnore]
        public ICollection<Transportation> Transportations { get; set; }

    }
}
