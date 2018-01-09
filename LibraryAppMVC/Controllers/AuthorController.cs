using DatabaseConnect;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppMVC.Controllers
{
    [Route("/simple/")]
    public class AuthorController : Controller
    {
        private Context _ctx;
        public AuthorController(Context context)
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
    }
}
