using Azure.Core;
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
    public sealed class ProductService(ApplicationDbContext dbContext)
    {
        public  async Task CreateProduct(ProductCreateDTO request, CancellationToken cancellationToken)
        {
            bool isNameExist = await dbContext.Products.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (isNameExist) 
            {
                throw new AggregateException ("Bu Kayıt Zaten Mevcut..");
            }


            Product product = new()
            {
                Name = request.Name,
                UnitPrice = request.UnitPrice,
                CategoryId = request.CategoryId 
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();    
        }

        public async Task<Product> GetAsync(Guid id,CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products.FindAsync(id, cancellationToken);

            if (product == null) 
            {
                throw new AggregateException("Veri bulunamadı.");
            }
            return product;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken) 
        {
            var products = await dbContext.Products
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);
            return products;
        }

        public async Task UpdateAsync(ProductUpdateDTO request, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ArgumentException("Veri bulunamadı.");
            }
            if (request.Name != product.Name)
            {
                bool isNameExit = await dbContext.Products
                    .AnyAsync(p => p.Name == request.Name, cancellationToken);
                if (isNameExit)
                {
                    throw new ArgumentException("Bu ad daha önce kullanılmış");
                }
                product.Name = request.Name;
                product.UnitPrice = request.UnitPrice;
                product.CategoryId = request.CategoryId;
                dbContext.Products.Update(product);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Guid Id, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(Id, cancellationToken);
            if (product == null)
            {
                throw new ArgumentException("Veri bulunamadı.");
            }
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
