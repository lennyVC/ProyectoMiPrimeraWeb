using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;

namespace Cibertec.Repositories.NorthWind
{
    public interface IProductRepository:IRepository<Products>
    {
        bool Update(Products products);
        bool Insert(Products products);
        Products GetById(int id);
        bool Delete(int id);
    }
}
