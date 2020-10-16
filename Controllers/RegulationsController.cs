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
    public class RegulationsController : Controller
    {
        RegulationContext db;
        
        
        public RegulationsController(RegulationContext context)
        {
            db = context;
        }
        
        
        [HttpGet]
        public IActionResult AddRegulation(int? id){
            if (id == null){
                return RedirectToAction("Index");
            }
            var reg = db.Regulations.Where<Regulation>(reg => reg.Id == id).FirstOrDefault();
            return View(reg);
        }
        
        [HttpGet]
        public IActionResult UpdateRegulation(int? id){
            if (id == null){
                return RedirectToAction("Index");
            }
            var reg = db.Regulations.Where<Regulation>(reg => reg.Id == id).FirstOrDefault();
            return View(reg);
        }

        [HttpPost]
        public string Post(Regulation regulation) //form sending
        {
            db.Regulations.Add(regulation);
            db.SaveChanges();
            return "Данные успешно добавлены.";
        }

        [HttpPut]
        public string Put(Regulation regulation){
            db.Regulations.Update(regulation);
            db.SaveChanges();
            return "Данные успешно обновлены.";
        }

        [HttpDelete]
        public string Delete(Regulation regulation){
            db.Regulations.Remove(regulation);
            db.SaveChanges();
            return "Данные успешно Удалены.";
        }

        
    }
}
