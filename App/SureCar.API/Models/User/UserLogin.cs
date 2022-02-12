using System.ComponentModel.DataAnnotations;

namespace SureCar.API.Models.User
{
    public class UserLogin
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
