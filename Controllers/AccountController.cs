using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Regulations.Models.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using Regulations.Models; // пространство имен UserContext и класса User
using Regulations.Models.DatabaseContexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Regulations.Controllers
{    
    public class AccountController : Controller
    {
        private RegulationContext db;
        public AccountController(RegulationContext context)
        {
            db = context;
        }
        
        [HttpGet]
        public IActionResult Login(){
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model){
            if(ModelState.IsValid){
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null){
                    await Authenticate(model.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("","Неправильный логин и(или) пароль.");
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model){
            if(ModelState.IsValid){
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email); //adding new user to database
                if(user == null){
                    db.Users.Add(new User {Email = model.Email, Password = model.Password});
                    await db.SaveChangesAsync();
                    await Authenticate(model.Email); //authentication
                    return RedirectToAction("Index", "Home");
                }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                    
                }
            return View(model);
            
        }

        private async Task Authenticate(string userName){
            //create claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            //create ClaimsIdentity object
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");            
        }    
    }
}
        


