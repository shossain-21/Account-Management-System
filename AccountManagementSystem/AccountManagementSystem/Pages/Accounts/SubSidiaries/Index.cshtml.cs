using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementSystem.Pages.Accounts.SubSidiaries
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public required List<SubSidiary> SubSidiaries { get; set; }

        public async Task OnGetAsync()
        {
            SubSidiaries = await _context.AspNetSubSidiaries
                .FromSqlRaw("EXEC spGetAllSubSidiaries")
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var connection = _context.Database.GetDbConnection();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "spDeleteAspNetSubSidiary";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = command.CreateParameter();
                idParam.ParameterName = "@Id";
                idParam.Value = id;
                command.Parameters.Add(idParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }

            return RedirectToPage("Index");
        }

    }
}
