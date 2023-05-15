using System.ComponentModel.DataAnnotations;

namespace BE_001.Web.Models;

public class Review
{
    public int ReviewId { get; set; }
    public int ItemId { get; set; }

    public string UserName { get; set; }
    
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
    
    public string Description { get; set; }
}