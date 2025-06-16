using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AccountManagementSystem.Models.Accounts;
using AccountManagementSystem.Services;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Data;
using AccountManagementSystem.Models.Users;

namespace AccountManagementSystem.Pages.AdminUserSettings
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<AccountUser> Users { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _context.Users
                .FromSqlRaw("EXEC spGetAllUsers")
                .ToListAsync();

            return Page();
        }
    }
}
