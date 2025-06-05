using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AccountManagementSystem.Models.Users
{
    public class AccountUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string Address { get; set; } = "";
    }
}
