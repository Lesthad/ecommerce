using System.Threading.Tasks;
using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider provider;

        public ProductsController(IProductsProvider provider)
        {
            this.provider = provider;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await provider.GetProductsAsync();
            if(result.IsSuccess)
            {
                return Ok(result.Products);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var result = await provider.GetProductsAsync(id);
            if(result.IsSuccess)
            {
                return Ok(result.Product);
            }

            return NotFound();
        }
    }
}