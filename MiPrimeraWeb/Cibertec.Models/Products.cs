using System;
using System.ComponentModel.DataAnnotations;

namespace Cibertec.Models
{
    [Serializable]
    public class Products
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }
        public bool Discontinued { get; set; }
    }
}
