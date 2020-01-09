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
using log4net;
using Cibertec.Mvc.ActionFilter;


namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("Product")]
    public class ProductController : BaseController
    {
        //public readonly IUnitOfWork _unit;

        //public ProductController()
        //{
        //    _unit = new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        //}
        public ProductController(ILog log, IUnitOfWork unit) : base(log, unit)
        {

        }

        // GET: Product
        public ActionResult ProductIndex()
        {
            _log.Info("Ejecucion de Product Controller OK");
            return View(_unit.Products.GetList());
        }

        public ActionResult Error()
        {
            throw new System.Exception("Pruebas de validacion de Error");
        }

        //CREATE:Product
        public PartialViewResult ProductCreate()
        {
            return PartialView("_ProductCreate", new Products());
        }

        [HttpPost]
        public ActionResult ProductCreate(Products product)
        {
            if (ModelState.IsValid)
            {
                _unit.Products.Insert(product);
                return RedirectToAction("ProductIndex");
            }

            return PartialView("_ProductCreate", product);
        }

        public PartialViewResult ProductUpdate(int id)
        {
            return PartialView("_ProductUpdate",_unit.Products.GetById(id));
        }
        
        [HttpPost]
        public ActionResult ProductUpdate(Products product)
        {
            var val = _unit.Products.Update(product);

            if (val)
            {
                return RedirectToAction("ProductIndex");
            }

            return PartialView("_ProductUpdate", product);
        }

        public PartialViewResult ProductDelete(int id)
        {
            return PartialView("_ProductDelete", _unit.Products.GetById(id));
        }

        [HttpPost]
        //[ActionName("Delete")] //esto es para que me reconozca a traves de la vista ya que la vista llama el post
        public ActionResult ProductDeletePost(int id)
        {
            var val = _unit.Products.Delete(id);

            if (val)
            {
                return RedirectToAction("ProductIndex");
            }

            return PartialView("_ProductDelete", _unit.Products.GetById(id));
        }

        [Route("ProductList/{page:int}/{rows:int}")]
        public PartialViewResult ProductList(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Products>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_ProductList", _unit.Products.PageList(startRecord, endRecord));
        }

        [Route("ProductCount/{rows:int}")]
        public int SupplierCount(int rows)
        {
            var TotalRecords = _unit.Products.ProductCount();
            return TotalRecords % rows != 0 ? (TotalRecords / rows) + 1 : TotalRecords / rows;
        }
    }
}