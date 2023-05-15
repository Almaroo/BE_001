using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BE_001.Web.Models;

public class User : IdentityUser
{
    public int UserId { get; set; }
    
    [Required]
    [MaxLength(25)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
}