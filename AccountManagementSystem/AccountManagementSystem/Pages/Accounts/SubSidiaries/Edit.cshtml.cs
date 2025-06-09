using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Models._Utilities;

using System.Collections.Generic;
using AccountManagementSystem.Services;

namespace AccountManagementSystem.Pages.Accounts.SubSidiaries
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubSidiary SubSidiary { get; set; }

        public List<Control> Controls { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubSidiary = _context.AspNetSubSidiaries
                                 .FromSqlInterpolated($"EXEC spGetSubsidiaryWithControlDetailsById @Id={id}")
                                 .AsNoTracking()
                                 .AsEnumerable()
                                 .FirstOrDefault();

            if (SubSidiary == null)
            {
                return NotFound();
            }

            Controls = await _context.AspNetControls.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Controls = await _context.AspNetControls.ToListAsync();
                return Page();
            }

            var connection = _context.Database.GetDbConnection();

            await using (var command = connection.CreateCommand())
            {
                command.CommandText = "spUpdateSubSidiary";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", SubSidiary.Id));
                command.Parameters.Add(new SqlParameter("@ControlId", SubSidiary.ControlId));
                command.Parameters.Add(new SqlParameter("@Code", SubSidiary.Code ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Name", SubSidiary.Name ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OpeningDr", SubSidiary.OpeningDr));
                command.Parameters.Add(new SqlParameter("@OpeningCr", SubSidiary.OpeningCr));

                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch (DbException ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the subsidiary. Please try again.");
                    
                    Controls = await _context.AspNetControls.ToListAsync();
                    return Page();
                }
            }

            return RedirectToPage("/Accounts/SubSidiaries/Index");
        }
    }
}