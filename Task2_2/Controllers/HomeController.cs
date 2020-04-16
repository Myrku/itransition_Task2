using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2_2.Models;

namespace Task2_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            bool isContinued = false;
            using (itranDBEntities1 userContext = new itranDBEntities1())
            {
                string sessionId = Session["id"].ToString();
                var u = userContext.Users.Where(user => user.ID.ToString().Equals(sessionId) && user.status != "block").FirstOrDefault();
                if (u != null)
                {
                    isContinued = true;
                }
            }
            if (isContinued)
            {
                using (itranDBEntities1 userContext = new itranDBEntities1())
                {
                    ViewBag.userList = userContext.Users.ToList();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult MainPage(FormCollection form, int[] selectedUsers)
        {
            bool isContinued = false;
            using (itranDBEntities1 userContext = new itranDBEntities1())
            {
                string sessionId = Session["id"].ToString();
                var u = userContext.Users.Where(user => user.ID.ToString().Equals(sessionId) && user.status != "block").FirstOrDefault();
                if(u != null)
                {
                    isContinued = true;
                }
            }
            if (form.GetKey(0) == "block" && isContinued)
            {
                User u;
                int id;
                using (itranDBEntities1 userContext = new itranDBEntities1())
                {
                    for (int i = 0; i < selectedUsers.Length; i++)
                    {
                        id = selectedUsers[i];
                        u = userContext.Users.Where(user => user.ID.Equals(id)).FirstOrDefault();
                        if (u != null)
                        {
                            u.status = "block";
                        }
                        if (u != null && u.ID.ToString().Equals(Session["id"]))
                        {
                            Session["id"] = null;
                        }
                    }
                    userContext.SaveChanges();
                }
                if (Session["id"] == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("MainPage");
                }
            }
            else if (form.GetKey(0) == "unblock" && isContinued)
            {
                User u;
                int id;
                using (itranDBEntities1 userContext = new itranDBEntities1())
                {
                    for (int i = 0; i < selectedUsers.Length; i++)
                    {
                        id = selectedUsers[i];
                        u = userContext.Users.Where(user => user.ID.Equals(id)).FirstOrDefault();
                        if (u != null)
                        {
                            u.status = "unblock";
                        }
                    }
                    userContext.SaveChanges();
                }

                return RedirectToAction("MainPage");
            }
            else if (form.GetKey(0) == "delete" && isContinued)
            {
                User u;
                int id;
                using (itranDBEntities1 userContext = new itranDBEntities1())
                {
                    for (int i = 0; i < selectedUsers.Length; i++)
                    {
                        id = selectedUsers[i];
                        u = userContext.Users.Where(user => user.ID.Equals(id)).FirstOrDefault();
                        if (u != null)
                        {
                            userContext.Users.Remove(u);
                        }
                        if (u != null && u.ID.ToString().Equals(Session["id"]))
                        {
                            Session["id"] = null;
                        }
                    }
                    userContext.SaveChanges();
                }
                if (Session["id"] == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    return RedirectToAction("MainPage");
                }
            }
            else if (!isContinued)
            {
                Session["id"] = null;
                return RedirectToAction("Login");
            }
            return RedirectToAction("MainPage");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin u)
        {
            if (ModelState.IsValid)
            {
                using (itranDBEntities1 userContext = new itranDBEntities1())
                {
                    var v = userContext.Users.Where(user => user.name.Equals(u.name) && user.password.Equals(u.password) && !user.status.Equals("block")).FirstOrDefault();
                    if (v != null)
                    {
                        Session["name"] = u.name.ToString();
                        Session["id"] = v.ID.ToString();
                        v.lastLoginDate = DateTime.Now;
                        userContext.SaveChanges();
                        return RedirectToAction("MainPage");
                    }
                    else
                    {
                        ViewBag.LoginMessage = "Неверный логин или пароль, либо пользователь " + u.name + " заблокирован";
                    }
                }
            }
            return View(u);

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User u)
        {
            if (ModelState.IsValid)
            {
                using (itranDBEntities1 userContext = new itranDBEntities1())
                {
                    var v = userContext.Users.Where(user => user.name.Equals(u.name)).FirstOrDefault();

                    if (v == null)
                    {
                        u.registerDate = DateTime.Now;
                        u.status = "unblock";
                        userContext.Users.Add(u);
                        userContext.SaveChanges();
                        ViewBag.RegisterMessage = "Пользователь " + u.name + " успешно зарегистрирован";
                    }
                    else
                    {
                        ViewBag.RegisterMessage = "Полбзователь c логином " + u.name + " уже существует";
                    }
                }
                ModelState.Clear();
            }
            return View();
        }
    }
}
