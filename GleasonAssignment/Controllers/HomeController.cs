using GleasonAssignment.IRepository;
using GleasonAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace GleasonAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _Repository;

        public HomeController(IUserRepository repository)
        {
            _Repository = repository;
        }
        public IActionResult Logout()
        {
            if (Request.Cookies["UserName"] != null)
            {
                string value = DateTime.Now.ToString();
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Append("UserName", value, options);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserFormModel userModel)
        {
            const string SessionUser = "UserName";
            var status = _Repository.UserValidation(userModel);
            if (status)
            {

                CookieOptions obj = new CookieOptions();
                obj.Expires = DateTime.Now.AddMinutes(15);
                Response.Cookies.Append(SessionUser, userModel.UserName, obj);
                return RedirectToAction("UserCount", "Users");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Credential");
            }
            return View(userModel);
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