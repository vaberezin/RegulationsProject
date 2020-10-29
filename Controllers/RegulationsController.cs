using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Regulations.Models;
using Regulations.Models.DatabaseContexts;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Regulations.Controllers
{
    [Authorize (Roles = "admin, user") ]
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
            if(ModelState.IsValid){
                db.Regulations.Add(regulation);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(regulation);
            
        }

        [Authorize (Roles = "admin")] //+"user"
        [HttpGet]
        public IActionResult UpdateRegulation(int? id){


            

            if()



            if (id == null){
                return RedirectToAction("Index");
            }
            var reg = db.Regulations.Where<Regulation>(reg => reg.Id == id).FirstOrDefault();
            return View(reg);
        } 

        [Authorize (Roles = "admin")]
        [HttpPost]
        public IActionResult UpdateRegulation(Regulation regulation){
            if(ModelState.IsValid){
            db.Regulations.Update(regulation);
            db.SaveChanges();            
            return RedirectToAction("Index", "Home");
            }
            return View(regulation);       
        }

        [Authorize (Roles = "admin")]
        [HttpDelete]
        public IActionResult DeleteRegulation(Regulation regulation) //void?! bad, ok let it be...
        {            
            var reg = db.Regulations.Where<Regulation>(r => r.Id == regulation.Id).FirstOrDefault();
            db.Regulations.Remove(reg);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize (Roles = "admin")]
        public string DeleteRegulation(int id)
        {                       
            Regulation regToDelete = db.Regulations.Find(id);
            db.Regulations.Remove(regToDelete);
            db.SaveChanges();
            return "Данные успешно удалены.";
        }

        private async Task<bool> ActionPermission(int id) //regulation id from get request
        {
            Regulation reg = await db.Regulations.Include(u => u.User).Where(r => r.Id == id).FirstOrDefaultAsync();
            string RecordCreator = reg.User?.Email;
            string CurrentUser = HttpContext.User.Identity.Name;
            if(RecordCreator == CurrentUser)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
