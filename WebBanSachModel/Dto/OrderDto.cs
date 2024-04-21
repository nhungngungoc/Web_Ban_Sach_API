using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string NguoiNhan { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public long Total { get; set; }
        public DateTime NgayDat { get; set; }
        public int TrangThai { get; set; }
        public Guid UserId { get; set; }
    }
}
