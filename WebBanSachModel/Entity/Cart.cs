using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class Cart:BaseEntity
    {
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        public User user { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
