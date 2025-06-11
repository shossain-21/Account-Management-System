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

        public async Task<IActionResult> OnGetAsync()
        {
            SubSidiaries = await _context.AspNetSubSidiaries
                .FromSqlRaw("EXEC spGetAllSubSidiaries")
                .ToListAsync();

            foreach (var subSidiary in SubSidiaries)
            {
                var controlId = new SqlParameter("@Id", subSidiary.ControlId);
                var control = _context.AspNetControls
                    .FromSqlRaw("EXEC spGetControlDetailsById @Id", controlId)
                    .AsEnumerable()
                    .FirstOrDefault();

                subSidiary.Control = control;
            }

            return Page();
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

        public required List<SubSidiary> SubSidiaries { get; set; }
    }
}
