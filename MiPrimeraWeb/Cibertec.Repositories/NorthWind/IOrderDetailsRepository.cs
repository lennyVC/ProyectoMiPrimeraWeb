using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface IOrderDetailsRepository: IRepository<OrderDetails>
    {
        List<OrderDetails> GetByOrderDetailsId(int OrderID);
    }
}

    
