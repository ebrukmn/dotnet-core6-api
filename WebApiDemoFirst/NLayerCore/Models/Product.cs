namespace NLayerCore.Models;

public class Product: BaseEntity
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }

    //Navigation Properties
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ProductFeature ProductFeature { get; set; }
}