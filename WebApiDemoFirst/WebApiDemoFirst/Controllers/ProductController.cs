using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Services;

namespace WebApiDemoFirst.Controllers
{
    
    public class ProductController : CustomBaseController
    {
        private readonly IMapper _mapper;

        private readonly IProductService _productService;

        public ProductController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productService.GetAll();
            var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));/
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto model)
        {
            var product = await _productService.AddAsync(_mapper.Map<Product>(model));
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto product)
        {
            await _productService.UpdateAsync(_mapper.Map<Product>(product));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toBeDeletedProduct = await _productService.GetByIdAsync(id);
            await _productService.DeleteAsync(toBeDeletedProduct);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductDetail()
        {
            return CreateActionResult(await _productService.GetProductDetail());
        }
    }
}