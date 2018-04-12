using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static LibraryAppMVC.Models.Models;
using DatabaseConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DatabaseConnect.Entities;

namespace LibraryAppMVC.Controllers
{
    [Route("search")]
    public class SearchController : Controller  // Still under construction
    {
        private Context _ctx;
        private IConfiguration _cfg;
        private readonly ILogger _logger;
        public SearchController(Context context, IConfiguration config)//, ILogger<SearchController> logger)
        {
            _ctx = context;
            _cfg = config;
            //_logger = logger;
        }
        /*  Still being worked on
        class SearchResult
        {
            private Context _ctx;
            public SearchResult(Context context, SearchRequest request)
            {
                if (request == null) { throw new Exception(); } // Null request received
                if (request.Author == null && request.Title == null && request.Category == null && request.BookID == 0) { throw new Exception(); } //"You need to specify at least one category"
                _ctx = context;
                var test = _ctx.Books.Where(b => true);
            }
            
        }
        */
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Search([FromQuery] SearchRequest request)
        {
            
            
            var Books = await _ctx.Books
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .ToListAsync();
            var result = new List<Book>();
            if(request.BookID != 0)
            {
                var q = Books
                    .Where(b => b.BookID == request.BookID);
                result = result.Union(q).ToList();
            }

            if(request.Title != null)
            {
                var q = Books
                    .Where(b => b.Title == request.Title);
                result = result.Union(q).ToList();
            }

            if (request.Author != null)
            {
                var q = Books
                    .Where(b => b.AuthorBooks
                        .Any(ab => ab.Author
                            .Name
                            .Contains(
                            request.Author)));
                result = result.Union(q).ToList();
            }
            // TODO Categories
            foreach (Book b in result)
            {
                List<String> AuthorList = new List<String>();
                foreach (AuthorBook ab in b.AuthorBooks)
                {
                    AuthorList.Add(ab.Author.Name);
                }
                b.Authors = AuthorList;
                b.AuthorBooks = null;
            }
            return Json(result);
        }
    }
}