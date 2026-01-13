using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LumelSalesManagementRepository.Models
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CategoryId { get; set; }
        public string Region { get; set; } = string.Empty;
        public DateTime DateOfSale { get; set; }
        public int QuantitySold { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        //(Other relevant fields){ get; set; } (e.g., product descriptions, customer demographics, marketing campaign details)
    }
}
