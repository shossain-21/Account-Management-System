using System.ComponentModel.DataAnnotations;

namespace AccountManagementSystem.Models.Accounts
{
    public class VoucherItem
    {
        [Key]
        public int Id { get; set; }

        public int VoucherId { get; set; }
        public Voucher? Voucher { get; set; }

        public int SubSidiaryId { get; set; }
        public SubSidiary? SubSidiary { get; set; }

        public int ControlId { get; set; }
        public Control? Control { get; set; }

        public double? Debit { get; set; }
        public double? Credit { get; set; }

        public string? Description { get; set; }
    }
}
