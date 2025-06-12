using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data;

namespace AccountManagementSystem.Pages.Accounts.Vouchers
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IVoucherHelperService _voucherHelper;

        public IndexModel(AppDbContext context, IVoucherHelperService voucherHelper)
        {
            _context = context;
            _voucherHelper = voucherHelper;
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = new Voucher();
        [BindProperty]
        public VoucherItem VoucherItem { get; set; } = new VoucherItem();

        public required List<VoucherItem> VoucherItems { get; set; }

        public string VoucherTypeName { get; set; } = string.Empty;


        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            VoucherTypeId = id;
            VoucherTypeName = _voucherHelper.GetVoucherTypeName(VoucherTypeId);

            var voucherTypeIdParam = new SqlParameter("@VoucherTypeId", VoucherTypeId);
            Vouchers = await _context.AspNetVouchers
                    .FromSqlRaw("EXEC spGetVouchersByVoucherTypeId @VoucherTypeId", voucherTypeIdParam)
                    .ToListAsync();

            VoucherItems = new List<VoucherItem>();

            foreach (var voucher in Vouchers)
            {
                var voucherIdParam = new SqlParameter("@VoucherId", voucher.Id);
                var items = await _context.AspNetVoucherItems
                    .FromSqlRaw("EXEC spGetVoucherItemsByVoucherId @VoucherId", voucherIdParam)
                    .ToListAsync();

                foreach (var item in items)
                {
                    item.Voucher = voucher; 
                }

                VoucherItems.AddRange(items);
            }

            return Page();
        }

        public List<Voucher> Vouchers { get; set; }
        public int VoucherTypeId { get; set; }
    }
}
