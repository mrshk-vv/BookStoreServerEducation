using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Store.DataAccess.Entities;
using Store.DataAccess.Initialize;

namespace Store.DataAccess.AppContext
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public virtual DbSet<PrintingEdition> PrintingEditions { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
        {
            public ApplicationContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=StoreDB;Trusted_Connection=true"
                    );

                return new ApplicationContext(optionsBuilder.Options);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.DataInitializer();
            base.OnModelCreating(modelBuilder);
        }


    }
}
