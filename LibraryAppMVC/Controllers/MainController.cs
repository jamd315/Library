using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace LibraryAppMVC.Controllers
{   
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RedirectController : Controller
    {
        private IConfiguration _cfg;
        public RedirectController(IConfiguration config)
        {
            _cfg = config;
        }


        [Route("")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RedirectTo()
        {
            if(Boolean.Parse(_cfg["DoAdminHomePage"]))
            {
                return Redirect("admin");
            }
            else if(Boolean.Parse(_cfg["DoSwaggerHomePage"]))
            {
                return Redirect("swagger");
            }
            return Ok("Config misconfigured");
        }
    }
}
