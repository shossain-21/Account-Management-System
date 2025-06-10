namespace AccountManagementSystem.Services
{
    public class VoucherHelperService : IVoucherHelperService
    {
        public string GetVoucherTypeName(int voucherTypeId)
        {
            return voucherTypeId switch
            {
                1 => "Journal Voucher",
                2 => "Cash Payment Voucher",
                3 => "Cash Receipt Voucher",
                4 => "Bank Payment Voucher",
                5 => "Bank Receipt Voucher",
                _ => "Unknown Voucher Type"
            };
        }
    }
}
