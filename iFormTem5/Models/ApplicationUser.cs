using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace iFormTem5.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public bool IsBlocked { get; set; } = false;
        public bool Isblocked { get; internal set; }
        public DateTime? LastLoginTime { get; set; }
        public string Role { get; internal set; }
    }
}
