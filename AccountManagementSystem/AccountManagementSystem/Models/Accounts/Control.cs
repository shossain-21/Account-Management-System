using System.ComponentModel.DataAnnotations;

namespace AccountManagementSystem.Models.Accounts
{
    public class Control
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}