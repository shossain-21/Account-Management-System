using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementSystem.Pages.Accounts.Controls
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Control> Control { get; set; }

        public async Task OnGetAsync()
        {
            Control = await _context.AspNetControls
                .FromSqlRaw("EXEC spGetAllControls")
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteControl @p0", id);
            return RedirectToPage();
        }

    }
}
