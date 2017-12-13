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

        public DbSet<Book> Books { get; set; }
    }
}
