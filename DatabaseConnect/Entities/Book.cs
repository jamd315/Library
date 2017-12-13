using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnect.Entities
{
    [Table("tblBook")]
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
    }
}
