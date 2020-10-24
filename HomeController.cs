using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using quickkart2.Models;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace quickkart2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly QuickKartContext _context;
        QuickKartRepository repObj;
        public HomeController(QuickKartContext context)
        {
            _context = context;
            repObj = new QuickKartRepository(_context);
        }


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckRole(IFormCollection frm)
        {
            string userId = frm["name"];
            string password = frm["pwd"];
            string checkbox = frm["RememberMe"];
            if (checkbox == "on")
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("UserId", userId, option);
                Response.Cookies.Append("Password", password, option);
            }
            string username = userId.Split('@')[0];
            byte? roleId = repObj.ValidateCredentials(userId, password);
            if (roleId == 1)
            {
                if (Request.Cookies[username.ToString() + "lastlogintime"] == null)
                    Response.Cookies.Append(username.ToString() + "lastlogintime", DateTime.Now.ToString());

                HttpContext.Session.SetString("username", username);
                return RedirectToAction("AdminHome", "Admin");
            }
            else if (roleId == 2)
            {
                HttpContext.Session.SetString("username", username);
                return Redirect("/Customer/CustomerHome?username=" + username);
            }
            return View("Login");
        }

        public ViewResult Contact()
        {
            ViewBag.EmailId = "admin@quickKart.com";
            ViewData["ContactNumber"] = 9876543210;
            return View();
        }

        public IActionResult Logout()
        {
            var username = HttpContext.Session.GetString("username");
            ViewData["username"] = username;
            HttpContext.Session.Clear();
            return View();
        }
    }
}
