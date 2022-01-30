using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food_Pasada.Models;
using Newtonsoft.Json;

namespace Food_Pasada.Controllers
{
    public class AdminController : Controller
    {
        food_pasadaEntities db = new food_pasadaEntities();

        public ActionResult Index()
        {
            var time = DateTime.Now;
            var date = DateTime.Now;
            var count = db.res_admin.Count();

            ViewBag.count = count;
            ViewBag.date = date.ToShortDateString();
            ViewBag.time = time.ToShortTimeString();

            List<res_admin> list = db.res_admin.ToList();
            List<tbl_riders> list1 = db.tbl_riders.ToList();
            ViewBag.rider = list1;
            ViewBag.riderCount = list1.Count();
            return View(list);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Account acc)
        {
            var check = db.fp_admin.Where(x => x.username.Equals(acc.username) && x.password.Equals(acc.password)).FirstOrDefault();

            if (check != null)
            {
                Session["users"] = check.username;
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult OwnerTable()
        {
            List<res_admin> res_admin = db.res_admin.ToList();

            return View(res_admin);
        }

        [HttpPost]
        public ActionResult Create(res_admin res)
        {
            if (ModelState.IsValid)
            {

                db.res_admin.Add(res);
                db.SaveChanges();

                return RedirectToAction("OwnerTable", "Admin");

            }
            return RedirectToAction("OwnerTable", "Admin");
        }

        [HttpGet]
        public JsonResult GetOwnersId(int id)
        {
            var result = db.res_admin.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(res_admin res)
        {
            try
            {
                var entity = db.res_admin.Where(x => x.id == res.id).FirstOrDefault();
                if (entity != null)
                {
                    entity.f_name = res.f_name;
                    entity.address = res.address;
                    entity.rest_name = res.rest_name;
                    entity.username = res.username;
                    entity.password = res.password;
                    entity.phone = res.phone;
                    entity.email = res.email;

                    db.SaveChanges();

                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex
                });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var entity = db.res_admin.Where(x => x.id == id).FirstOrDefault();
                if(entity != null)
                {
                    db.res_admin.Remove(entity);
                    db.SaveChanges();

                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex
                });
            }
        }

        public ActionResult RiderTable()
        {
            List<tbl_riders> list = db.tbl_riders.ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult RiderCreate(tbl_riders riders)
        {
            if (ModelState.IsValid)
            {

                db.tbl_riders.Add(riders);
                db.SaveChanges();

                return RedirectToAction("RiderTable", "Admin");

            }
            return RedirectToAction("RiderTable", "Admin");
        }
        [HttpGet]
        public JsonResult GetRidersId(int id)
        {
            var result = db.tbl_riders.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RiderUpdate(tbl_riders rider)
        {
            try
            {
                var entity = db.tbl_riders.Where(x => x.id == rider.id).FirstOrDefault();
                if (entity != null)
                {
                    entity.full_name = rider.full_name;
                    entity.address = rider.address;
                    entity.number = rider.number;
                    entity.email = rider.email;
                    entity.IsActive = rider.IsActive;
                    entity.username = rider.username;
                    entity.password = rider.password;
                    db.SaveChanges();

                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex
                });
            }
        }

        [HttpPost]
        public JsonResult RiderDelete(int id)
        {
            try
            {
                var entity = db.tbl_riders.Where(x => x.id == id).SingleOrDefault();
                if (entity != null)
                {
                    db.tbl_riders.Remove(entity);
                    db.SaveChanges();

                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex
                });
            }
        }


    }
}