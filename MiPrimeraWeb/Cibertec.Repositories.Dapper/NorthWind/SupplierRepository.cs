using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Data;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    class SupplierRepository:Repository<Suppliers>,ISupplierRepository
    {
        public SupplierRepository(string connectionString):base(connectionString)
        {

        }

        public bool Update(Suppliers suppliers)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result =
                    connection.Execute("UPDATE Suppliers SET CompanyName=@companyName," +
                                            "ContactName = @contactName, " +
                                            "ContactTitle = @contactTitle, " +
                                            "City = @city, " +
                                            "Country = @country, " +
                                            "Phone = @phone, " +
                                            "Fax = @fax " +
                                            "WHERE SupplierID = @supplierID", new
                                            {
                                                companyName = suppliers.CompanyName,
                                                contactName = suppliers.ContactName,
                                                contactTitle = suppliers.ContactTitle,
                                                city = suppliers.City,
                                                country = suppliers.Country,
                                                phone = suppliers.Phone,
                                                fax = suppliers.Fax,
                                                supplierID = suppliers.SupplierID,
                                            });
                return Convert.ToBoolean(result);
            }
        }

        public Suppliers GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Suppliers>().Where(suppliers => suppliers.SupplierID.Equals(id)).First();
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result =
                    connection.Execute("DELETE FROM Suppliers " +
                                            "WHERE SupplierID = @SuppliersID", new
                                            {
                                                SuppliersID = id
                                            });
                return Convert.ToBoolean(result);
            }
        }

        public bool Insert(Suppliers Suppliers)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result =
                    connection.Execute("INSERT INTO [dbo].[Suppliers]([CompanyName],[ContactName],[ContactTitle]"+
                                        ",[City],[Country],[Phone],[Fax])VALUES(@companyName, @contactName"+
                                        ", @contactTitle, @city, @country, @phone, @fax)", new
                                       {
                                            companyName = Suppliers.CompanyName,
                                            contactName = Suppliers.ContactName,
                                            contactTitle = Suppliers.ContactTitle,
                                            city = Suppliers.City,
                                            country = Suppliers.Country,
                                            phone = Suppliers.Phone,
                                            fax = Suppliers.Fax,
                                        });
                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<Suppliers> PageList(int startRow, int endRow)
        {
            if (startRow >= endRow) return new List<Suppliers>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);

                return connection.Query<Suppliers>("dbo.uspSupplierPageList", parameters, commandType:
                    CommandType.StoredProcedure);
            }
        }

        public int SupplierCount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select Count(SupplierID) from dbo.Suppliers");
            }
        }
    }
}
