﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class SearchController : Controller
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
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Search([FromQuery] SearchRequest request)
        {
            if(request == null) { return BadRequest(); }
            if(request.Author == null && request.Title == null && request.Category == null && request.BookID == 0) { return BadRequest("You need to specify at least one category"); }
            
            var Books = await _ctx.Books.ToListAsync();
            if(request.BookID != 0)
            {
                try
                {
                    return Json(_ctx.Books.Single(b => b.BookID == request.BookID));  // Needs to be a new request to trigger the try-catch if it fails to find a book
                }
                catch
                {
                    return BadRequest($"Error when searching for BookID {request.BookID}");
                }
            }
            List<Book> result = new List<Book>(); // Not working TODO
            if(request.Author != null)
            {
                result.Union(
                    Books
                        .Where(b => b.Authors.Contains(request.Author))
                );
            }
            if(request.Title != null)
            {
                result.Union(
                    Books
                        .Where(b => b.Title.Contains(request.Title))
                );
            }
            return Ok(Books.ToList());
        }
    }
}