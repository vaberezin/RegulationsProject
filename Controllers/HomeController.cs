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

         public void GetHeaders()
        {
            string table = "";
            foreach (var header in Request.Headers){
                table += $"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>";
            }
            Response.WriteAsync($"<table>{table}</table>");
            
            
        }
    
    }}
        


