using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseConnect.Entities
{
    class Entities
    {
    }

    [Table("tblAuthor")]
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public IEnumerable<AuthorBook> AuthorBooks { get; set; }
    }

    [Table("tblBook")]
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public Cover Cover { get; set; }
        public IEnumerable<AuthorBook> AuthorBooks { get; set; }
        [NotMapped]
        public IList<String> Authors { get; set; }
        public String ISBN { get; set; }
        public String DeweyDecimal { get; set; }
        public String FicID { get; set; }
        public int CoverID { get; set; }  // Order matters
    }

    [Table("tblAuthorBook")]
    public class AuthorBook
    {
        public Author Author { get; set; }
        public int AuthorID { get; set; }
        public Book Book { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
    }

    [Table("tblCovers")]
    public class Cover
    {
        [Key]
        public int CoverID { get; set; }
        public int BookID { get; set; }
        public List<Book> Books { get; set; }
        public String Base64Encoded { get; set; }
    }

    [Table("tblUsers")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public String FullName { get; set; }
        public String SchoolID { get; set; }
        public String PasswordHash { get; set; }
        public String Salt { get; set; }
        [NotMapped]
        public String Password { get; set; }
        public int TokenVersion { get; set; }
    }

    [Table("tblUType")]
    public class UType
    {
        [Key]
        public int UTypeID { get; set; }
        public String UTypeName { get; set; }
        public Boolean WriteAccess { get; set; }
        public int CheckoutLimit { get; set; }
    }

    [Table("tblUserUType")]
    public class UserUType
    {
        public User User { get; set; }
        public int UserID { get; set; }
        public UType UType { get; set; }
        public int UTypeID { get; set; }
    }

    [Table("tblCheckouts")]
    public class Checkout
    {
        [Key]
        public int ID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public Book Book { get; set; }
        public int BookID { get; set; }
        public Boolean Active { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Renewals { get; set; }
    }

    [Table("tblReservations")]
    public class Reservation
    {
        [Key]
        public int ID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public Book Book { get; set; }
        public int BookID { get; set; }
        public DateTime Datetime { get; set; }
        public Boolean Active { get; set; }
    }
}
