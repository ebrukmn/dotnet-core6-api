using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerCore.Models;

namespace NLayerRepository.Seeds;

public class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category { Id = 1, CategoryName = "Kalemler" },
            new Category { Id = 2, CategoryName = "Kitaplar" },
            new Category { Id = 3, CategoryName = "Defterler" });
    }
}