using GleasonAssignment.IRepository;
using GleasonAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace GleasonAssignment.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _repository;
        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }
        public ActionResult UserCount()
        {
            var requestCookie = Request.Cookies["UserName"];
            if (requestCookie != null)
            {
                TempData["UserName"] = requestCookie;
                var totalUserCount = _repository.ShowAllUsers().Count();
                TempData["UserCount"] = totalUserCount;
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult UserList()
        {
            var requestCookie = Request.Cookies["UserName"];
            if (requestCookie != null)
            {
                TempData["UserName"] = requestCookie;
                return View(_repository.ShowAllUsers());

            }
            else
                return RedirectToAction("Index", "Home");
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            var requestCookie = Request.Cookies["UserName"];
            if (requestCookie != null)
            {
                var result = _repository.GetUser(id);
                var roles = _repository.GetRoles();
                var assignedRole = result.Roles.Split(',').ToList();
                foreach (var item in roles)
                {
                    item.IsAssigned = assignedRole.Find(w => w == item.RoleName) != null;

                }
                ViewBag.RoleList = roles;
                TempData["UserName"] = requestCookie;
                return View(result);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        // GET: UsersController/Create
        public ActionResult CreateUser()
        {
            var requestCookie = Request.Cookies["UserName"];
            if (requestCookie != null)
            {
                var roles = _repository.GetRoles();
                ViewBag.Roles = roles;
                TempData["UserName"] = requestCookie;
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(IFormCollection pairs, UserFormModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var roleList = new List<int>();
                    foreach (var pair in pairs["selectedItems"])
                    {
                        roleList.Add(Convert.ToInt32(pair));
                    }
                    _repository.SaveUser(obj, roleList);
                    return RedirectToAction("UserList");
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                return RedirectToAction("UserList");
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            var requestCookie = Request.Cookies["UserName"];
            if (requestCookie != null)
            {
                var result = _repository.GetUser(id);
                var roles = _repository.GetRoles();
                var assignedRole = result.Roles.Split(',').ToList();
                foreach (var item in roles)
                {
                    item.IsAssigned = assignedRole.Find(w => w == item.RoleName) != null;

                }
                ViewBag.RoleList = roles;
                TempData["UserName"] = requestCookie;
                return View(result);
            }
            else
                return RedirectToAction("Index", "Home");

        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, UserFormModel model)
        {
            try
            {

                var roleList = new List<int>();
                foreach (var pair in collection["selectedItems"])
                {
                    roleList.Add(Convert.ToInt32(pair));
                }
                _repository.UpdateUser(id, model, roleList);
                return RedirectToAction("UserList");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var requestCookie = Request.Cookies["UserName"];
                if (requestCookie != null)
                {
                    _repository.DeleteUser(id);
                    TempData["UserName"] = requestCookie;
                    return RedirectToAction("UserList");
                }
                else
                    return RedirectToAction("Index", "Home");

            }
            catch
            {
                return View();
            }
        }
    }
}
