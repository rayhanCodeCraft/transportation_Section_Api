using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace transportation_Section_Api.Model
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [JsonIgnore]
        // Collection - Package can have multiple PackageTransportations
        public ICollection<PackageTransportation> PackageTransportations { get; set;}
    }
}
