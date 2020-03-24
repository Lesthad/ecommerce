using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            
            SeedData();
        }

        public void SeedData()
        {
            if(!this.dbContext.Orders.Any())
            {
                this.dbContext.Orders.Add(new Db.Order(){ Id = 1, CustomerId = 1, OrderDate= DateTime.UtcNow, Total = 150, Items= new List<Db.OrderItem>(){
                    new Db.OrderItem(){ Id = 1, OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice= 15 }
                } });

                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 1, OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice= 15 });
                
                this.dbContext.Orders.Add(new Db.Order(){ Id = 2, CustomerId = 2, OrderDate= DateTime.UtcNow, Total = 700, Items= new List<Db.OrderItem>(){
                    new Db.OrderItem(){ Id = 2, OrderId = 2, ProductId = 1, Quantity = 10, UnitPrice= 30 },
                    new Db.OrderItem(){ Id = 3, OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice= 40 }
                } });

                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 2, OrderId = 2, ProductId = 1, Quantity = 10, UnitPrice= 30 });
                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 3, OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice= 40 });
                
                this.dbContext.Orders.Add(new Db.Order(){ Id = 3, CustomerId = 3, OrderDate= DateTime.UtcNow, Total = 850, Items= new List<Db.OrderItem>(){
                    new Db.OrderItem(){ Id = 4, OrderId = 3, ProductId = 1, Quantity = 10, UnitPrice= 15 },
                    new Db.OrderItem(){ Id = 5, OrderId = 3, ProductId = 2, Quantity = 10, UnitPrice= 30 },
                    new Db.OrderItem(){ Id = 6, OrderId = 3, ProductId = 3, Quantity = 10, UnitPrice= 40 }
                } });

                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 4, OrderId = 3, ProductId = 1, Quantity = 10, UnitPrice= 15 });
                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 5, OrderId = 3, ProductId = 2, Quantity = 10, UnitPrice= 30 });
                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 6, OrderId = 3, ProductId = 3, Quantity = 10, UnitPrice= 40 });

                this.dbContext.Orders.Add(new Db.Order(){ Id = 4, CustomerId = 4, OrderDate= DateTime.UtcNow, Total = 1350, Items= new List<Db.OrderItem>(){
                    new Db.OrderItem(){ Id = 7, OrderId = 4, ProductId = 1, Quantity = 10, UnitPrice= 15 },
                    new Db.OrderItem(){ Id = 8, OrderId = 4, ProductId = 2, Quantity = 10, UnitPrice= 30 },
                    new Db.OrderItem(){ Id = 9, OrderId = 4, ProductId = 3, Quantity = 10, UnitPrice= 40 },
                    new Db.OrderItem(){ Id = 10, OrderId = 4, ProductId = 4, Quantity = 10, UnitPrice= 50 }
                } });

                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 7, OrderId = 4, ProductId = 1, Quantity = 10, UnitPrice= 15 });
                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 8, OrderId = 4, ProductId = 2, Quantity = 10, UnitPrice= 30 });
                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 9, OrderId = 4, ProductId = 3, Quantity = 10, UnitPrice= 40 });
                //this.dbContext.OrdersItems.Add(new Db.OrderItem(){ Id = 10, OrderId = 4, ProductId = 4, Quantity = 10, UnitPrice= 50 });
                this.dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrder(int Id)
        {
            try
            {
                var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == Id);
                if(order != null)
                {
                    var results = mapper.Map<Db.Order, Models.Order>(order);
                    return(true, results, null);
                }
                return(false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return(false, null, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrders()
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();
                if(orders != null && orders.Any())
                {
                    var results = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return(true, results, null);
                }

                return(false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetCustomerOrders(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Items).ToListAsync();
                if(orders != null)
                {
                    var results = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return(true, results, null);
                }
                return(false, null, "Not found");
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                return(false, null, ex.Message);
            }
        }
    }
}