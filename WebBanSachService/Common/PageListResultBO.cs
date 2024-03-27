using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSachService.Common
{
    public class PageListResultBO<T> where T : class
    {
            public List<T> items { get; set; }
            public int totalItems { get; set; }
    }
}
