using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibertec.UnitOfWork;
using Cibertec.Repositories.Dapper.NorthWind;
using System.Configuration;
using Cibertec.Repositories;
using Cibertec.Repositories.NorthWind;
using Cibertec.Models;

namespace Cibertec.Mvc.Controllers
{
    public class SupplierController : Controller
    {
        public readonly IUnitOfWork _unit;

        //public SupplierController()
        //{
        //    _unit = new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        //}
        public SupplierController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        // GET: Supplier
        public ActionResult SupplierIndex()
        {
            return View(_unit.Suppliers.GetList());
        }

        //CREATE:Supplier
        public ActionResult SupplierCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SupplierCreate(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                _unit.Suppliers.Insert(suppliers);
                return RedirectToAction("SupplierIndex");
            }

            return View();
        }

        public ActionResult SupplierUpdate(int id)
        {
            return View(_unit.Suppliers.GetById(id));
        }

        [HttpPost]
        public ActionResult SupplierUpdate(Suppliers suppliers)
        {
            var val = _unit.Suppliers.Update(suppliers);

            if (val)
            {
                return RedirectToAction("SupplierIndex");
            }

            return View(suppliers);
        }

        public ActionResult SupplierDelete(int id)
        {
            return View(_unit.Suppliers.GetById(id));
        }

        [HttpPost]
        //[ActionName("Delete")] esto es para que me reconozca a traves de la vista ya que la vista llama el post
        public ActionResult SupplierDeletePost(int id)
        {
            var val = _unit.Suppliers.Delete(id);

            if (val)
            {
                return RedirectToAction("SupplierIndex");
            }

            return View();
        }
    }
}