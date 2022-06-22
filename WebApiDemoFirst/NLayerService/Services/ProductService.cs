using AutoMapper;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Repositories;
using NLayerCore.Services;
using NLayerCore.UnitOfWorks;

namespace NLayerService.Services;

public class ProductService : Service<Product>, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper; 
    
    public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IProductRepository productRepository, IMapper mapper) : base(repository, unitOfWork)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<List<DetailedProductsDto>>> GetProductDetail()
    {
        var products = await _productRepository.GetProductDetails();
        return CustomResponseDto<List<DetailedProductsDto>>.Success(200, _mapper.Map<List<DetailedProductsDto>>(products));
    }
}