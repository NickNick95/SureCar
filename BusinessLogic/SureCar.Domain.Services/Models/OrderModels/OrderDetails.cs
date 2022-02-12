namespace SureCar.Services.Models.OrderModel
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
