using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface ICustomerRepository: IRepository<Customers>
    {
        bool Update(Customers customers);
        Customers GetById(string id);
        bool Delete(string id);
        IEnumerable<Customers> PageList(int startRow, int endRow);
        int Count();
    }
}
