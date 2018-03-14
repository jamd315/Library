using DatabaseConnect;
using DatabaseConnect.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppMVC.Controllers
{
    [Route("simple")] // All endpoints checked 2/25/18
    public class SimpleController : Controller
    {
        private Context _ctx;
        private IConfiguration _cfg;
        public SimpleController(Context context, IConfiguration config)
        {
            _ctx = context;
            _cfg = config;
        }

        [Route("books")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetABook(int page = 1) // Checked 2/25/18 working
        {
            int PerPageCount = Int32.Parse(_cfg["PerPageCount"]);
            var BookQuery = _ctx.Books
                        .Include(book => book.AuthorBooks)
                            .ThenInclude(ab => ab.Author)
                        .ToList();
            int PageCount = Int32.Parse(_cfg["PerPageCount"]);
            var results = new List<Book>();
            if (Boolean.Parse(_cfg["DoPages"]))
            {
                results = BookQuery.Skip(page * PageCount).Take(PageCount).ToList();
            }
            else
            {
                results = BookQuery;
            }

            foreach (Book b in results)
            {
                List<String> AuthorList = new List<String>();
                foreach (AuthorBook ab in b.AuthorBooks)
                {
                    AuthorList.Add(ab.Author.Name);
                }
                b.Authors = AuthorList;
                b.AuthorBooks = null;
            }
            return Json(results);
        }

        [Route("checkouts")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCheckouts() // Checked 2/25/18 working
        {
            var CheckoutList = _ctx.Checkouts
                .Include(c => c.Book)
                .Where(c => c.Active)
                .ToList();
            return Json(CheckoutList);
        }

        [Route("reservations")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetReservations() // Checked 2/25/18 working
        {
            var CheckoutList = _ctx.Reservations
                .Include(r => r.Book)
                .Where(r => r.Active)
                .ToList();
            return Json(CheckoutList);
        }
    }
}
