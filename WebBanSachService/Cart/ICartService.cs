using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;

namespace WebBanSachService.Cart
{
    public interface ICartService
    {
        IEnumerable<CartDto> getAll();
        bool Add(CartDto cartDto);
        bool Update(CartDto cartDto);
        bool Delete(Guid id);

    }
}
