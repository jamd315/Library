using DatabaseConnect;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppMVC.Controllers
{
    [Route("/other/")]
    public class BookController : Controller
    {
        private Context _ctx;
        public BookController(Context context)
        {
            _ctx = context;
        }

        [Route("thing")]
        public IActionResult GetABook()
        {
            var a = _ctx.Books
                    .Where(b => b.PageCount > 10)
                    .Select(b => new { title = b.Title }) //Anonymous object
                    .ToList();
            return Json(a);
        }
    }
}
