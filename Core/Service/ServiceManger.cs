using AutoMapper;
using DomainLayer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManger(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManger
    {
        private readonly Lazy<ProductService> _lazyProductService = new Lazy<ProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService =>  _lazyProductService.Value;
    }
}
