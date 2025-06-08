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
    }
}
