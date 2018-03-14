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
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static LibraryAppMVC.Models.Models;

namespace LibraryAppMVC.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class SwaggerRedirectController : Controller
    {
        [Route("")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RedirectToSwaggerUi()
        {
            // Put the one you're using on top
            return Redirect("/admin");
            return Redirect("swagger");
        }
    }
}
