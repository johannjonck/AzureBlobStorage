using Domain.Entities;
using Domain.Entities.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistance
{
    public class DataContext : DbContext, IDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //NOTE: comment this out when running the EF database update command
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        //NOTE: Not used for in memory database. comment this out when running the EF database update command
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //"Server=(localdb)\\MSSQLLocalDB;Database=QBlobStorageDB"
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=QBlobStorageDB");
        //    //optionsBuilder.UseSqlServer(@"Server = ******.database.windows.net; Database = {DbName}; User Id = ****; Password = ****");
        //}

        public DbSet<Document> Document { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeGroup> EmployeeGroup { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }

        DatabaseFacade IDbContext.Database
        {
            get
            {
                return base.Database;
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }

    }
}