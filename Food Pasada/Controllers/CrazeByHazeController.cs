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
    public class CrazeByHazeController : Controller
    {
        // GET: CrazeByHaze
        public ActionResult Index()
        {
            var db = new food_pasadaEntities();
            List<cbh_product> list = db.cbh_product.ToList();
            List<cbh_order> list1 = db.cbh_order.ToList();
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
            food_pasadaEntities db = new food_pasadaEntities();

            var check = db.res_admin.Where(x => x.username.Equals(acc.username) && x.password.Equals(acc.password)).FirstOrDefault();

            if (check != null)
            {
                Session["users"] = check.f_name;
                return RedirectToAction("Index", "CrazeByHaze");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "CrazeByHaze");
        }

        public ActionResult Product()
        {
            var db = new food_pasadaEntities();
            List<cbh_product> list = db.cbh_product.ToList();

            return View(list);
        }

        [HttpPost]
        public JsonResult Create(cbh_product products)
        {
            var db = new food_pasadaEntities();

            if(db.cbh_product.Any(x=>x.product == products.product))
            {
                var result = "Product Name Already Taken";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if(products.product!=null && products.price!=null&& products.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(products.ImageUpload.FileName);
                    string extension = Path.GetExtension(products.ImageUpload.FileName);
                    filename = filename + extension;
                    products.image = filename;
                    products.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Craze"), filename));

                    db.cbh_product.Add(products);
                    db.SaveChanges();

                    var result = "New Product Save.";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = "Complete the form";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public JsonResult Update(cbh_product pr)
        {
            var db = new food_pasadaEntities();
            try
            {
                var entity = db.cbh_product.Where(x => x.id == pr.id).FirstOrDefault();
                if (entity != null)
                {
                    if (pr.ImageUpload != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(pr.ImageUpload.FileName);
                        string extension = Path.GetExtension(pr.ImageUpload.FileName);
                        
                        pr.image = pr.product + extension;
                        pr.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFile/Craze"), pr.image));

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
            catch(Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex
                });
            }
        }

        [HttpGet]
        public JsonResult GetProduct(int id)
        {
            var db = new food_pasadaEntities();
            var result = db.cbh_product.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var db = new food_pasadaEntities();
            var del = db.cbh_product.Where(x => x.id == id).FirstOrDefault();

            if (del != null)
            {
                db.cbh_product.Remove(del);
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


        public ActionResult Order()
        {
            var db = new food_pasadaEntities();
            List<cbh_order> list = db.cbh_order.OrderByDescending(x => x.id).ToList();
            return View(list);
        }

        public ActionResult DeleteOrder(int id)
        {
            var db = new food_pasadaEntities();
            var del = db.cbh_order.Where(x => x.id == id).SingleOrDefault();
            db.cbh_order.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Order", "CrazeByHaze");
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
            var result = db.cbh_order.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
    }
}