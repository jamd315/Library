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
        // Relationships
        public DbSet<AuthorBook> AuthorBook_rel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>()  // Composite Key
            .HasKey(t => new { t.BookID, t.AuthorID });

            modelBuilder.Entity<AuthorBook>()  // One to many AuthorBooks.Book to Book
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookID);

            modelBuilder.Entity<AuthorBook>()  // One to many AuthorBooks.Author to Author
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorID);
        }
    }
}
