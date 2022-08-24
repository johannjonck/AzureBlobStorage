using Domain.Entities;
using Domain.Entities.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistance
{
    public interface IDbContext : IDisposable
    {
        public DbSet<Document> Document { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }

        DatabaseFacade Database { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        EntityEntry Add(object entity);

        EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    }
}
