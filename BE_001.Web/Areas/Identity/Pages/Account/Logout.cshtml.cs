using BE_001.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BE_001.Web.Areas.Identity.Pages.Account;

public class LogoutModel : DI_BasePageModel
{
    private readonly ILogger<LogoutModel> _logger;

    public LogoutModel(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<LogoutModel> logger) : base(signInManager, userManager)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await SignInManager.SignOutAsync();
        _logger.LogInformation("User {UserName} logged out", HttpContext.User.Identity?.Name);
        return LocalRedirect("~/");
    }
}