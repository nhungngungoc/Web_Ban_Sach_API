using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachService.Order
{
    public class OrderDetail
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public string Image { get; set; }
    }
}
