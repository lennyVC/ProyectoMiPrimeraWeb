using System;
namespace Cibertec.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipName { get; set; }
        public string CustomerID { get; set; }
        public decimal Freight { get; set; }
    }
}