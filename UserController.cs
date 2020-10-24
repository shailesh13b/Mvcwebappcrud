using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace quickkart2.Controllers
{
    public class UserController : Controller
    {
        private readonly QuickKartContext _context;
        QuickKartRepository repObj;
        public UserController(QuickKartContext context)
        {
            _context = context;
            repObj = new QuickKartRepository(_context);
        }
        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult Loginpage()
        {
            return RedirectToAction("Login", "Home");
        }
        public IActionResult SaveRegisterUser(Models.Users userObj)
        {
            if (ModelState.IsValid)
            {
                var returnValue = repObj.
                    RegisterUser(userObj.EmailId, userObj.UserPassword, userObj.Gender, userObj.DateOfBirth, userObj.Address);
                if (returnValue)
                    return RedirectToAction("Login", "Home");
                else
                    return View("Error");
            }
            return View("RegisterUser");
        }


    }
}
