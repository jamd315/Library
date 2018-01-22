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
        public List<Author> Authors { get; set; }
    }

    [Table("tblAuthorBook")]
    public class AuthorBook
    {
        public Author Author { get; set; }
        public int AuthorID { get; set; }
        public Book Book { get; set; }
        public int BookID { get; set; }
    }

    [Table("tblCovers")]
    public class Cover
    {
        [Key]
        public int CoverID { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public String Base64Encoded { get; set; }
    }
}
