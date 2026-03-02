using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.Business.Services
{
    public sealed class OrderService(ApplicationDbContext dbContext)
    {
        public async Task CreateAsync(
            OrderCreateDTO request, CancellationToken cancellationToken)
        {

            Order order = new()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                OrderDate = DateTimeOffset.UtcNow
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Order> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            Order? order = await dbContext.Orders.FindAsync(id, cancellationToken);
            if (order == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            return order;
        }

        public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            var Orders = await dbContext.Orders
                .ToListAsync(cancellationToken);
            return Orders;
        }

        public async Task UpdateAsync(OrderUpdateDTO request, CancellationToken cancellationToken = default)
        {
            Order? Order = await dbContext.Orders.FindAsync(request.Id, cancellationToken);

            if (Order == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }

            Order.ProductId = request.ProductId;
            Order.Quantity = request.Quantity;
            dbContext.Orders.Update(Order);
            await dbContext.SaveChangesAsync(cancellationToken);

        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Order? Order = await dbContext.Orders.FindAsync(id, cancellationToken);
            if (Order == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            dbContext.Orders.Remove(Order);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
