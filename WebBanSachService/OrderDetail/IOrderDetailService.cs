using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;

namespace WebBanSachService.OrderDetail
{
    public interface IOrderDetailService
    {
        bool Add(OrderDetailDto dto);
    }
}
