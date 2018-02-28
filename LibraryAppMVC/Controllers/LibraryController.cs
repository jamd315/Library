using DatabaseConnect;
using DatabaseConnect.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static LibraryAppMVC.Models.Models;

namespace LibraryAppMVC.Controllers
{
    [Route("/library/")] // All endpoints checked 2/25/18
    public class LibraryController : Controller
    {
        private Context _ctx;

        public LibraryController(Context context)
        {
            _ctx = context;
        }


        [Route("checkout")]
        [HttpPost]
        [Authorize]
        public IActionResult BookCheckout([FromBody]TransactionRequest request) // Checked 2/24/18 working
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userID = _ctx.Users
                .Single(u => u.SchoolID == schoolID)
                .UserID;

            if (!_ctx.Books.Any(b => b.BookID == request.BookID))
            {
                return StatusCode(409, "Book does not exist");
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

            _ctx.Checkouts
                .Add(new Checkout { BookID = request.BookID, UserID = userID, Active = true, CheckoutDate = DateTime.Now, DueDate = DateTime.Now.AddDays(14) });
            _ctx.SaveChanges();
            return Ok();
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
                return StatusCode(409, "Book does not exist");
            }

            _ctx.Checkouts
                .Where(c => c.BookID == request.BookID && c.UserID == userID)
                .Last()
                .Active = false;
            _ctx.SaveChanges();
            return Ok();
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
                return StatusCode(409, "Book does not exist");
            }

            Boolean BookAvailable = _ctx.Checkouts
                .Where(c => c.BookID == request.BookID && c.Active)
                .Count() > 0;

            Boolean UserAlreadyReserved = _ctx.Reservations
                .Where(r => r.Active && r.UserID == userID)
                .Count() > 0;

            if (!UserAlreadyReserved || !BookAvailable)
            {
                _ctx.Reservations
                    .Add(new Reservation { BookID = request.BookID, UserID = userID, Datetime = DateTime.Now, Active = true });
                _ctx.SaveChanges();
                return Ok();
            }
            else if (UserAlreadyReserved)
            {
                return StatusCode(409, "You have already reserved this book");
            }
            else if (BookAvailable)
            {
                return StatusCode(409, "This book can be checked out now, not reserved");
            }
            return StatusCode(500);

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
                return StatusCode(409, "Book does not exist");
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
                return StatusCode(409, "Book already checked out");
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
                return Forbid();
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
            return Ok();
        }
    }
}
