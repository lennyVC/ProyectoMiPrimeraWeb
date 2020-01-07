using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface ISupplierRepository:IRepository<Suppliers>
    {
        bool Update(Suppliers suppliers);
        bool Insert(Suppliers suppliers);
        Suppliers GetById(int id);
        bool Delete(int id);
    }
}
