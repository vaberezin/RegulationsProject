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
                User user = await db.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password); //finding user in db
                if (user != null){
                    await Authenticate(user);
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
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email); //checking if login already exists
                 //adding new user to database
                if(user == null){    //if not exists 
                    user = new User{ Email = model.Email, Password = model.Password};
                    //attach role to user. Mean, if u register through the form, you can only be 'user'. Admin roles 
                    //add only manually.
                    Role userRole = await db.Roles.FirstOrDefaultAsync(role => role.Name == "user");
                    if(userRole != null){
                        user.Role = userRole;
                    }
                    db.Users.Add(new User {Email = model.Email, Password = model.Password});
                    await db.SaveChangesAsync();
                    await Authenticate(user); //authentication   
                    return RedirectToAction("Index", "Home");
                }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                    
                }
            return View(model);
            
        }

        private async Task Authenticate(User user){
            
            //create list of claims
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),   //defaultname claim
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name) //defaultrole claim                 
            };
            //create ClaimsIdentity object
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)); //creating cookies
        }
        
        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");            
        }  

        public IActionResult AccessDenied(){
            return View();
        }  
    }
}
        


