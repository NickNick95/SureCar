namespace SureCar.Services.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int? UserId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
