using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BowlingAlleyWeb.Models;
using Microsoft.AspNetCore.Http;
using BowlingAlleyDAL.Models;
using BowlingAlleyDAL;
using AutoMapper;

namespace BowlingAlleyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly BowlingAlleyDBContext _context;
        BowlingAlleyRepository rep;

        private readonly IMapper _mapper;
        public HomeController(BowlingAlleyDBContext context, IMapper mapper)
        {
            _context = context;
            rep = new BowlingAlleyRepository(context);
            _mapper = mapper;

        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckRole(IFormCollection frm)
        {
            string userId = frm["name"];
            string checkbox = frm["RememberMe"];
            if (checkbox == "on")
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("UserId", userId, option);
            }
            string username = userId;
            bool? roleType = rep.GetRoleType(username);
            if (roleType == true)
            {
                HttpContext.Session.SetString("username", username);
                TempData["username"] = username;
                return RedirectToAction("GetReservationDetails", "Admin");
            }
            else if (roleType == false)
            {
                TempData["username"] = username;
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("GetFreeSlots","Member");
            }
            TempData["username"] = "Guest";
            return View("Login");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
