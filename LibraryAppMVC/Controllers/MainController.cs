using DatabaseConnect;
using DatabaseConnect.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryAppMVC.Controllers
{
    [Route("/simple/")]
    public class MainController : Controller
    {
        private Context _ctx;

        public MainController(Context context)
        {
            _ctx = context;
        }

        [Route("books")]
        public IActionResult GetABook(string title)
        {
            List<Book> a;
            if (title != null)
            {
                a = _ctx.Books
                    .Where(b => b.Title.Contains(title))
                    //.Include(book => book.Cover)
                    .Include(book => book.AuthorBooks)
                        .ThenInclude(ab => ab.Author)
                    .ToList();
            }
            else
            {
                a = _ctx.Books
                    //.Include(book => book.Cover)
                    .Include(book => book.AuthorBooks)
                        .ThenInclude(ab => ab.Author)
                    .ToList();
            }
            return Json(a);
        }
    }


    [Route("/dev/")]
    public class DevController : Controller
    {
        private Context _ctx;
        public DevController(Context context)
        {
            _ctx = context;
        }

        [Route("authors")]
        public IActionResult GetAnAuthor()
        {
            var a = _ctx.Authors
                .ToList();
            return (Json(a));
        }

        [Route("authorbooks")]
        public IActionResult GetAuthorBook()
        {
            var a = _ctx.AuthorBook_rel
                .ToList();
            return (Json(a));
        }
    }
}
