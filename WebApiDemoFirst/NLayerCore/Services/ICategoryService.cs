using NLayerCore.DTOs;
using NLayerCore.Models;

namespace NLayerCore.Services;

public interface ICategoryService : IService<Category>
{
    Task<CustomResponseDto<CategoryProducts>> GetCategoryProducts(int categoryId);
}