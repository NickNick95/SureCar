using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SureCar.Entities
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int YearModel { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public bool Licensed { get; set; }
        public string DateAdded { get; set; }

        public ICollection<VehicleOrder> VehicleOrders { get; set; }
    }
}
