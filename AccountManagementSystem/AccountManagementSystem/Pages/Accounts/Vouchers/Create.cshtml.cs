using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
using Microsoft.Data.SqlClient;

namespace AccountManagementSystem.Pages.Accounts.Vouchers
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IVoucherHelperService _voucherHelper;

        public CreateModel(AppDbContext context, IVoucherHelperService voucherHelper)
        {
            _context = context;
            _voucherHelper = voucherHelper;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            VoucherTypeId = id;

            Voucher = new Voucher
            {
                VoucherTypeId = id,
                VoucherDate = DateTime.Now,
                ChequeDate = DateTime.Now,
                VoucherTypeName = _voucherHelper.GetVoucherTypeName(id)
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
             if (!ModelState.IsValid)
            {
                Voucher.VoucherTypeName = _voucherHelper.GetVoucherTypeName(Voucher.VoucherTypeId);
                return Page();
            }

            var chequeDateValue = Voucher.ChequeDate == DateTime.MinValue ? (object)DBNull.Value : Voucher.ChequeDate;
            var voucherDateValue = Voucher.VoucherDate == DateTime.MinValue ? (object)DBNull.Value : Voucher.VoucherDate;

            var bankParam = new SqlParameter("@Bank", (object?)Voucher.Bank ?? DBNull.Value);
            var chequeNoParam = new SqlParameter("@ChequeNo", (object?)Voucher.ChequeNo ?? DBNull.Value);
            var chequeDateParam = new SqlParameter("@ChequeDate", (object?)Voucher.ChequeDate ?? DBNull.Value);
            var voucherNoParam = new SqlParameter("@VoucherNo", Voucher.VoucherNo);
            var voucherDateParam = new SqlParameter("@VoucherDate", voucherDateValue);
            var voucherTypeIdParam = new SqlParameter("@VoucherTypeId", Voucher.VoucherTypeId);
            var titleParam = new SqlParameter("@Title", Voucher.Title);

            // OUTPUT parameter
            var insertedIdParam = new SqlParameter("@InsertedId", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spCreateVoucher @Bank, @ChequeNo, @ChequeDate, @VoucherNo, @VoucherDate, @VoucherTypeId, @Title, @InsertedId OUT",
                bankParam, chequeNoParam, chequeDateParam, voucherNoParam, voucherDateParam, voucherTypeIdParam, titleParam, insertedIdParam);

            // Read the new inserted Id
            int insertedId = (int)insertedIdParam.Value;

            // Redirect to Edit page
            return RedirectToPage("Accounts/Vouchers/Edit", new { id = Voucher.Id });
        }


        [BindProperty]
        public Voucher Voucher { get; set; }
        public int VoucherTypeId { get; set; }
    }
}
