using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Repositories;
using NLayerCore.Services;
using NLayerCore.UnitOfWorks;
using NLayerService.Exceptions;

namespace NLayerCaching;

public class ProductServiceWithCaching : IProductService
{
    private const string CacheProductKey = "produtcsCache";
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IUnitOfWork _unitOfWork;

    public ProductServiceWithCaching(IProductRepository repository, IMapper mapper, IMemoryCache memoryCache, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _unitOfWork = unitOfWork;

        if (!_memoryCache.TryGetValue(CacheProductKey, out _))
        {
            _memoryCache.Set(CacheProductKey, _repository.GetProductDetails().Result);
        }
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            throw new NotFoundException($"{typeof(Product)}({id}) Bulunamadı!");
        }
        
        return Task.FromResult(product);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_memoryCache.Get<List<Product>>(CacheProductKey).ToList());
    }

    public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
    {
        return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
    }

    public async Task<Product> AddAsync(Product entity)
    {
        await _repository.AddAsync(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entity;
    }

    public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
    {
        await _repository.AddRangeAsync(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
        return entities;
    }

    public async Task UpdateAsync(Product entity)
    { 
        _repository.Update(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task DeleteAsync(Product entity)
    {
        _repository.Delete(entity);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public async Task RemoveRangeAsync(IEnumerable<Product> entities)
    {
        _repository.RemoveRange(entities);
        await _unitOfWork.CommitAsync();
        await CacheAllProductsAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
    {
        return Task.FromResult(_memoryCache.Get<List<Product>>(CacheProductKey).Any(expression.Compile()));
    }

    public Task<CustomResponseDto<List<DetailedProductsDto>>> GetProductDetail()
    {
        var products = _memoryCache.Get<List<Product>>(CacheProductKey).ToList();
        var detailedProducts = _mapper.Map<List<DetailedProductsDto>>(products);
        return Task.FromResult(CustomResponseDto<List<DetailedProductsDto>>.Success(200, detailedProducts));
    }

    private async Task CacheAllProductsAsync()
    {
        _memoryCache.Set(CacheProductKey, await _repository.GetProductDetails());
    }
}