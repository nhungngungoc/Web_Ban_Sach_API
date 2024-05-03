using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Dto
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
