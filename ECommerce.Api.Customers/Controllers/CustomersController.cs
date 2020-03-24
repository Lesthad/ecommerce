using System.Threading.Tasks;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await customersProvider.GetCustomersAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomer(int Id)
        {
            var result = await customersProvider.GetCustomerAsync(Id);
            if(result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound(result.ErrorMessage);
        }


    }
}