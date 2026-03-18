using NTierArchitecture.Entities.DTOs;
using NTierArchitecture.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using NTierArchitecture.Entities.Models;
using TS.Result;
using Mapster;
using Microsoft.Extensions.Caching.Memory;

namespace NTierArchitecture.Business.Services
{
    public sealed class CategoryService(
        ApplicationDbContext dbContext,
        IMemoryCache memoryCache)
    {
        public async Task<Result<string>> CreateAsync(
            CategoryCreateDTO request,CancellationToken cancellationToken)
        {
            bool isNameExist = await dbContext.Categories.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if(isNameExist)
            {
                throw new ArgumentException("Bu ad daha önce kullanılmış");
            }

            Category category = request.Adapt<Category>();

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);
            memoryCache.Remove("categories");


            return "Kategori başarıyla oluşturuldu";
        }

        public async Task<Result<Category>> GetAsync(Guid id,CancellationToken cancellationToken)
        {
            Category? category = await dbContext.Categories.FindAsync(id, cancellationToken);
            if (category == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            return category;
        }

        //HttpGet neden kullanmadık?
        public async Task<Result<List<Category>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = memoryCache.Get<List<Category>>("categories");
            if (categories is null)
            {
             categories = await dbContext.Categories
                .OrderBy(p=> p.Name)
                .ToListAsync(cancellationToken);
                memoryCache.Set("categories", categories);
            }
            return categories;

        }

        public async Task<Result<string>>UpdateAsync(CategoryUpdateDTO request,CancellationToken cancellationToken = default)
        {
            Category? category = await dbContext.Categories.FindAsync(request.Id,cancellationToken);

            if (category == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }

            if(request.Name != category.Name)
            {
                bool isNameExsist = await dbContext.Categories
                    .AnyAsync(p =>p.Name == request.Name, cancellationToken);
                if (isNameExsist)
                {
                    throw new ArgumentException("Bu ad daha önce kullanılmış");
                }
                request.Adapt(category);
                dbContext.Categories.Update(category);
                await dbContext.SaveChangesAsync(cancellationToken);
                memoryCache.Remove("categories");

            }
            return "Kategori başarıyla güncellendi";
        }

        public async Task<Result<string>> DeleteAsync(Guid id,CancellationToken cancellationToken = default)
        {
            Category? category = await dbContext.Categories.FindAsync(id, cancellationToken);
            if (category == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync(cancellationToken);
            memoryCache.Remove("categories");
            return "Kategori başarıyla silindi";
        }
    }
}
