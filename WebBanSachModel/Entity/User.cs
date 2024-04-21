using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class User:BaseEntity
    {
        public string? HoVaTen { get; set; }
        public string MatKhau{ get; set; }
        public string Email { get; set; }
        public string? NgaySinh { get; set; }
        public string? Phone { get; set; }
        public string Quyen { get; set; }
        public IEnumerable<Cart> carts { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
