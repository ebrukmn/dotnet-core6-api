using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayerCore.Models;

namespace NLayerRepository;

public class AppDbContext : DbContext
{
    //Buradaki options'ı veritabanı yolunu Startup dosyasından vermek istediğim için kullandım.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
            
    }

    //Tablolarımızı set ettik. ProductFeature ayrı bir tablo olmasını istemediğim için set etmedim.
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //tüm assembly kodlarını reflection ile assembly'den okur
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
        {
            Id = 1,
            Color = "Kırmızı",
            Width = 100,
            Height = 200,
            ProductId = 1
        });
        
        base.OnModelCreating(modelBuilder);
    }
}