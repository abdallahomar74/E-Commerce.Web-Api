using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) : base(
            p => (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId) 
            && (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())) )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.SortingOptions) 
            {
                    case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                    case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                    case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                    case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price); 
                    break;
                    default:
                    break;
            }
            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
