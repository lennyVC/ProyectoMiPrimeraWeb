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

namespace Cibertec.Mvc.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IUnitOfWork _unit;

        public OrderDetailsController()
        {
            _unit = new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
        }
        // GET: OrderDetails
        public ActionResult OrderDetailsIndex(int id)
        {
            return View(_unit.OrdersDetails.GetByOrderDetailsId(id));
        }
    }
}