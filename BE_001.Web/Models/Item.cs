namespace BE_001.Web.Models;

public class Item
{
    public int ItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImgUrl { get; set; }
    public ICollection<Review> Reviews { get; } = new List<Review>();
}