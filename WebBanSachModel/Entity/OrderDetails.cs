using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class OrderDetails:BaseEntity
    {
        public int Quantity { get; set; }
        public int Price { get; set; }
        public Guid ProductId { get; set; }
        public Product product { get; set; }
        public Guid OrderId { get; set; }
        public Order order { get; set; }
    }
}
