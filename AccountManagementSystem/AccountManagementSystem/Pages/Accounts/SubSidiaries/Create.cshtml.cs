using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using AccountManagementSystem.Services;
using AccountManagementSystem.Models.Accounts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementSystem.Pages.Accounts.SubSidiaries
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public required SubSidiary SubSidiary { get; set; }
        public List<Control>? Controls { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Controls = await _context.AspNetControls.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var controlIdParam = new SqlParameter("@ControlId", SubSidiary.ControlId);
            var codeParam = new SqlParameter("@Code", SubSidiary.Code ?? (object)DBNull.Value);
            var nameParam = new SqlParameter("@Name", SubSidiary.Name ?? (object)DBNull.Value);
            var openingDrParam = new SqlParameter("@OpeningDr", SubSidiary.OpeningDr);
            var openingCrParam = new SqlParameter("@OpeningCr", SubSidiary.OpeningCr);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC spCreateSubSidiary @ControlId, @Code, @Name, @OpeningDr, @OpeningCr",
                controlIdParam, codeParam, nameParam, openingDrParam, openingCrParam);

            return RedirectToPage("/Accounts/SubSidiaries/Index");
        }


    }
}