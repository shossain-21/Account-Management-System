using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccountManagementSystem.Models.Accounts; 
using AccountManagementSystem.Services; 
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; 

namespace AccountManagementSystem.Pages.Accounts.Vouchers
{
    public class VoucherAjaxModel : PageModel
    {
        private readonly AppDbContext _context;

        public VoucherAjaxModel(AppDbContext context)
        {
            _context = context;
        }

        // --- AJAX for fetching Subsidiaries based on ControlId ---
        public async Task<JsonResult> OnPostGetSubSidiariesByControlId(int controlId)
        {
            var controlIdParam = new SqlParameter("@ControlId", controlId);

            // Fetch Subsidiaries using stored procedure
            var subsidiaries = await _context.AspNetSubSidiaries
                                             .FromSqlRaw("EXEC spGetSubSidiariesByControlId @ControlId", controlIdParam)
                                             .AsNoTracking()
                                             .ToListAsync();

            // Return as JSON, mapping to anonymous object with 'id' and 'name' for jQuery's use
            return new JsonResult(subsidiaries.Select(s => new { id = s.Id, name = s.Name }).ToList());
        }
    }
}