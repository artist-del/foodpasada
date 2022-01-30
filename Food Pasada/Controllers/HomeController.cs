using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Food_Pasada.Models;
using Newtonsoft.Json;

namespace Food_Pasada.Controllers
{
    
    public class HomeController : Controller
    {
        food_pasadaEntities db = new food_pasadaEntities();
        public ActionResult Index()
        {
            List<cbh_product> list = db.cbh_product.ToList();
            return View(list);
        }

        [HttpGet]
        public JsonResult GetId(int id)
        {
            var result = db.cbh_product.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Order(int id)
        {
            var result = db.cbh_product.Where(x => x.id == id).First();
            ViewBag.product = result.product;
            ViewBag.price = result.price;
            ViewBag.image = result.image;
            return View(result);
        }

        [HttpPost]
        public JsonResult Order(cbh_order order)
        {


            if (order.f_name != null && order.address != null && order.number != null)
            {
                db.cbh_order.Add(order);
                db.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(false);
            }
           
        }


        //qualiteaTime
        public ActionResult QualiteaIndex()
        {
            List<qt_product> list = db.qt_product.ToList();
            return View(list);
        }

        [HttpGet]
        public JsonResult QualiteaGetId(int id)
        {
            var result = db.qt_product.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QualiteaOrder(int id)
        {
            var result = db.qt_product.Where(x => x.id == id).First();
            ViewBag.product = result.product;
            ViewBag.price = result.price;
            ViewBag.image = result.image;
            return View(result);
        }
        [HttpPost]
        public JsonResult QualiteaOrder(qt_order order)
        {


            if (order.f_name != null && order.address != null && order.number != null)
            {
                db.qt_order.Add(order);
                db.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(false);
            }

        }

        //gnbRestuarant
        public ActionResult GnBIndex()
        {
            List<gnb_product> list = db.gnb_product.ToList();
            return View(list);
        }

        [HttpGet]
        public JsonResult GnBGetId(int id)
        {
            var result = db.gnb_product.Where(x => x.id == id).SingleOrDefault();

            string value = string.Empty;

            value = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GnBOrder(int id)
        {
            var result = db.gnb_product.Where(x => x.id == id).First();
            ViewBag.product = result.product;
            ViewBag.price = result.price;
            ViewBag.image = result.image;
            return View(result);
        }

        [HttpPost]
        public ActionResult GnBOrder(gnb_order order)
        {

            
            if (order.f_name != null && order.address != null && order.number != null)
            {
                db.gnb_order.Add(order);
                db.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return View();
            }

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
    }
}