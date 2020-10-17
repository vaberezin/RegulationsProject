using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Regulations.Models;
using Regulations.Models.DatabaseContexts;

namespace Regulations.Controllers
{
    public class HomeController : Controller
    {
        RegulationContext db;
        public HomeController(RegulationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Regulations.ToList());
        }        
    }
}
