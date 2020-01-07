using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Repositories.NorthWind;

namespace Cibertec.UnitOfWork
{
    public interface IUnitOfWork
    {
        //Es el contrato llamamos a todas las interfaces de las entidades
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IOrderDetailsRepository OrdersDetails { get; }
        IProductRepository Products { get; }
        ISupplierRepository Suppliers { get; }
        IUserRepository Users { get; }
    }
}
