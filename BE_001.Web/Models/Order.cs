namespace BE_001.Web.Models;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public ICollection<Item> Items { get; } = new List<Item>();
}