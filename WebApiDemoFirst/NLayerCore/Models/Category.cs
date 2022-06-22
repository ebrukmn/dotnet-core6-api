namespace NLayerCore.Models;

public class Category: BaseEntity
{
    public string CategoryName { get; set; }

    //Navigation Property
    public ICollection<Product> Products { get; set; }
}