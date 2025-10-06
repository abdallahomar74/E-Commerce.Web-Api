using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications.OrderModuleSpecification;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;
using Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            // Map Address
            var Address = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);
            
            // Create OrderItems List
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundExceptions(item.Id);
                OrderItems.Add(CreateOrderItem(item, Product));
            }
            
            // Get DeliveryMethod
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            
            // calculate SubTotal
            var SubTotal = OrderItems.Sum(I => I.quantity * I.price);
            
            var Order = new Order(email,Address,DeliveryMethod,OrderItems,SubTotal);
            
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();
            
            return _mapper.Map<Order,OrderToReturnDto>(Order);
        }


        private static OrderItem CreateOrderItem(DomainLayer.Models.BasketModule.BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                product = new ProductItemOrdered() { PictureUrl = Product.PictureUrl, ProductId = Product.Id, ProductName = Product.Name },
                price = Product.Price,
                quantity = item.Quantity,
            };
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecification(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var Spec = new OrderSpecification(id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return _mapper.Map<Order,OrderToReturnDto>(Order!);


        }

        public async Task<IEnumerable<DeliveryMetodDto>> GetAllDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMetodDto>>(DeliveryMethods);
        }
    }
}
