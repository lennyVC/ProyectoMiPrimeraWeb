using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibertec.UnitOfWork;
using System.Configuration;
using Cibertec.Repositories.Dapper.NorthWind;
using Cibertec.Models;
using Cibertec.Repositories;
using Cibertec.Repositories.NorthWind;


namespace Cibertec.Mvc.Controllers
{
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unit;

        //public ProductController()
        //{
        //    _unit = new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        //}
        public ProductController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: Product
        public ActionResult ProductIndex()
        {
            return View(_unit.Products.GetList());
        }

        //CREATE:Product
        public ActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProductCreate(Products product)
        {
            if (ModelState.IsValid)
            {
                _unit.Products.Insert(product);
                return RedirectToAction("ProductIndex");
            }

            return View();
        }

        public ActionResult ProductUpdate(int id)
        {
            return View(_unit.Products.GetById(id));
        }
        
        [HttpPost]
        public ActionResult ProductUpdate(Products product)
        {
            var val = _unit.Products.Update(product);

            if (val)
            {
                return RedirectToAction("ProductIndex");
            }

            return View(product);
        }

        public ActionResult ProductDelete(int id)
        {
            return View(_unit.Products.GetById(id));
        }

        [HttpPost]
        //[ActionName("Delete")] esto es para que me reconozca a traves de la vista ya que la vista llama el post
        public ActionResult ProductDeletePost(int id)
        {
            var val = _unit.Products.Delete(id);

            if (val)
            {
                return RedirectToAction("ProductIndex");
            }

            return View();
        }
    }
}