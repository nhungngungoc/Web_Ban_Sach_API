using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachModel.Entity
{
    public class Huyen
    {
        public long Id { get; set; }
        public string MaHuyen { get; set; }
        public string TenHuyen { get; set; }
        public string Location { get; set; }
        public string Loai { get; set; }
        public string TinhId { get; set; }    
    }
}
