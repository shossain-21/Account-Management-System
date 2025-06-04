using AccountManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountManagementSystem.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        private readonly UserManager<AccountUser> userManager;

        public AccountUser? accUser;

        public UserModel(UserManager<AccountUser> userManager)
        {
            this.userManager = userManager;
        }
        public void OnGet()
        {
            var task = userManager.GetUserAsync(User);
            task.Wait();
            accUser = task.Result;
        }
    }
}
