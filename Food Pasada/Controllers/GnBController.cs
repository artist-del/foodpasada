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
    public class GnBController : Controller
    {
        // GET: GnB
        public ActionResult Index()
        {
            if (Session["users"] == null)
            {
                return RedirectToAction("Login", "GnB");
            }
            var db = new food_pasadaEntities();
            List<gnb_product> list = db.gnb_product.ToList();
            List<gnb_order> list1 = db.gnb_order.ToList();
            List<tbl_riders> riders = db.tbl_riders.Where(x => x.IsActive == "Active").ToList();
            List<tbl_riders> riderList = db.tbl_riders.ToList();

            ViewBag.riderList = riderList;
            ViewBag.rider = riders.Count();
            ViewBag.count1 = list1.Count();
            ViewBag.count = list.Count;
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
                return RedirectToAction("Index", "GnB");
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "GnB");
        }

        public ActionResult Product()
        {
            var db = new food_pasadaEntities();
            List<gnb_product> list = db.gnb_product.ToList();

            return View(list);
        }

        [HttpPost]
        public JsonResult Create(gnb_product pr)
        {
            var db = new food_pasadaEntities();
           
                if (pr.product != null && pr.price != null && pr.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pr.ImageUpload.FileName);
                    string extension = Path.GetExtension(pr.ImageUpload.FileName);
                    pr.image = pr.product + extension;
                    pr.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/GnB"), pr.image));

                    db.gnb_product.Add(pr);
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
        public JsonResult Updates(gnb_product pr)
        {
            var db = new food_pasadaEntities();

            var entity = db.gnb_product.Where(x => x.id == pr.id).FirstOrDefault();

            if (entity != null)
            {
                if (pr.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pr.ImageUpload.FileName);
                    string extension = Path.GetExtension(pr.ImageUpload.FileName);
                    pr.image = pr.product + extension;
                    pr.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/GnB"), pr.image));

                    entity.product = pr.product;
                    entity.price = pr.price;
                    entity.image = pr.image;

                    db.SaveChanges();

                    return Json(new
                    {
                        status = true
                    });
                }
                entity.product = pr.product;
                entity.price = pr.price;
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
            var entity = db.gnb_product.Where(x => x.id == id).FirstOrDefault();

            if (entity != null)
            {
                db.gnb_product.Remove(entity);
                db.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpGet]
        public JsonResult GetUpdateId(int id)
        {
            var db = new food_pasadaEntities();
            var result = db.gnb_product.Where(x => x.id == id).SingleOrDefault();

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
            List<gnb_order> list = db.gnb_order.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        public ActionResult DeleteOrder(int id)
        {
            var db = new food_pasadaEntities();
            var del = db.gnb_order.Where(x => x.id == id).SingleOrDefault();
            db.gnb_order.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Order", "GnB");
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
            var result = db.gnb_order.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}