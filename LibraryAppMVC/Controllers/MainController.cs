using DatabaseConnect;
using DatabaseConnect.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibraryAppMVC.Controllers
{
    [Route("/login")]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private Context _ctx;
        private readonly ILogger _logger;

        public LoginController(IConfiguration config, Context context, ILogger<LoginController> logger)
        {
            _config = config;
            _ctx = context;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if(user!=null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        private String BuildToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(LoginModel login)
        {
            User User;
            UserModel usermodel = null;
            try
            {
                User =_ctx.Users
                    .Where(u => u.SchoolID.Equals(login.Username))
                    .First();
                
            }
            catch
            {
                return null;
            }
            if(VerifyPass(login.Password, User.Salt, User.PasswordHash))
            {
                usermodel = new UserModel { Name = User.FullName };
            }
            return usermodel;
        }

        private Boolean VerifyPass(String RawPass, String Salt, String PasswordHash)
        {
            byte[] salt_array = Convert.FromBase64String(Salt);
            String hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: RawPass,
                salt: salt_array,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed.Equals(PasswordHash);
        }

        public class LoginModel
        {
            public String Username { get; set; }
            public String Password { get; set; }
        }

        public class UserModel  // Maybe get user from entities instead
        {
            public String Name { get; set; }
        }


    }


    [Route("/library/")]
    public class CheckoutController : Controller
    {
        private Context _ctx;

        public CheckoutController(Context context)
        {
            _ctx = context;
        }

        public class TransactionRequest
        {
            public int BookID { get; set; }
            public int UserID { get; set; }
        }

        [Route("checkout")]
        [HttpPost]
        [Authorize]
        public IActionResult BookCheckout([FromBody]TransactionRequest request)
        {
            int limit = _ctx.UserUType_rel // Get max checked out books for usertype
                .Where(ut => ut.UserID == request.UserID)
                .Include(ut => ut.UType)
                .First()
                .UType
                .CheckoutLimit;

            int current = _ctx.Checkouts // Get current user checked out books
                .Where(c => c.Active)
                .Where(c => c.UserID == request.UserID)
                .Count();

            if (current >= limit)  // Check to see if user can checkout more books
            {
                return Forbid($"You already have checked out {current} books, as many as you can.");
            }

            bool CheckedOut = _ctx.Checkouts
                .Where(c => c.Active && c.BookID.Equals(request.BookID))
                .Count() > 0;

            if (CheckedOut)
            {
                return Forbid("Already checked out");
            }


            _ctx.Checkouts
                .Add(new Checkout { BookID = request.BookID, UserID = request.UserID, Active=true });
            _ctx.SaveChanges();
            return Ok();
        }

        [Route("checkin")]
        [HttpPost]
        [Authorize]
        public IActionResult BookCheckin([FromBody]TransactionRequest request)
        {
            _ctx.Checkouts
                .Where(c => c.BookID == request.BookID && c.UserID == request.UserID)
                .Last()
                .Active = false;
            _ctx.SaveChanges();
            return Ok();
        }

        [Route("reserve")]
        [HttpPost]
        [Authorize]
        public IActionResult ReserveBook([FromBody]TransactionRequest request)
        {
            _ctx.Reservations
                .Add(new Reservation { BookID = request.BookID, UserID = request.UserID, datetime = DateTime.Now, Active = true});
            _ctx.SaveChanges();
            return Ok();
        }

        [Route("fill_reservation")]
        [HttpPost]
        [Authorize]
        public IActionResult FillReservation([FromBody]TransactionRequest request)
        {
            _ctx.Reservations
                .Where(r => r.Active && r.BookID.Equals(request.BookID) && r.UserID.Equals(request.UserID))
                .OrderByDescending(r => r.datetime)
                .First()
                .Active = false;
            _ctx.SaveChanges();
            return Ok();
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

        [AllowAnonymous]
        [Route("books")]
        public IActionResult GetABook(string title, int page = 1)
        {
            List<Book> a;
            if (title != null)  // Title specified
            {
                a = _ctx.Books
                    .Where(b => b.Title.Contains(title))
                    .Include(book => book.Cover)
                    .Include(book => book.AuthorBooks)
                        .ThenInclude(ab => ab.Author)
                    .ToList();
            }
            else  // Title not specified
            {
                if(page<1) { page = 1; }
                int pos_i = (page-1) * 10;
                int pos_f = page * 10;
                int count = _ctx.Books.Count();
                if(pos_f>count) { pos_f = count; }
                if(pos_i>count) { a = new List<Book>(); }
                else
                {
                a = _ctx.Books
                    .Include(book => book.Cover)
                    .Include(book => book.AuthorBooks)
                        .ThenInclude(ab => ab.Author)
                    .ToList()
                    .GetRange(pos_i, (pos_f-pos_i));
                }
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

        [Route("authors")]  // Marked for removal, dummy data for testing at this point
        public IActionResult GetAnAuthor()
        {
            var a = _ctx.Authors
                .ToList();
            return (Json(a));
        }
        

        [Route("adduser")]
        public IActionResult AddUser([FromBody]User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: user.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            user.Salt = Convert.ToBase64String(salt);
            user.PasswordHash = hashed;
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return Ok();
        }
    }
}
