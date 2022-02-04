using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SureCar.Entities
{
    [Table("Locations")]
    public class Location
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
