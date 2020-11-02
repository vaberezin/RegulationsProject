using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Regulations.Models;
using Regulations.Models.DatabaseContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Regulations.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        RegulationContext db;

        public HomeController(RegulationContext context)
        {
            db = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var RegList = await db.Regulations.ToListAsync();
            return View(RegList);
        }

        public void GetHeaders()
        {
            string table = "";
            foreach (var header in Request.Headers)
            {
                table += $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>";
            }
            Response.WriteAsync($"<table>{table}</table>");
        }

    }
}



