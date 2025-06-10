using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountManagementSystem.Pages.CommonAjaxCalls
{
    public class MathHelperAjaxModel : PageModel
    {
        private readonly AppDbContext _context;

        public MathHelperAjaxModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet() => Page();

        public string GetVoucherTypeName(int voucherTypeId)
        {
            string voucherTypeName = voucherTypeId switch
            {
                1 => "Journal Voucher",
                2 => "Cash Payment Voucher",
                3 => "Cash Receipt Voucher",
                4 => "Bank Payment Voucher",
                5 => "Bank Receipt Voucher",
                _ => "Unknown Voucher Type"
            };

            return voucherTypeName;
        }
    }
}
