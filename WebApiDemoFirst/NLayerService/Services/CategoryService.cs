using AutoMapper;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Repositories;
using NLayerCore.Services;
using NLayerCore.UnitOfWorks;

namespace NLayerService.Services;

public class CategoryService : Service<Category>, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CustomResponseDto<CategoryProducts>> GetCategoryProducts(int categoryId)
    {
        var categoryProducts = await _categoryRepository.GetCategoryProductsAsync(categoryId);
        return CustomResponseDto<CategoryProducts>.Success(200, _mapper.Map<CategoryProducts>(categoryProducts));
    }
}