using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;

namespace WebBanSachRepository.OrderDetail
{
    public class OrderDeatilRepo : GenericRepository<WebBanSachModel.Entity.OrderDetails>, IOrderDetailRepo
    {
        public OrderDeatilRepo(BanSachContext context) : base(context)
        {
        }
    }
}
