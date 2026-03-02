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

namespace NTierArchitecture.Business.Services
{
    public sealed class CategoryService(ApplicationDbContext dbContext)
    {
        public async Task CreateAsync(
            CategoryCreateDTO request,CancellationToken cancellationToken)
        {
            bool isNameExist = await dbContext.Categories.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if(isNameExist)
            {
                throw new ArgumentException("Bu ad daha önce kullanılmış");
            }

            Category category = new()
            {
                Name = request.Name
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Category> GetAsync(Guid id,CancellationToken cancellationToken)
        {
            Category? category = await dbContext.Categories.FindAsync(id, cancellationToken);
            if (category == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            return category;
        }

        //HttpGet neden kullanmadık?
        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = await dbContext.Categories
                .OrderBy(p=> p.Name)
                .ToListAsync(cancellationToken);
            return categories;
        }

        public async Task UpdateAsync(CategoryUpdateDTO request,CancellationToken cancellationToken = default)
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
                category.Name = request.Name;
                dbContext.Categories.Update(category);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Guid id,CancellationToken cancellationToken = default)
        {
            Category? category = await dbContext.Categories.FindAsync(id, cancellationToken);
            if (category == null)
            {
                throw new ArgumentException("Veri bulunamadı");
            }
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
