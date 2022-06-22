using NLayerCore.Models;

namespace NLayerCore.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetCategoryProductsAsync(int categoryId);
}