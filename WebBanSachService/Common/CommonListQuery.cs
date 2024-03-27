using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachService.Common
{
    public class CommonListQuery
    {
        public int page { get; set; }
        public int limit { get; set; }
        public string orderBy { get; set; }
        public string keyword { get; set; }
        public CommonListQuery()
        {
            page = 1;
            limit = 10;
            orderBy = "CreateAt";
            keyword = string.Empty;
        }
    }
}
