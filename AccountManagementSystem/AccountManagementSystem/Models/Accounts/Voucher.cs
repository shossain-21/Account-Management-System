using System;
using System.ComponentModel.DataAnnotations;

namespace AccountManagementSystem.Models.Accounts
{
    public class Voucher
    {
        [Key]
        public int Id { get; set; }

        public string? Bank { get; set; }
        public string? ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        [Required(ErrorMessage = "Reference No is required")]
        public string VoucherNo { get; set; }
        [Required]
        public DateTime VoucherDate { get; set; }
        public int VoucherTypeId { get; set; }                      // 1 = Journal Voucher
                                                                    // 2 = Cash Payment Voucher
                                                                    // 3 = Cash Receive Voucher
                                                                    // 4 = Cheque Payment Voucher
                                                                    // 5 = Cheque Receive Voucher
        [Required]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string VoucherTypeName { get; set; } = string.Empty;
    }
}
