using Azure.Core;
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
    public sealed class ProductService(ApplicationDbContext dbContext)
    {
        public  async Task<Result<string>> CreateProduct(ProductCreateDTO request, CancellationToken cancellationToken)
        {
            bool isNameExist = await dbContext.Products.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (isNameExist) 
            {
                throw new AggregateException ("Bu Kayıt Zaten Mevcut..");
            }


            Product product = request.Adapt<Product>();

            dbContext.Products.Add(product);
            dbContext.SaveChanges();    
            return "Ürün başarıyla oluşturuldu.";
        }

        public async Task<Result<Product>> GetAsync(Guid id,CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products.
                FindAsync(id, cancellationToken);

            if (product == null) 
            {
                throw new AggregateException("Veri bulunamadı.");
            }
            return product;
        }

        public async Task<Result<List<Product>>> GetAllAsync(int pageNumber, int pageSize,CancellationToken cancellationToken) 
        {
            var products = await dbContext.Products
                .OrderBy(p => p.Name)
                .Skip(pageSize * (pageNumber -1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return products;
        }

        public async Task<Result<string>> UpdateAsync(ProductUpdateDTO request, CancellationToken cancellationToken)
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
                request.Adapt(product);
                dbContext.Products.Update(product);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            return "Ürün başarıyla güncellendi.";
        }

        public async Task<Result<string>> DeleteAsync(Guid Id, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products.FindAsync(Id, cancellationToken);
            if (product == null)
            {
                throw new ArgumentException("Veri bulunamadı.");
            }
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync(cancellationToken);
            return "Ürün başarıyla silindi.";
        }
    }
}
