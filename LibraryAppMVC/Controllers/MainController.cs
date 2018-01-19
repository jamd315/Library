using DatabaseConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IActionResult GetABook()
        {
            var thing = new List<String>();
            var books = _ctx.Books;
            foreach (var b in books)
            {
                foreach (var ab in b.AuthorBooks)
                {
                    thing.Add(ab.Author.Name);
                }
            }
            return Json(thing);
        }

        [Route("authors")]
        public IActionResult GetAnAuthor()
        {
            var a = _ctx.Authors
                    .ToList();
            return (Json(a));
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
        [Route("authorbooks")]
        public IActionResult GetAuthorBook()
        {
            var a = _ctx.AuthorBook_rel
                    .ToList();
            return (Json(a));
        }
    }
}
