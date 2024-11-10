using ETicaretServer.Domain.Entities;
using ETicaretServer.Domain.Entities.Common;
using ETicaretServer.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretServer.Persistence.Contexts
{
    public class ETicaretAPIDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product>   Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer>  Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoinceFile> InvoinceFiles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Veritabanı üzerinde yapacagımız degısıklıkleri yakalayan property
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in datas)
            {
                _ = entity.State switch // _ = donusun onemlı olmadıgı ıcın kullandım.
                {
                    EntityState.Added => entity.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
