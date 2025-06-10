using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq; // Keep this for LINQ-to-Objects (e.g., .Sum(), .Where() on lists)
using System.Threading.Tasks;
using System;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering; // Added this using directive

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

        // This property will hold all subsidiaries as SelectListItems for the dropdown
        public List<SelectListItem> AllSubsidiaries { get; set; } = new List<SelectListItem>();

        // REMOVE THIS PROPERTY if you no longer need the dictionary for specific control IDs.
        // public Dictionary<int, List<SubSidiary>> SubsidiariesForExistingItems { get; set; } = new Dictionary<int, List<SubSidiary>>();


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

            // --- START: MODIFIED SECTION FOR AllSubsidiaries in OnGetAsync ---
            // Fetch all subsidiaries from the database (You need to ensure spGetAllSubSidiaries exists)
            var allSubsidiariesData = _context.AspNetSubSidiaries
                                              .FromSqlRaw("EXEC spGetAllSubSidiaries")
                                              .AsEnumerable()
                                              .ToList();

            // Manually populate AllSubsidiaries from the fetched data (avoiding LINQ Select)
            AllSubsidiaries = new List<SelectListItem>(); // Initialize to ensure it's clean
            foreach (var subData in allSubsidiariesData)
            {
                AllSubsidiaries.Add(new SelectListItem
                {
                    Value = subData.Id.ToString(), // Convert int Id to string for SelectListItem.Value
                    Text = subData.Name
                });
            }
            // --- END: MODIFIED SECTION FOR AllSubsidiaries in OnGetAsync ---

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

            // --- START: MODIFIED SECTION FOR AllSubsidiaries in OnPostAsync ---
            // Fetch all subsidiaries from the database (You need to ensure spGetAllSubSidiaries exists)
            var allSubsidiariesData = _context.AspNetSubSidiaries
                                              .FromSqlRaw("EXEC spGetAllSubSidiaries")
                                              .AsEnumerable()
                                              .ToList();

            // Manually populate AllSubsidiaries from the fetched data (avoiding LINQ Select)
            AllSubsidiaries = new List<SelectListItem>(); // Initialize to ensure it's clean
            foreach (var subData in allSubsidiariesData)
            {
                AllSubsidiaries.Add(new SelectListItem
                {
                    Value = subData.Id.ToString(), // Convert int Id to string for SelectListItem.Value
                    Text = subData.Name
                });
            }
            // --- END: MODIFIED SECTION FOR AllSubsidiaries in OnPostAsync ---


            // REMOVE this entire block, as it refers to the removed SubsidiariesForExistingItems dictionary
            // and the previous logic for uniqueControlIds is no longer necessary here for populating options.
            // var uniqueControlIds = VoucherItems.Where(vi => vi.ControlId > 0).Select(vi => vi.ControlId).Distinct().ToList();
            // foreach (var controlId in uniqueControlIds)
            // {
            //     var subsidiaryParam = new SqlParameter("@ControlId", controlId);
            //     var subsidiaries = _context.AspNetSubSidiaries
            //                                .FromSqlRaw("EXEC spGetSubSidiariesByControlId @ControlId", subsidiaryParam)
            //                                .AsEnumerable()
            //                                .ToList();
            //     if (!SubsidiariesForExistingItems.ContainsKey(controlId)) // THIS WAS THE ERROR LINE
            //     {
            //         SubsidiariesForExistingItems.Add(controlId, subsidiaries);
            //     }
            // }


            if (!ModelState.IsValid)
            {
                // Ensure data is reloaded for page display if validation fails
                // The AllControls and AllSubsidiaries are already loaded above, so no need to reload them here.
                return Page();
            }

            //// LINQ-to-Objects operations are fine here for calculation
            //if (VoucherItems.Sum(vi => vi.Debit) != VoucherItems.Sum(vi => vi.Credit))
            //{
            //    ModelState.AddModelError(string.Empty, "Total Debit must equal Total Credit.");
            //    // Ensure data is reloaded for page display if validation fails
            //    // The AllControls and AllSubsidiaries are already loaded above.
            //    return Page();
            //}

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

                return RedirectToPage("./Edit", new { id = Voucher.Id });
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "An error occurred while saving the voucher: " + ex.Message);
                Console.WriteLine(ex.ToString());
                // Ensure data is reloaded for page display if an error occurs
                // The AllControls and AllSubsidiaries are already loaded above.
                return Page();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "An unexpected error occurred: " + ex.Message);
                Console.WriteLine(ex.ToString());
                // Ensure data is reloaded for page display if an error occurs
                // The AllControls and AllSubsidiaries are already loaded above.
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