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
using log4net;
using Cibertec.Mvc.ActionFilter;

namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("Supplier")]
    public class SupplierController : BaseController
    {

        public SupplierController(ILog log, IUnitOfWork unit) : base(log, unit)
        {
            
        }

        // GET: Supplier
        public ActionResult SupplierIndex()
        {
            _log.Info("Ejecucion de Customer Controller OK");
            return View(_unit.Suppliers.GetList());
        }

        public ActionResult Error()
        {
            throw new System.Exception("Pruebas de validacion de Error");
        }

        //CREATE:Supplier
        public PartialViewResult SupplierCreate()
        {
            return PartialView("_SupplierCreate", new Suppliers());
        }

        [HttpPost]
        public ActionResult SupplierCreate(Suppliers suppliers)
        {
            if (ModelState.IsValid)
            {
                _unit.Suppliers.Insert(suppliers);
                return RedirectToAction("SupplierIndex");
            }

            return PartialView("_SupplierCreate", suppliers);
        }

        public PartialViewResult SupplierUpdate(int id)
        {
            return PartialView("_SupplierUpdate", _unit.Suppliers.GetById(id));
        }

        [HttpPost]
        public ActionResult SupplierUpdate(Suppliers suppliers)
        {
            var val = _unit.Suppliers.Update(suppliers);

            if (val)
            {
                return RedirectToAction("SupplierIndex");
            }

            return PartialView("_SupplierUpdate", suppliers);
        }

        public PartialViewResult SupplierDelete(int id)
        {
            return PartialView("_SupplierDelete", _unit.Suppliers.GetById(id));
        }

        [HttpPost]
        //[ActionName("Delete")] //esto es para que me reconozca a traves de la vista ya que la vista llama el post
        public ActionResult SupplierDeletePost(int id)
        {
            var val = _unit.Suppliers.Delete(id);

            if (val)
            {
                return RedirectToAction("SupplierIndex");
            }

            return PartialView("_SupplierDelete",_unit.Suppliers.GetById(id));
        }

        [Route("SupplierList/{page:int}/{rows:int}")]
        public PartialViewResult SupplierList(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Suppliers>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_SupplierList", _unit.Suppliers.PageList(startRecord, endRecord));
        }

        [Route("SupplierCount/{rows:int}")]
        public int SupplierCount(int rows)
        {
            var TotalRecords = _unit.Suppliers.SupplierCount();
            return TotalRecords % rows != 0 ? (TotalRecords / rows) + 1 : TotalRecords / rows;
        }
    }
}