using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;

namespace WebBanSachService.Order
{
    public interface IOrderService
    {
        bool Add(OrderDto OrderDto);
        IEnumerable<OrderDto> getAllNoQuery();
        IEnumerable<OrderDetail> GetOrderDetails(Guid orderId);
    }
}
