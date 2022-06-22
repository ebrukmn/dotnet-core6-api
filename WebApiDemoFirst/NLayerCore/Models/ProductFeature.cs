﻿namespace NLayerCore.Models;

public class ProductFeature
{
    public int Id { get; set; }
    public string Color { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    
    //Navigation Properties
    public int ProductId { get; set; }
    public Product Product { get; set; }
}