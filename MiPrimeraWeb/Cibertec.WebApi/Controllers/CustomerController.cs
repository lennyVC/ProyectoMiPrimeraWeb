using Cibertec.Models;
using Cibertec.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cibertec.WebApi.Controllers
{
    [RoutePrefix("customer")]
    [Authorize]
    public class CustomerController : BaseController
    {
        public CustomerController(IUnitOfWork unit) : base(unit)
        {

        }

        [Route("{id}")] //Al momento de llamarlo en la url pones customer/id ya no hace caso lo programado en el router
        public IHttpActionResult Get(string id)
        {
            if (id == "" || id == null) return BadRequest();

            return Ok(_unit.Customers.GetById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Customers customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = _unit.Customers.Insert(customer);

            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Customers customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_unit.Customers.Update(customer)) return BadRequest(ModelState);

            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            if (id == "" || id == null) return BadRequest();

            var result = _unit.Customers.Delete(id);
            return Ok(new { delete=true});
        }

        [Route("list")] 
        [HttpGet]
        public IHttpActionResult Getlist()
        {
            return Ok(_unit.Customers.GetList());
        }

    }
}