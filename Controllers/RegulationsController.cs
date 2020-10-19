﻿using System;
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
        public IActionResult AddRegulation(){

            Regulation reg = new Regulation(); //create instance for default model binding
            return View(reg);
            //return View();
        }

        [HttpPost]
        public IActionResult AddRegulation(Regulation regulation)
        {

            db.Regulations.Add(regulation);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
        public IActionResult UpdateRegulation(Regulation regulation){
            db.Regulations.Update(regulation);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");        
        }

        [HttpDelete]
        public IActionResult DeleteRegulation(Regulation regulation) //void?! bad, ok let it be...
        {            
            var reg = db.Regulations.Where<Regulation>(r => r.Id == regulation.Id).FirstOrDefault();
            db.Regulations.Remove(reg);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public string DeleteRegulation(int id)
        {            
            Regulation regToDelete = db.Regulations.Find(id);
            db.Regulations.Remove(regToDelete);
            db.SaveChanges();
            return "Данные успешно удалены.";
        }


    }
}
