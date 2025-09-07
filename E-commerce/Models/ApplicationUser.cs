using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        // to make name required and not null
        public string Name { get; set; }=string.Empty;
        // to make address not required add ? after string and nullable
        public string? Address { get; set; }
    }
}
