using System.ComponentModel.DataAnnotations.Schema;

namespace SureCar.Entities
{
    public class VehicleOrder
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }
    }
}
