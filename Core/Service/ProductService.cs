using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandsDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandsDto>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginatedResponse<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications( queryParams);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Products = await Repo.GetAllAsync(Specifications);
            var ProductsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var PageSize = Products.Count();
            var TotalCount = await Repo.CountAsync(new ProductWithCountSpecification(queryParams));
            return new PaginatedResponse<ProductDto>(queryParams.PageIndex, PageSize,TotalCount, ProductsDto);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductType, int>();
            var Types = await Repo.GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithBrandAndTypeSpecifications(id);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Product = await Repo.GetByIdAsync(specification);
            var ProductsDto = _mapper.Map<Product, ProductDto>(Product);
            return ProductsDto;
        }
    }
}
