using DatabaseConnect;
using DatabaseConnect.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static LibraryAppMVC.Models.Models;

namespace LibraryAppMVC.Controllers
{
    [Route("library")] // All endpoints checked 2/25/18
    public class LibraryController : Controller
    {
        private Context _ctx;
        private IConfiguration _cfg;

        public LibraryController(Context context, IConfiguration config)
        {
            _ctx = context;
            _cfg = config;
        }


        [Route("checkout")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ReturnModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 409)]
        [ProducesResponseType(typeof(string), 409)]
        public IActionResult BookCheckout([FromBody]TransactionRequest request) // Checked 2/24/18 working
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userID = _ctx.Users
                .Single(u => u.SchoolID == schoolID)
                .UserID;

            if (!_ctx.Books.Any(b => b.BookID == request.BookID))
            {
                return StatusCode(404, "Book does not exist");
            }

            int limit = _ctx.UserUType_rel // Get max checked out books for usertype
                .Include(ut => ut.UType)
                .Single(ut => ut.UserID == userID)
                .UType
                .CheckoutLimit;

            int current = _ctx.Checkouts // Get current user checked out books
                .Where(c => c.Active)
                .Where(c => c.UserID == userID)
                .Count();

            if (current >= limit)  // Check to see if user can checkout more books
            {
                return StatusCode(409, $"You already have checked out {current} books, as many as you can.");
            }

            bool CheckedOut = _ctx.Checkouts
                .Where(c => c.Active && c.BookID.Equals(request.BookID))
                .Count() > 0;

            if (CheckedOut)
            {
                return StatusCode(409, "Already checked out");
            }
            Checkout checkout = new Checkout { BookID = request.BookID, UserID = userID, Active = true, CheckoutDate = DateTime.Now, DueDate = DateTime.Now.AddDays(Int32.Parse(_cfg["CheckoutLengthDays"])) };
            _ctx.Checkouts
                .Add(checkout);
            _ctx.SaveChanges();
            return Json(new ReturnModel() { Msg = "Checked Out" });
        }

        [Route("checkin")]
        [HttpPost]
        [Authorize]
        public IActionResult BookCheckin([FromBody]TransactionRequest request) // Checked 2/24/18 working
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userID = _ctx.Users
                .Single(u => u.SchoolID == schoolID)
                .UserID;

            if (!_ctx.Books.Any(b => b.BookID == request.BookID))
            {
                return StatusCode(404, "Book does not exist");
            }

            _ctx.Checkouts
                .Where(c => c.BookID == request.BookID && c.UserID == userID)
                .Last()
                .Active = false;
            _ctx.SaveChanges();
            return Json(new ReturnModel() { Msg = "Checked In" });
        }

        [Route("reserve")]
        [HttpPost]
        [Authorize]
        public IActionResult ReserveBook([FromBody]TransactionRequest request) // Checked 2/25/18 working
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userID = _ctx.Users
                .Single(u => u.SchoolID == schoolID)
                .UserID;

            if (!_ctx.Books.Any(b => b.BookID == request.BookID))
            {
                return StatusCode(404, "Book does not exist");
            }

            Boolean UserAlreadyReserved = _ctx.Reservations
                .Where(r => r.Active && r.UserID == userID)
                .Count() > 0;

            if (!UserAlreadyReserved)
            {
                _ctx.Reservations
                    .Add(new Reservation { BookID = request.BookID, UserID = userID, Datetime = DateTime.Now.ToUniversalTime(), Active = true });
                _ctx.SaveChanges();
                return Json(new ReturnModel() { Msg = "Reserved" });
            }
            else if (UserAlreadyReserved)
            {
                return StatusCode(409, "You have already reserved this book");
            }
            return StatusCode(500);  // Should be unreachable

        }

        [Route("fill_reservation")]
        [HttpPost]
        [Authorize]
        public IActionResult FillReservation([FromBody]TransactionRequest request) // Checked 2/25/18 working
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userID = _ctx.Users
                .Single(u => u.SchoolID == schoolID)
                .UserID;

            if (!_ctx.Books.Any(b => b.BookID == request.BookID))
            {
                return StatusCode(404, "Book does not exist");
            }

            Boolean CheckedOut = _ctx.Checkouts
                .Any(c => c.BookID == request.BookID && c.UserID == userID && c.Active == true);

            if (!CheckedOut)
            {
                IActionResult resp = BookCheckout(request);
                _ctx.Reservations
                    .Where(r => r.Active && r.BookID.Equals(request.BookID) && r.UserID.Equals(userID))
                    .OrderByDescending(r => r.Datetime)
                    .First()
                    .Active = false;
                _ctx.SaveChanges();
                return resp;
            }
            else
            {
                return StatusCode(410, "Book already checked out");
            }
        }

        [Route("renew")]
        [HttpPost]
        [Authorize]
        public IActionResult RenewBook([FromBody]TransactionRequest request) // Checked 2/25/18 working
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userID = _ctx.Users
                .Single(u => u.SchoolID == schoolID)
                .UserID;

            bool AlreadyReserved = _ctx.Reservations
                .Where(r => r.Active && r.BookID.Equals(request.BookID))
                .Count() > 0;
            bool OverRenewals = _ctx.Checkouts
                .Where(c => c.Active && c.BookID.Equals(request.BookID) && c.UserID.Equals(userID))
                .OrderByDescending(c => c.CheckoutDate)
                .First()
                .Renewals > 2;
            if (AlreadyReserved || OverRenewals)
            {
                return StatusCode(409);
            }
            DateTime Checkout = _ctx.Checkouts
                .Where(c => c.Active && c.BookID.Equals(request.BookID) && c.UserID.Equals(userID))
                .OrderByDescending(c => c.CheckoutDate)
                .First()
                .CheckoutDate;
            _ctx.Checkouts
                .Where(c => c.Active && c.BookID.Equals(request.BookID) && c.UserID.Equals(userID))
                .OrderByDescending(c => c.CheckoutDate)
                .First()
                .CheckoutDate = Checkout.AddDays(7);
            _ctx.Checkouts
                .Where(c => c.Active && c.BookID.Equals(request.BookID) && c.UserID.Equals(userID))
                .OrderByDescending(c => c.CheckoutDate)
                .First()
                .Renewals += 1;
            _ctx.SaveChanges();
            return Json(new ReturnModel() { Msg = "Book renewed" });
        }
    }
}
