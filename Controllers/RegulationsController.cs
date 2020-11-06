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
using Microsoft.Extensions.Localization;

namespace Regulations.Controllers
{   
    [Authorize (Roles = "admin, user") ]
    public class RegulationsController : Controller
    {
        private readonly IStringLocalizer<AccountController> localizer;
        RegulationContext db;
        
        
        public RegulationsController(RegulationContext context, IStringLocalizer<AccountController> _localizer)
        {
            db = context;
            localizer = _localizer;
        }
        
        
        [HttpGet]
        
        public IActionResult AddRegulation()
        {            
            Regulation reg = new Regulation(); //create instance for default model binding
            return View(reg);

        }

        [HttpPost]
        public async Task<IActionResult> AddRegulation(Regulation regulation)
        {
            if(ModelState.IsValid){
                await db.Regulations.AddAsync(regulation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(regulation);
            
        }

        
        [HttpGet]
        public async Task<IActionResult> UpdateRegulation(int? id){

            if (ActionPermission(id).Result)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                var reg = await db.Regulations.Where<Regulation>(reg => reg.Id == id).FirstOrDefaultAsync();
                return View(reg);
            }
            else
            {
                return Forbid();
            }
        } 

        
        [HttpPost]
        public async Task<IActionResult> UpdateRegulation(Regulation regulation){
            if(ModelState.IsValid){
                db.Regulations.Update(regulation);
                await db.SaveChangesAsync();            
            return RedirectToAction("Index", "Home");
            }
            return View(regulation);       
        }

        
        [HttpDelete]
        public async Task<IActionResult> DeleteRegulation(Regulation regulation) 
        {            
            var reg = await db.Regulations.Where<Regulation>(r => r.Id == regulation.Id).FirstOrDefaultAsync();
            db.Regulations.Remove(reg);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        
        public async Task<IActionResult> DeleteRegulation(int id)
        {       
            if (ActionPermission(id).Result)
            {                
            Regulation regToDelete = await db.Regulations.FindAsync(id);
            db.Regulations.Remove(regToDelete);
            await db.SaveChangesAsync();
            return Content(localizer["strDataDeletedSuccessfully"]); //doest work right. why it doesnt get the value by key?
            }
            else
            {
                return Forbid();
            }
        }

        #region 
        private async Task<bool> ActionPermission(int? id) //regulation id from get request
        {
            Regulation reg = await db.Regulations.Include(u => u.User).Where(r => r.Id == id).FirstOrDefaultAsync();
            string RecordCreator = reg.User?.Email;
            string CurrentUser = HttpContext.User.Identity.Name;
            if(CurrentUser == RecordCreator || HttpContext.User.IsInRole("admin")) //works 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        #endregion
    }
}
