using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController(IServiceManger _serviceManger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ProductDto>>> GetAllProduct([FromQuery] ProductQueryParams queryParams)
        {
            var Products = await _serviceManger.ProductService.GetAllProductsAsync( queryParams);
            return Ok(Products);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await _serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var Types = await _serviceManger.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandsDto>>> GetAllBrands()
        {
            var Brands = await _serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
