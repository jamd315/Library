using DatabaseConnect;
using DatabaseConnect.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static LibraryAppMVC.Models.Models;

namespace LibraryAppMVC.Controllers
{
    [Route("dev")]
    public class DevController : Controller
    {
        private Context _ctx;
        private readonly ILogger _logger;
        private IConfiguration _cfg;
        public DevController(Context context, ILogger<DevController> logger, IConfiguration config)
        {
            _ctx = context;
            _logger = logger;
            _cfg = config;
        }

        [Route("adduser")]
        [HttpPost]
        public IActionResult AddUser([FromBody]NewUser newuser)
        {
            if (newuser.UserTypeInt == 0) { newuser.UserTypeInt = 1; }
            User user = new User() { SchoolID = newuser.Username, Password = newuser.Password };
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
            int UserID = _ctx.Users
                .Single(u => u.SchoolID == user.SchoolID)
                .UserID;
            _ctx.UserUType_rel
                .Add(new UserUType { UserID = UserID, UTypeID = 1 });
            _ctx.SaveChangesAsync();
            return Ok($"Successfully added user with ID {user.SchoolID}");
        }

        [Route("addbook")]
        [HttpPost]
        public IActionResult AddBook([FromBody]NewBook nb)
        {
            Book book = new Book()
            {
                Title = nb.Title,
                PageCount = nb.PageCount,
                Description = nb.Description,
                ImagePath = nb.ImagePath,
                ISBN = nb.ISBN
            };
            if(nb.DeweyDecimal != null && nb.FicID == null)
            {
                book.DeweyDecimal = nb.DeweyDecimal;
            }
            else if(nb.FicID != null && nb.DeweyDecimal == null)
            {
                book.FicID = nb.FicID;
            }
            else if(nb.DeweyDecimal == null && nb.FicID == null)
            {
                return StatusCode(400, "Specify Dewey Decimal or Fiction ID");
            }
            else
            {
                return StatusCode(400, "Can not specify both Dewey Decimal and Fiction ID");
            }
            _ctx.Books.Add(book);
            _ctx.SaveChanges();
            foreach(int authID in nb.AuthorIDs)
            {
                _ctx.AuthorBook_rel.Add(
                    new AuthorBook()
                    {
                        BookID = book.BookID,
                        AuthorID = authID
                    }
                );
            }
            _ctx.SaveChanges();
            return Ok();
        }

        [Route("addauthor")]
        [HttpPost]
        public IActionResult AddAuthor([FromBody]NewAuthor na)
        {
            Author author = new Author()
            {
                Name = na.Name,
                AuthorType = na.AuthorType
            };
            _ctx.Add(author);
            _ctx.SaveChanges();
            return Ok();
        }
    }
}
