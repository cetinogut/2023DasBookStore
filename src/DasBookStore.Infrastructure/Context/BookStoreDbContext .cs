using DasBookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DasBookStore.Infrastructure.Context
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        //, we added a configuration (in line 24) that will create the size of the column in the database as varchar(150) for the columns of type string that were not mapped. This is good to use in projects which you will have more classes.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(150)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes() //to disable the cascade deletion
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
