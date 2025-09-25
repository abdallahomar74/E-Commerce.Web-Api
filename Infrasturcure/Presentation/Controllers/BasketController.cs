using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    public class BasketController(IServiceManger _serviceManger) : ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string Id)
        {
            var Basket = await _serviceManger.BasketService.GetBasketAsync(Id);
            return Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManger.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string Id)
        {
            var Basket = await _serviceManger.BasketService.DeleteBasketAsync(Id);
            return Ok(Basket);
        }
    }
}
