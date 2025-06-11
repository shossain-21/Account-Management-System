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
using Microsoft.AspNetCore.Mvc.Rendering; 

namespace AccountManagementSystem.Pages.Accounts.Vouchers
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IVoucherHelperService _voucherHelper;

        public EditModel(AppDbContext context, IVoucherHelperService voucherHelper)
        {
            _context = context;
            _voucherHelper = voucherHelper;
        }

        [BindProperty]
        public Voucher Voucher { get; set; } = new Voucher();

        [BindProperty]
        public List<VoucherItem> VoucherItems { get; set; } = new List<VoucherItem>();

        public List<Control> AllControls { get; set; } = new List<Control>();

        public List<SelectListItem> AllSubsidiaries { get; set; } = new List<SelectListItem>();

        public string VoucherTypeName { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucherParam = new SqlParameter("@VoucherId", id.Value);
            Voucher = _context.AspNetVouchers
                              .FromSqlRaw("EXEC spGetVoucherById @VoucherId", voucherParam)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (Voucher == null)
            {
                return NotFound();
            }

            var voucherItemsParam = new SqlParameter("@VoucherId", id.Value);
            VoucherItems = _context.AspNetVoucherItems
                                   .FromSqlRaw("EXEC spGetVoucherItemsByVoucherId @VoucherId", voucherItemsParam)
                                   .AsEnumerable()
                                   .ToList();

            AllControls = _context.AspNetControls
                                  .FromSqlRaw("EXEC spGetAllControls")
                                  .AsEnumerable()
                                  .ToList();

            var allSubsidiariesData = _context.AspNetSubSidiaries
                                              .FromSqlRaw("EXEC spGetAllSubSidiaries")
                                              .AsEnumerable()
                                              .ToList();

            AllSubsidiaries = new List<SelectListItem>(); 
            foreach (var subData in allSubsidiariesData)
            {
                AllSubsidiaries.Add(new SelectListItem
                {
                    Value = subData.Id.ToString(), 
                    Text = subData.Name
                });
            }

            VoucherTypeName = _voucherHelper.GetVoucherTypeName(Voucher.VoucherTypeId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AllControls = _context.AspNetControls
                                  .FromSqlRaw("EXEC spGetAllControls")
                                  .AsEnumerable()
                                  .ToList();

            VoucherTypeName = _voucherHelper.GetVoucherTypeName(Voucher.VoucherTypeId);

            var allSubsidiariesData = _context.AspNetSubSidiaries
                                              .FromSqlRaw("EXEC spGetAllSubSidiaries")
                                              .AsEnumerable()
                                              .ToList();

            foreach (var subData in allSubsidiariesData)
            {
                AllSubsidiaries.Add(new SelectListItem
                {
                    Value = subData.Id.ToString(), 
                    Text = subData.Name
                });
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var bankParam = new SqlParameter("@Bank", (object?)Voucher.Bank ?? DBNull.Value);
                var chequeNoParam = new SqlParameter("@ChequeNo", (object?)Voucher.ChequeNo ?? DBNull.Value);
                var chequeDateParam = new SqlParameter("@ChequeDate", (Voucher.ChequeDate == default(DateTime) || Voucher.ChequeDate == null) ? (object)DBNull.Value : Voucher.ChequeDate);

                var voucherNoParam = new SqlParameter("@VoucherNo", Voucher.VoucherNo);
                var voucherDateParam = new SqlParameter("@VoucherDate", Voucher.VoucherDate);
                var voucherTypeIdParam = new SqlParameter("@VoucherTypeId", Voucher.VoucherTypeId);
                var titleParam = new SqlParameter("@Title", Voucher.Title);
                var idParam = new SqlParameter("@Id", Voucher.Id);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spUpdateVoucher @Id, @Bank, @ChequeNo, @ChequeDate, @VoucherNo, @VoucherDate, @VoucherTypeId, @Title",
                    idParam, bankParam, chequeNoParam, chequeDateParam, voucherNoParam, voucherDateParam, voucherTypeIdParam, titleParam);

                var voucherItemsTable = new DataTable("VoucherItemType");
                voucherItemsTable.Columns.Add("Id", typeof(int));
                voucherItemsTable.Columns.Add("ControlId", typeof(int));
                voucherItemsTable.Columns.Add("SubSidiaryId", typeof(int));
                voucherItemsTable.Columns.Add("Debit", typeof(decimal));
                voucherItemsTable.Columns.Add("Credit", typeof(decimal));
                voucherItemsTable.Columns.Add("Description", typeof(string));

                foreach (var item in VoucherItems)
                {
                    voucherItemsTable.Rows.Add(
                        item.Id == 0 ? (object)DBNull.Value : item.Id,
                        item.ControlId,
                        item.SubSidiaryId,
                        item.Debit,
                        item.Credit,
                        (object?)item.Description ?? DBNull.Value
                    );
                }

                var voucherItemsTvpParam = new SqlParameter("@VoucherItems", voucherItemsTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.VoucherItemType"
                };

                var voucherIdForItemsParam = new SqlParameter("@VoucherId", Voucher.Id);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC spManageVoucherItems @VoucherId, @VoucherItems",
                    voucherIdForItemsParam, voucherItemsTvpParam);

                await transaction.CommitAsync();

                return RedirectToPage("./Details", new { id = Voucher.Id });
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "An error occurred while saving the voucher: " + ex.Message);
                Console.WriteLine(ex.ToString());
                return Page();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                Console.WriteLine(ex.ToString());
                return Page();
            }
        }

        public async Task<JsonResult> OnPostDeleteItem(int id)
        {
            var itemIdParam = new SqlParameter("@Id", id);

            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC spDeleteVoucherItem @Id", itemIdParam);
                return new JsonResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting voucher item {id}: {ex.Message}");
                return new JsonResult(false);
            }
        }
    }
}