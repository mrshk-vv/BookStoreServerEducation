using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.Shared.Enums;

namespace Store.DataAccess.Initialize
{
    public static class DefaultDataInitializer
    {
        public static void DataInitializer(this ModelBuilder modelBuilder)
        {
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
                    Currency = Enums.Currency.USD.ToString(),
                    IsRemoved = false,
                    Type = Enums.Edition.Book.ToString()
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
