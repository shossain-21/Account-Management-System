using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementSystem.Pages.Accounts.Controls
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _Control = await _context.AspNetControls.FindAsync(id);
            if (_Control == null)
            {
                return NotFound();
            }

            Control = _Control;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var connection = _context.Database.GetDbConnection();
            await using (var command = connection.CreateCommand())
            {
                command.CommandText = "spUpdateAspNetControl";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@Id", Control.Id));
                command.Parameters.Add(new SqlParameter("@Code", Control.Code ?? (object)DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Name", Control.Name ?? (object)DBNull.Value));

                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    await connection.OpenAsync();
                }

                await command.ExecuteNonQueryAsync();
            }

            return RedirectToPage("Index");
        }


        [BindProperty]
        public Control? Control { get; set; }
    }
}
