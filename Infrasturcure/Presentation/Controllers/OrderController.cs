using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrderController(IServiceManger _serviceManger) : ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto)
        {
            var Result = await _serviceManger.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(Result);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrdersAsync()
        {
            var Result = await _serviceManger.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Result);
        }
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdAsync(Guid id)
        {
            var Result = await _serviceManger.OrderService.GetOrderByIdAsync(id);
            return Ok(Result);
        }
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMetodDto>>>  GetDeliveryMethodAsync()
        {
            var Result = await _serviceManger.OrderService.GetAllDeliveryMethodsAsync();
            return Ok(Result);
        }

    }
}
