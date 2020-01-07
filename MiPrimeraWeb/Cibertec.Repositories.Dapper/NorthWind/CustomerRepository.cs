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
    class CustomerRepository : Repository<Customers>, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {

        }

        public bool Update(Customers customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result=
                    connection.Execute("UPDATE Customers SET CompanyName=@company," +
                                            "ContactName = @contact," +
                                            "City = @city," +
                                            "Country = @country," +
                                            "Phone = @phone " +
                                            "WHERE CustomerID = @customerID",new {
                                                company=customer.CompanyName,
                                                contact=customer.ContactName,
                                                city=customer.City,
                                                country=customer.Country,
                                                phone=customer.Phone,
                                                customerID=customer.CustomerID});
                return Convert.ToBoolean(result);
            }
        }

        public Customers GetById(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.GetAll<Customers>().Where(customer => customer.CustomerID.Equals(id)).First();
            }
        }

        public bool Delete(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result =
                    connection.Execute("DELETE FROM Customers "+
                                            "WHERE CustomerID = @customerID", new
                                            {
                                                customerID = id
                                            });
                return Convert.ToBoolean(result);
            }
        }

        public IEnumerable<Customers> PageList(int startRow,int endRow)
        {
            if (startRow >= endRow) return new List<Customers>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@startRow", startRow);
                    parameters.Add("@endRow", endRow);

                    return connection.Query<Customers>("dbo.uspCustomerPageList", parameters, commandType:
                        CommandType.StoredProcedure);
                }
        }

        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("Select Count(CustomerID) from dbo.Customers");
            }
        }
    }
}
