using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
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

        [BindProperty]
        public Control Control { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var codeParam = new SqlParameter("@Code", Control.Code);
            var nameParam = new SqlParameter("@Name", Control.Name);

            await _context.Database.ExecuteSqlRawAsync("EXEC spCreateControl @Code, @Name", codeParam, nameParam);

            return RedirectToPage("/Accounts/Controls/Index");
        }
    }
}
