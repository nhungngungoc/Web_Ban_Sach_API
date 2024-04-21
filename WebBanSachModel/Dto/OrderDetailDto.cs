using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;

namespace WebBanSachModel.Dto
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
