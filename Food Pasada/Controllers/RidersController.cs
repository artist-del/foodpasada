using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food_Pasada.Models;

namespace Food_Pasada.Controllers
{
    public class RidersController : Controller
    {
       
        // GET: Riders
        public ActionResult Index()
        {
            
            ViewBag.time = DateTime.Now.ToShortTimeString();
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.time = DateTime.Now.ToShortTimeString();
            return View();
        }

        [HttpPost]
        public ActionResult Login(tbl_riders riders, Rider rider)
        {
            var db = new food_pasadaEntities();
            var check = db.tbl_riders.Where(x => x.username.Equals(riders.username) && x.password.Equals(riders.password)).FirstOrDefault();
            if(check != null)
            {
                HttpContext.Session["name"] = check.full_name;
                HttpContext.Session["id"] = check.id;
                HttpContext.Session["status"] = check.IsActive;
                return RedirectToAction("Index", "Riders");
                
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Riders");
        }

        public ActionResult Message()
        {
            var db = new food_pasadaEntities();

            var data = (string)HttpContext.Session["name"];
            
                List<tbl_msg> list = db.tbl_msg.Where(x => x.name.Equals(data)).ToList();

                ViewBag.time = DateTime.Now.ToShortTimeString();
                return View(list);
        }

        public ActionResult DeleteMsg(int id)
        {
            var db = new food_pasadaEntities();
            var del = db.tbl_msg.Where(x => x.id == id).SingleOrDefault();
            db.tbl_msg.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Message", "Riders");
        }

        [HttpPost]
        public ActionResult Update(tbl_riders rider)
        {
            var db = new food_pasadaEntities();
            var id = (int)HttpContext.Session["id"];

            var entity = db.tbl_riders.Where(x => x.id == id).FirstOrDefault();
            entity.IsActive = rider.IsActive;
            db.SaveChanges();

            HttpContext.Session["status"] = rider.IsActive;
            return RedirectToAction("Index", "Riders");
        }
    }
}