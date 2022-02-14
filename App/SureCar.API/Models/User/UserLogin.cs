using System.ComponentModel.DataAnnotations;

namespace SureCar.API.Models.User
{
    public class UserLogin
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
