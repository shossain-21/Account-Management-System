using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountManagementSystem.Models.Accounts
{
    public class SubSidiary
    {
        [Key]
        public int Id { get; set; }

        public int ControlId { get; set; }
        public Control? Control { get; set; }

        public required string Code { get; set; }
        public required string Name { get; set; }

        public double OpeningDr { get; set; }
        public double OpeningCr { get; set; }

        
        public string ControlCode { get; set; } = string.Empty;
        
        public string ControlName { get; set; } = string.Empty;

    }
}
