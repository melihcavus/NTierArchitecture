using Microsoft.EntityFrameworkCore;
using NTierArchitecture.Entities.Abstractions;
using NTierArchitecture.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierArchitecture.DataAccess.Context
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base (options)
        {
                
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AbstractEntity>();
            foreach(var entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreatedAt)
                        .CurrentValue = DateTimeOffset.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(p => p.UpdatedAt)
                        .CurrentValue = DateTimeOffset.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                    {
                    if (!entry.Property(p => p.IsDeleted).CurrentValue)
                    {
                        entry.Property(p => p.IsDeleted)
                           .CurrentValue = true;
                        entry.Property(p => p.DeletedAt)
                            .CurrentValue = DateTimeOffset.UtcNow;
                        entry.State = EntityState.Modified;
                    }   
                }
                   

            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
