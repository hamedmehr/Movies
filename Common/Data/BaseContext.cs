using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options):base(options) { }

        //Automatically update create date and update date if entity has these properties
        //Also soft delete feature added 
        public override int SaveChanges()
        {
            var now = DateTime.Now;
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    if (entityEntry.Entity.GetType().GetProperty("CreatedAt") != null)
                        entityEntry.Property("CreatedAt").CurrentValue = now;
                }

                if (entityEntry.State == EntityState.Deleted && entityEntry.Entity.GetType().GetProperty("IsDeleted") != null)
                {
                    entityEntry.Property("IsDeleted").CurrentValue = true;
                    entityEntry.State = EntityState.Modified;
                }

                if (entityEntry.Entity.GetType().GetProperty("UpdatedAt") != null)
                    entityEntry.Property("UpdatedAt").CurrentValue = now;

            }
            return base.SaveChanges();
        }

        //Automatically update create date and update date if entity has these properties
        //Also soft delete feature added 
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    if (entityEntry.Entity.GetType().GetProperty("CreatedAt") != null)
                        entityEntry.Property("CreatedAt").CurrentValue = now;
                }

                if (entityEntry.State == EntityState.Deleted && entityEntry.Entity.GetType().GetProperty("IsDeleted") != null)
                {
                    entityEntry.Property("IsDeleted").CurrentValue = true;
                    entityEntry.State = EntityState.Modified;
                }

                if (entityEntry.Entity.GetType().GetProperty("UpdatedAt") != null)
                    entityEntry.Property("UpdatedAt").CurrentValue = now;
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
