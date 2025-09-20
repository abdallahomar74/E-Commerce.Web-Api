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
    internal class ProductWithCountSpecification : BaseSpecifications<Product, int>
    {
       public ProductWithCountSpecification(ProductQueryParams queryParams) : base(
            p => (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
        }
    }
}
