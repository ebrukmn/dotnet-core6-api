using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerCore.Models;

namespace NLayerRepository.Seeds;

public class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product { Id = 1, Name = "Faber Castel", Price = 100, CategoryId = 1 },
            new Product { Id = 2, Name = "Stabilo", Price = 150, CategoryId = 1 },
            new Product { Id = 3, Name = "Rotring", Price = 76, CategoryId = 1 },
            
            new Product { Id = 4, Name = "Yolda", Price = 44, CategoryId = 2 },
            new Product { Id = 5, Name = "Kendine Ait Bir Oda", Price = 76, CategoryId = 2 },
            
            new Product { Id = 6, Name = "GIPTA", Price = 76, CategoryId = 3 }
        );
    }
}