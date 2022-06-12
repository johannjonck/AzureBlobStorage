using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistance
{
    public interface IDbContext : IDisposable
    {
        public DbSet<Document> Document { get; set; }

        DatabaseFacade Database { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        EntityEntry Add(object entity);

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    }
}
