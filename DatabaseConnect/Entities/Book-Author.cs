using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseConnect.Entities
{
    [Table("tblBookAuthor")]
    class Book_Author
    {
        [Key]
        public int ID { get; set; }
        public string Book { get; set; }
        public string Author { get; set; }
    }
}
