using Microsoft.EntityFrameworkCore;
using NLayerCore.Models;
using NLayerCore.Repositories;
using NLayerCore.Services;

namespace NLayerRepository.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository 
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Category> GetCategoryProductsAsync(int categoryId)
    {
        return await _context.Categories.Include(x => x.Products).Where(category => category.Id == categoryId).SingleOrDefaultAsync();
    }
}