using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class Product:BaseEntity
    {
        public string TenSP { get; set; }
        public string Image { get; set; }
        public string MoTa { get; set; }
        public long Gia { get; set; }
        public string TenTacGia { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public IEnumerable<Cart> carts { get; set; }
    }
}
