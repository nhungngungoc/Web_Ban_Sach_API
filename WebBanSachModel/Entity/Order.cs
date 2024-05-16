using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class Order:BaseEntity
    {
        public string NguoiNhan { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public long Total { get; set; }
        public DateTime NgayDat { get; set; }
        public int TrangThai { get; set; }
        public Guid UserId { get; set; }
        public string TypePay { get; set; }
        public User user { get; set; }
        public IEnumerable<OrderDetails> orderDetails { get; set; }
    }
}
