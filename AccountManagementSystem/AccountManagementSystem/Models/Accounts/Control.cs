using System.ComponentModel.DataAnnotations;

namespace AccountManagementSystem.Models.Accounts
{
    public class Control
    {
        [Key]
        public int Id { get; set; }

        public required string Code { get; set; }
        
        public required string Name { get; set; }

        public required List<SubSidiary> SubSidiaries { get; set; }
    }
}