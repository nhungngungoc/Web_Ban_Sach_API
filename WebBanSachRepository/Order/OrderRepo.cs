using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;

namespace WebBanSachRepository.Order
{
    public class OrderRepo : GenericRepository<WebBanSachModel.Entity.Order>, IOrderRepo
    {
        public OrderRepo(BanSachContext context) : base(context)
        {
        }
    }
}
