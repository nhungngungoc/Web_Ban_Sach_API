using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class Tinh
    {
        public long Id { get; set; }
        public string MaTinh { get; set; }
        public string TenTinh { get; set; }
        public string Loai { get; set; }
        public string OldSysId { get; set; }
    }
}
