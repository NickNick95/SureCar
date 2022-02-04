using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SureCar.Entities
{
    [Table("Warehouses")]
    public class Warehouse
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Locations")]
        public Location Locations { get; set; }

        [ForeignKey("CarId")]
        public Car Cars { get; set; }
    }
}
