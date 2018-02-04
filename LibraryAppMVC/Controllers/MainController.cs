using DatabaseConnect;
using DatabaseConnect.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryAppMVC.Controllers
{
    [Route("/library/")]
    public class CheckoutController : Controller
    {
        private Context _ctx;

        public CheckoutController(Context context)
        {
            _ctx = context;
        }

        [Route("checkout")]
        [HttpPost]
        public IActionResult BookCheckout(string bookid, string userid)
        {
            int B_id, U_id;
            try
            {
                B_id = Int32.Parse(bookid);
                U_id = Int32.Parse(userid);
            }
            catch
            {
                return StatusCode(400); // Catch non-int requests
            }

            int limit = _ctx.UserUType_rel
                .Where(ut => ut.UserID == U_id)
                .Include(ut => ut.UType)
                .First()
                .UType
                .CheckoutLimit;

            int current = _ctx.Checkouts
                .Where(c => c.Active)
                .Where(c => c.UserID == U_id)
                .Count();

            if(current >= limit)
            {
                return StatusCode(403);
            }


            _ctx.Checkouts
                .Add(new Checkout { BookID = B_id, UserID = U_id, Active=true });
            _ctx.SaveChanges();
            return StatusCode(200);
        }

        [Route("checkin")]
        [HttpPost]
        public IActionResult BookCheckin(string bookid, string userid)
        {
            int B_id, U_id;
            try
            {
                B_id = Int32.Parse(bookid);
                U_id = Int32.Parse(userid);
            }
            catch
            {
                return StatusCode(400); // Catch non-int requests
            }
            _ctx.Checkouts
                .Where(c => c.BookID == B_id && c.UserID == U_id)
                .First()  // TODO is this bad?
                .Active = false;
            _ctx.SaveChanges();
            return StatusCode(200);
        }
    }
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
