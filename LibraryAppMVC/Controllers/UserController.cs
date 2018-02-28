using DatabaseConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static LibraryAppMVC.Models.Models;
using DatabaseConnect.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LibraryAppMVC.Controllers
{
    [Route("/user/")] // All endpoints checked 2/25/18, logout not working but not important (token dumped client side at logout)
    public class UserController : Controller
    {
        private IConfiguration _config;
        private Context _ctx;

        public UserController(IConfiguration config, Context context)
        {
            _config = config;
            _ctx = context;
        }


        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login) // Checked 2/24/18 working
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                response = BuildToken(user);
            }
            return response;
        }

        [Route("logout")]
        [Authorize]
        [HttpPost]
        public IActionResult Logout() // Checked 2/24/18 NOT working TODO, maybe not important because client dumps token on logout
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            int userID = _ctx.Users
                .Single(u => u.SchoolID == schoolID)
                .UserID;
            _ctx.Users
                .Single(u => u.UserID == userID);
            _ctx.SaveChanges();
            return Ok();
        }

        [Route("info")]
        [Authorize]
        [HttpGet]
        public IActionResult UserInfo()
        {
            string schoolID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = _ctx.Users
                .Single(u => u.SchoolID == schoolID);
            int userID = user.UserID;

            var checkouts = _ctx.Checkouts
                .Where(c => c.Active)
                .Where(c => c.UserID == userID)
                .Include(c => c.Book)
                .ToList();

            var reservations = _ctx.Reservations
                .Where(r => r.Active)
                .Where(r => r.UserID == userID)
                .Include(r => r.Book)
                .ToList();

            foreach (Checkout c in checkouts)
            {
                c.User = null;
            }
            foreach (Reservation r in reservations)
            {
                r.User = null;
            }
            user.PasswordHash = null;
            user.Salt = null;
            var resp = new { checkouts, reservations, user };
            return Json(resp);
        }

        private IActionResult BuildToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.StudentID),
                new Claim(JwtRegisteredClaimNames.Jti, user.TokenVersion.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["LoginDurationMinutes"])),
                signingCredentials: creds,
                claims: claims
                );
            return Ok(
                new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
        }

        private UserModel Authenticate(LoginModel login)
        {
            User User;
            UserModel usermodel = null;
            try
            {
                User = _ctx.Users
                    .Single(u => u.SchoolID.Equals(login.Username));
            }
            catch
            {
                return null;  // No user found with specified school ID
            }
            if (VerifyPass(login.Password, User.Salt, User.PasswordHash))
            {
                usermodel = new UserModel { Name = User.FullName, StudentID = User.SchoolID, TokenVersion = User.TokenVersion };
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
    }
}
