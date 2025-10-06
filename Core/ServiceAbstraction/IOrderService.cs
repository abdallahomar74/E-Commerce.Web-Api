using Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto,string email);
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);
        Task<IEnumerable<DeliveryMetodDto>> GetAllDeliveryMethodsAsync();
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
        
    }
}
