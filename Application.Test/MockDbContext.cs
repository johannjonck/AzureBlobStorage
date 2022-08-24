using Microsoft.EntityFrameworkCore;
using Persistance;
using System;

namespace Application.Tests.Handlers
{
    public class MockDbContext : DataContext
    {
        private static DbContextOptions<DataContext> options;

        public MockDbContext()
            : base(options)
        {
        }

        public static void InitializeDatabase()
        {
            options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }
    }
}
