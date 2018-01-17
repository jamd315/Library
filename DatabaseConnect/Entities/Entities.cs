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
        public IEnumerable<AuthorBook> AuthorBooks { get; set; }
    }

    [Table("tblAuthorBook")]
    public class AuthorBook
    {
        public Author Author { get; set; }
        public int AuthorID { get; set; }
        public Book Book { get; set; }
        public int BookID { get; set; }
    }
}
