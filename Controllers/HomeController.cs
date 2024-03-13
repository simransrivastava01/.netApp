using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        DBEntities db = new DBEntities();
        public ActionResult Index()
        {
            return View(db.UserInfoes.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserInfo userInfo)
        {
            if(db.UserInfoes.Any(x=>x.Username == userInfo.Username))
            {
                ViewBag.Notification = "Account already exists.";
                return View();
            }
            else
            {
                db.UserInfoes.Add(userInfo);
                db.SaveChanges();

                Session["Id"] = userInfo.Id.ToString();
                Session["Username"] = userInfo.Username.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserInfo userInfo)
        {
            var checkLogin = db.UserInfoes.Where(x => x.Username.Equals(userInfo.Username) && x.Username.Equals(userInfo.Username)).FirstOrDefault();
            if(checkLogin !=null)
            {
                Session["Id"] = userInfo.Id.ToString();
                Session["Username"] = userInfo.Username.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong username or password";
            }
            return View();
        }
    }
}