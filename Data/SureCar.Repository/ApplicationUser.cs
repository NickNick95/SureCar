using Microsoft.AspNetCore.Identity;

namespace SureCar.Repositories
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
