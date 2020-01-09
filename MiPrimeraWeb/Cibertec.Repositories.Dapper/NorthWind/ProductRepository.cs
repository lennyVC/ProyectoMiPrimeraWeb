using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Data;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    class ProductRepository:Repository<Products>,IProductRepository
    {
        public ProductRepository(string connectionString):base(connectionString)
        {

        }
        public bool Update(Products products)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result =
                    connection.Execute("UPDATE Products SET ProductName=@productName," +
                                            "SupplierId = @supplierId," +
                                            "UnitPrice = @unitPrice," +
                                            "QuantityPerUnit = @quantityPerUnit," +
                                            "Discontinued = @discontinued " +
                                            "WHERE ProductID = @productID", new
                                            {
                                                productName = products.ProductName,
                                                supplierId = products.SupplierId,
                                                unitPrice = products.UnitPrice,
                                                quantityPerUnit = products.QuantityPerUnit,
                                                discontinued = products.Discontinued,
                                                productID = products.ProductID
                                            });
                return Convert.ToBoolean(result);
            }
        }

        public Products GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Products>().Where(products => products.ProductID.Equals(id)).First();
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result =
                    connection.Execute("DELETE FROM Products " +
                                            "WHERE ProductID = @productsID", new
                                            {
                                                productsID = id
                                            });
                return Convert.ToBoolean(result);
            }
        }

        public bool Insert(Products products)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result =
                    connection.Execute("INSERT INTO [dbo].[Products]([ProductName],[SupplierID],[QuantityPerUnit]"+
                                       ",[UnitPrice],[Discontinued])VALUES"+
                                       " (@productName, @supplierId,@quantityPerUnit, @unitPrice, @discontinued)", new
                                            {
                                                productName = products.ProductName,
                                                supplierId = products.SupplierId,
                                                quantityPerUnit = products.QuantityPerUnit,
                                                unitPrice = products.UnitPrice,
                                                discontinued = products.Discontinued,
                                            });
                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<Products> PageList(int startRow, int endRow)
        {
            if (startRow >= endRow) return new List<Products>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);

                return connection.Query<Products>("dbo.uspProductPageList", parameters, commandType:
                    CommandType.StoredProcedure);
            }
        }

        public int ProductCount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select Count(ProductID) from dbo.Products");
            }
        }
    }
}
