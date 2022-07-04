using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistance
{
    public class DataContext : DbContext, IDbContext
    {
        //NOTE: comment this out when running the EF database update command
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        //NOTE: Not used for in memory database. comment this out when running the EF database update command
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer(@"Server = ******.database.windows.net; Database = {DbName}; User Id = ****; Password = ****");
        //}

        public DbSet<Document> Document { get; set; }

        public DbSet<User> User { get; set; }

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