using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cibertec.Models;
using Cibertec.Repositories;
using Cibertec.Repositories.NorthWind;
using Cibertec.UnitOfWork;
using System.Configuration;
using Cibertec.Repositories.Dapper.NorthWind;
using log4net;
using Cibertec.Mvc.ActionFilter;

namespace Cibertec.Mvc.Controllers
{
    [RoutePrefix("Customer")]
    //[ErrorActionFilter] podemos ponerlo aca o en BaseController 
    public class CustomerController : BaseController
    {
        //private readonly IUnitOfWork _unit; lo comentamos por que ya se hereda de BaseController

        //ya no se utiliza por injeccion de dependencia
        //public CustomerController()
        //{
        //    _unit = new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        //}

        public CustomerController(ILog log,IUnitOfWork unit):base(log,unit)
        {
            //_unit = unit; ya se inicializa en BaseController
        }

        // GET: Customer
        public ActionResult Index()
        {
            _log.Info("Ejecucion de Customer Controller OK");
            return View(_unit.Customers.GetList());
        }

        public ActionResult Error()
        {
            throw new System.Exception("Pruebas de validacion de Error");
        }

        //CREATE:Customer
        public PartialViewResult Create() // si ponemos ActionResult o PartialViewResult igual va a funcionar
        {
            //return View();
            return PartialView("_Create", new Customers());
        }

        [HttpPost]
        public ActionResult Create(Customers customer)
        {
            if(ModelState.IsValid)
            {
                _unit.Customers.Insert(customer);
                return RedirectToAction("Index");
            }

            return PartialView("_Create", customer);
        }

        public PartialViewResult Update(string id)
        {
            return PartialView("_Update",_unit.Customers.GetById(id));
        }

        [HttpPost]
        public ActionResult Update(Customers customer)
        {
            var val = _unit.Customers.Update(customer);

            if(val)
            {
                return RedirectToAction("Index");
            }

            return PartialView("_Update",customer);
        }

        public PartialViewResult Delete(string id)
        {
            return PartialView("_Delete",_unit.Customers.GetById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(string id)
        {
            var val = _unit.Customers.Delete(id);

            if (val)
            {
                return RedirectToAction("Index");
            }

            return PartialView("_Delete", _unit.Customers.GetById(id));
        }

        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Customers>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Customers.PageList(startRecord, endRecord));
        }

        [Route("Count/{rows:int}")]
        public int Count(int rows)
        {
            var TotalRecords = _unit.Customers.Count();
            return TotalRecords % rows != 0 ? (TotalRecords / rows) + 1 : TotalRecords / rows;
        }
    }
}