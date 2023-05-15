using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BE_001.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BE_001.Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
// added for easy post 
[IgnoreAntiforgeryToken(Order = 1001)]
public class RegisterModel : DI_BasePageModel
{
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<RegisterModel> logger) 
        : base(signInManager, userManager)
    {
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }
    
    public async Task OnGetAsync(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        ReturnUrl ??= "~/";
        
        if (ModelState.IsValid)
        {
            var user = new User()
            {
                Email = Input.Email,
                UserName = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName
            };
        
            var result = await UserManager.CreateAsync(user, Input.Password);
        
            if (result.Succeeded)
            {
                _logger.LogInformation("Successfully created user: {UserName}", Input.Email);
                await SignInManager.SignInAsync(user, false);
                return LocalRedirect(ReturnUrl);
            }
        
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
    
    public class InputModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DisplayName("Password")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(25, MinimumLength = 3)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
    }
}