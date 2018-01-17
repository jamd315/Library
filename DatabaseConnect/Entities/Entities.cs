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
        public ICollection<Book> Books { get; set; }
    }

    [Table("tblBook")]
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public ICollection<Author> Authors { get; set; }
    }

    public class AuthorBook
    {

    }
}
