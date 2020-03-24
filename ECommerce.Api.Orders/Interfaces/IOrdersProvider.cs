using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetCustomerOrders(int customerId);
         Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrders();
         Task<(bool IsSuccess, Order Order, string ErrorMessage)> GetOrder(int Id);
    }
}