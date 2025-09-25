using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManger(IUnitOfWork _unitOfWork, IMapper _mapper,IBasketRepository _basketRepository,UserManager<ApplicationUser> userManager, IConfiguration configuration) : IServiceManger
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository,_mapper));
        private readonly Lazy<IAuthenticationService> _lazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration,_mapper));
        public IProductService ProductService =>  _lazyProductService.Value;

        public IBasketService BasketService => _lazyBasketService.Value;

        public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    }
}
