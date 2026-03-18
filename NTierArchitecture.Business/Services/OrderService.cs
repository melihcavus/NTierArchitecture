using Mapster;
using Microsoft.EntityFrameworkCore;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace NTierArchitecture.Business.Services
{
    public sealed class OrderService(ApplicationDbContext dbContext)
    {
        public async Task<Result<string>> CreateAsync(
            OrderCreateDTO request, CancellationToken cancellationToken)
        {

            Order order = request.Adapt<Order>();   

            dbContext.Orders.Add(order);
            var res = await dbContext.SaveChangesAsync(cancellationToken);
            return "Sipariş başarıyla oluşturuldu.";
        }

        public async Task<Result<Order>> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            Order? order = await dbContext.Orders.FindAsync(id, cancellationToken);
            if (order == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            return order;
        }

        public async Task<Result<List<Order>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var Orders = await dbContext.Orders
                .ToListAsync(cancellationToken);
            return Orders;
        }

        public async Task<Result<string>> UpdateAsync(OrderUpdateDTO request, CancellationToken cancellationToken = default)
        {
            Order? Order = await dbContext.Orders.FindAsync(request.Id, cancellationToken);

            if (Order == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }

           request.Adapt(Order);
            dbContext.Orders.Update(Order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return "Sipariş başarıyla güncellendi.";

        }

        public async Task<Result<string>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Order? Order = await dbContext.Orders.FindAsync(id, cancellationToken);
            if (Order == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            dbContext.Orders.Remove(Order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return "Sipariş başarıyla silindi.";
        }
    }
}
