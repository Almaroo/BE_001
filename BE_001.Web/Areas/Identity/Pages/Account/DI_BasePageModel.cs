using BE_001.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BE_001.Web.Areas.Identity.Pages.Account;

public abstract class DI_BasePageModel : PageModel
{
    protected readonly SignInManager<User> SignInManager;
    protected readonly UserManager<User> UserManager;

    protected DI_BasePageModel(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        SignInManager = signInManager;
        UserManager = userManager;
    }
}