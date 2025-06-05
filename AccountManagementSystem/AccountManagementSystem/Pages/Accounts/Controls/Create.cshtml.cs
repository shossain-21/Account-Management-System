using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using AccountManagementSystem.Services;
using AccountManagementSystem.Models.Accounts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementSystem.Pages.Accounts.Controls
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var codeParam = new SqlParameter("@Code", Control.Code);
            var nameParam = new SqlParameter("@Name", Control.Name);

            await _context.Database.ExecuteSqlRawAsync("EXEC spCreateControl @Code, @Name", codeParam, nameParam);

            return RedirectToPage("/Accounts/Controls/Index");
        }

        [BindProperty]
        public required Control Control { get; set; }
    }
}
