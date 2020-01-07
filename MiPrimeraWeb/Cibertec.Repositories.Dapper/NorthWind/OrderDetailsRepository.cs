using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cibertec.Models;
using Cibertec.Repositories.NorthWind;
using System.Data.SqlClient;
using System.Configuration;
using Cibertec.Repositories.Dapper.NorthWind;
using Cibertec.UnitOfWork;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Cibertec.Repositories.Dapper.NorthWind
{
    class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {

        public OrderDetailsRepository(string connectionString) : base(connectionString)
        {
        }

        public List<OrderDetails> GetByOrderDetailsId(int OrderId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                //LINQ
                //return conn.GetAll<OrderDetails>().Where(orderDetails => orderDetails.OrderId.Equals(OrderId)).ToList();

                //return conn.Query<OrderDetails>("select OrderID,ProductID,UnitPrice,Quantity from OrderDetails where OrderID = @OrderID", new { OrderId }).ToList();

                //Query es propio del dapper, esto es mas solido
                //return conn.Query<OrderDetails>("usp_GetDatailsByOrder", new { OrderID = OrderId },commandType:CommandType.StoredProcedure).ToList();

                //Usando Ado QueryString
                //string query = "select OrderID,ProductID,UnitPrice,Quantity from OrderDetails where OrderID = @OrderID";

                //SqlCommand command = new SqlCommand(query, conn);
                //command.Parameters.AddWithValue("@OrderID", OrderId);
                //List<OrderDetails> lstOrderDetails = new List<OrderDetails>();

                //try
                //{
                //    conn.Open();
                //    SqlDataReader reader = command.ExecuteReader();
                //    while(reader.Read())
                //    {
                //        OrderDetails orderDetails = new OrderDetails();
                //        orderDetails.OrderId = Int32.Parse(reader["OrderID"].ToString());
                //        orderDetails.ProductId = Int32.Parse(reader["ProductID"].ToString());
                //        orderDetails.UnitPrice = Double.Parse(reader["UnitPrice"].ToString());
                //        orderDetails.Quantity = Int32.Parse(reader["Quantity"].ToString());
                //        lstOrderDetails.Add(orderDetails);
                //    }

                //    reader.Close();
                //}
                //catch(Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                //return lstOrderDetails;

                //Ado forma 2
                //string query = "select OrderID,ProductID,UnitPrice,Quantity from OrderDetails where OrderID = {0}";

                //SqlCommand command = new SqlCommand(query, conn);
                //command.CommandText=string.Format(query,OrderId);
                //List<OrderDetails> lstOrderDetails = new List<OrderDetails>();

                //try
                //{
                //    conn.Open();
                //    SqlDataReader reader = command.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        OrderDetails orderDetails = new OrderDetails();
                //        orderDetails.OrderId = Int32.Parse(reader["OrderID"].ToString());
                //        orderDetails.ProductId = Int32.Parse(reader["ProductID"].ToString());
                //        orderDetails.UnitPrice = Double.Parse(reader["UnitPrice"].ToString());
                //        orderDetails.Quantity = Int32.Parse(reader["Quantity"].ToString());
                //        lstOrderDetails.Add(orderDetails);
                //    }

                //    reader.Close();
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                //return lstOrderDetails;

                //ADO opcion 3

                SqlCommand command = new SqlCommand("usp_GetDatailsByOrder", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", OrderId);
                List<OrderDetails> lstOrderDetails = new List<OrderDetails>();

                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderDetails orderDetails = new OrderDetails();
                        orderDetails.OrderId = Int32.Parse(reader["OrderID"].ToString());
                        orderDetails.ProductId = Int32.Parse(reader["ProductID"].ToString());
                        orderDetails.UnitPrice = Double.Parse(reader["UnitPrice"].ToString());
                        orderDetails.Quantity = Int32.Parse(reader["Quantity"].ToString());
                        lstOrderDetails.Add(orderDetails);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return lstOrderDetails;
            }
        }

    }
}
