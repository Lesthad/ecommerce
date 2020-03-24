using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/orders")]
    public class OrdersControllers : Controller
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersControllers(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        public async Task<IActionResult> GetOrders()
        {
           var results = await this.ordersProvider.GetOrders();
           if(results.IsSuccess)
           {
               return Ok(results.Orders);
           } 

           return NotFound(results.ErrorMessage);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrder(int Id)
        {
           var result = await this.ordersProvider.GetOrder(Id);
           if(result.IsSuccess)
           {
               return Ok(result.Order);
           } 

           return NotFound(result.ErrorMessage);
        }
        
        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomerOrder(int id)
        {
           var results = await this.ordersProvider.GetCustomerOrders(id);
           if(results.IsSuccess)
           {
               return Ok(results.Orders);
           } 

           return NotFound(results.ErrorMessage);
        }
    }
}