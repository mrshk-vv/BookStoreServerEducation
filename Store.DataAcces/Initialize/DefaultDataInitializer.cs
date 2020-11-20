using System;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.Shared.Enums;

namespace Store.DataAccess.Initialize
{
    public static class DefaultDataInitializer
    {
        public static void DataInitializer(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PrintingEdition>()
                .Property(e => e.EditionType)
                .HasConversion(
                    v => v.ToString(),
                    v => (Enums.Edition)Enum.Parse(typeof(Enums.Edition), v));

            modelBuilder
                .Entity<PrintingEdition>()
                .Property(e => e.EditionCurrency)
                .HasConversion(
                    v => v.ToString(),
                    v => (Enums.Currency)Enum.Parse(typeof(Enums.Currency), v));

            modelBuilder
                .Entity<Order>()
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Enums.Status)Enum.Parse(typeof(Enums.Status), v));

            modelBuilder
                .Entity<OrderItem>()
                .Property(e => e.Currency)
                .HasConversion(
                    v => v.ToString(),
                    v => (Enums.Currency)Enum.Parse(typeof(Enums.Currency), v));

            modelBuilder.Entity<AuthorInPrintingEdition>()
                .HasKey(table => new { table.AuthorId, table.PrintingEditionId });

            modelBuilder.Entity<AuthorInPrintingEdition>()
                .HasOne(p => p.PrintingEdition)
                .WithMany(pe => pe.AuthorInPrintingEditions)
                .HasForeignKey(ap => ap.PrintingEditionId);

            modelBuilder.Entity<AuthorInPrintingEdition>()
                .HasOne(ap => ap.Author)
                .WithMany(author => author.AuthorInPrintingEditions)
                .HasForeignKey(ap => ap.AuthorId);

            //Initialize default data
            modelBuilder.Entity<Author>()
                .HasData(new Author
                {
                    Id = 1,
                    Name = "Mark Twain"
                });

            modelBuilder.Entity<PrintingEdition>()
                .HasData(new PrintingEdition
                {
                    Id = 1,
                    Title = "The Adventures of Tom Sawyer",
                    Description = "This Description",
                    Price = 100,
                    EditionCurrency = Enums.Currency.USD,
                    IsRemoved = false,
                    EditionType = Enums.Edition.Book
                });

            modelBuilder.Entity<AuthorInPrintingEdition>()
                .HasData(new AuthorInPrintingEdition
                {
                    AuthorId = 1,
                    PrintingEditionId = 1,
                    Date = DateTime.Now
                });
        }
    }
}
