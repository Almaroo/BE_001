using System.ComponentModel.DataAnnotations;
using BE_001.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BE_001.Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LoginModel : DI_BasePageModel
{
    private readonly ILogger<LoginModel> _logger;
    
    public LoginModel(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<LoginModel> logger) 
        : base(signInManager, userManager)
    {
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }
    
    public string ReturnUrl { get; set; }

    public class InputModel
    {
        [Required(ErrorMessage = "Login is required")] 
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    
    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }
 
        returnUrl ??= Url.Content("~/");
 
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
 
        ReturnUrl = returnUrl;
    }
 
    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
 
        
        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await SignInManager.PasswordSignInAsync(Input.Login, Input.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User {UserName} logged in.", Input.Login);
                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }
 
        // If we got this far, something failed, redisplay form
        return Page();
    }
}