using DatabaseConnect;
using DatabaseConnect.Entities;
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
        public IActionResult GetABook()
        {
            var a = _ctx.Books
                    .ToList();
            return Json(a);
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
