using Microsoft.AspNetCore.Identity;

namespace FoodOrdering.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string? RoleName { get; set; }
    }
}