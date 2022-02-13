using System.ComponentModel.DataAnnotations;

namespace SureCar.API.Models.Order
{
    public class Order
    {
        [Required(ErrorMessage = "VehicleIds is required")]
        public List<int> VehicleIds { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }
    }
}
