using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food_Pasada.Models;
using System.IO;
using Newtonsoft.Json;

namespace Food_Pasada.Controllers
{
    public class QualiteaTimeController : Controller
    {
        // GET: QualityTime
        public ActionResult Index()
        {
            var db = new food_pasadaEntities();
            List<qt_product> list = db.qt_product.ToList();
            List<qt_order> list1 = db.qt_order.ToList();
            List<tbl_riders> riders = db.tbl_riders.Where(x => x.IsActive == "Active").ToList();
            List<tbl_riders> riderList = db.tbl_riders.ToList();

            ViewBag.riderList = riderList;
            ViewBag.rider = riders.Count();
            ViewBag.count1 = list1.Count();
            ViewBag.count = list.Count();
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
            var db = new food_pasadaEntities();
            var check = db.res_admin.Where(x => x.username.Equals(acc.username) && x.password.Equals(acc.password)).FirstOrDefault();

            if (check != null)
            {
                Session["users"] = check.f_name;
                return RedirectToAction("Index","QualiteaTime");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "QualiteaTime");
        }

        public ActionResult Product()
        {
            var db = new food_pasadaEntities();
            List<qt_product> list = db.qt_product.ToList();

            return View(list);
        }

        [HttpPost]
        public JsonResult Create(qt_product qt)
        {
            var db = new food_pasadaEntities();

            if (qt.product != null && qt.product!=null&& qt.ImageUpload!=null)
            {
                string filename = Path.GetFileNameWithoutExtension(qt.ImageUpload.FileName);
                string extension = Path.GetExtension(qt.ImageUpload.FileName);
                qt.image = qt.product + extension;
                qt.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Qt"), qt.image));

                db.qt_product.Add(qt);
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

        [HttpPost]
        public JsonResult Updates(qt_product qt)
        {
            var db = new food_pasadaEntities();
            var entity = db.qt_product.Where(x => x.id == qt.id).FirstOrDefault();

            if (entity != null)
            {
                if (qt.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(qt.ImageUpload.FileName);
                    string extension = Path.GetExtension(qt.ImageUpload.FileName);
                    qt.image = qt.product + extension;
                    qt.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Qt"), qt.image));

                    entity.product = qt.product;
                    entity.price = qt.price;
                    entity.image = qt.image;
                    db.SaveChanges();

                    return Json(new
                    {
                        status = true
                    });
                }
                entity.product = qt.product;
                entity.price = qt.price;
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

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var db = new food_pasadaEntities();
            var entity = db.qt_product.Where(x => x.id == id).FirstOrDefault();

            if (entity != null)
            {
                db.qt_product.Remove(entity);
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

        [HttpGet]
        public JsonResult GetId(int id)
        {
            var db = new food_pasadaEntities();
            var result = db.qt_product.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Order()
        {
            var db = new food_pasadaEntities();
            List<qt_order> list = db.qt_order.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        public ActionResult DeleteOrder(int id)
        {
            var db = new food_pasadaEntities();
            var del = db.qt_order.Where(x => x.id == id).SingleOrDefault();
            db.qt_order.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Order", "QualiteaTime");
        }

        public ActionResult OrderMessage()
        {
            var db = new food_pasadaEntities();
            List<tbl_riders> list = db.tbl_riders.ToList();
            return View(list);
        }

        [HttpGet]
        public JsonResult GetRiders(int id)
        {
            var db = new food_pasadaEntities();
            var result = db.tbl_riders.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveMessage(tbl_msg msg)
        {
            var db = new food_pasadaEntities();

            db.tbl_msg.Add(msg);
            db.SaveChanges();
            return RedirectToAction("OrderMessage", "CrazeByHaze");
        }

        [HttpGet]
        public JsonResult GetOrders(int id)
        {
            var db = new food_pasadaEntities();
            var result = db.qt_order.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}
