﻿using DatabaseConnect;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppMVC.Controllers
{
    [Route("/simple/")]
    public class MainController : Controller
    {
        private Context _ctx;
        public MainController(Context context)
        {
            _ctx = context;
        }


        [Route("books")]
        public IActionResult GetABook()
        {
            var a = _ctx.Books
                    //.Where(b => b.PageCount > 10)
                    //.Select(b => new { title = b.Title }) //Anonymous object
                    .ToList();
            return Json(a);
        }

        [Route("authors")]
        public IActionResult GetAnAuthor()
        {
            var a = _ctx.Authors
                    .ToList();
            return (Json(a));
        }
    }
}