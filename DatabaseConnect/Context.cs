using DatabaseConnect.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnect
{
    public class Context : DbContext
    {
        public Context(DbContextOptions DbCfg) : base(DbCfg)
        {
        }
        // Simple
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UType> UTypes { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        // Relationships
        public DbSet<AuthorBook> AuthorBook_rel { get; set; }
        public DbSet<UserUType> UserUType_rel { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>()  // Composite Key for AuthorBooks
                .HasKey(ab => new { ab.BookID, ab.AuthorID });

            modelBuilder.Entity<AuthorBook>()  // One to many AuthorBooks.Book to Book
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookID);

            modelBuilder.Entity<AuthorBook>()  // One to many AuthorBooks.Author to Author
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorID);

            modelBuilder.Entity<UserUType>()  // Composite Key
                .HasKey(ut => new { ut.UserID, ut.UTypeID });

            modelBuilder.Entity<Book>()
                .Property(a => a.DeweyDecimal)
                .HasDefaultValue("n/a");

            modelBuilder.Entity<Book>()
                .Property(a => a.FicID)
                .HasDefaultValue("n/a");

            modelBuilder.Entity<Book>()
                .Property(a => a.DeweyDecimal)
                .HasDefaultValue("n/a");

            modelBuilder.Entity<AuthorBook>()
                .Property(ab => ab.AuthorID)
                .HasDefaultValue(3);

        }
    }
}
