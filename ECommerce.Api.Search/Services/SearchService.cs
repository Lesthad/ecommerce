using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService,
                             IProductsService productsService,
                             ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {

            var ordersResult = await ordersService.GetOrders(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomerAsync(customerId);

            if(ordersResult.IsSuccess)
            {
                foreach(var order in ordersResult.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess 
                            ? productsResult.Products.FirstOrDefault(p => p.Id ==  item.ProductId)?.Name
                            : "Product information is not available";
                    }
                }
                return(true, 
                        new { 
                            Customer = customersResult.Customer,
                            Orders = ordersResult.Orders 
                        });
            }

            return (false, null);
        }
    }
}