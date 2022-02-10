using System.ComponentModel.DataAnnotations;

namespace SureCar.API.Models.User
{
    public class UserLogout
    {
        [Required]
        public string UserId { get; set; }
    }
}
