using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace transportation_Section_Api.Model
{
    public class Provider
    {
        public int ProviderId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string  CompanyName { get; set; }

        [Required]
        [StringLength(15)]
        public string ContactNumber { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public bool IsVerified { get; set; }

        // Collection - A provider can offer multiple transportation services
        [JsonIgnore]
        public ICollection<Transportation> Transportations { get; set; }
    }
}
