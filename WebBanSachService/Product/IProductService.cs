using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachService.Common;
using WebBanSachService.User;

namespace WebBanSachService.Product
{
   public interface IProductService
    {
        PageListResultBO<ProductDto> GetAll(ProductQuery? query);
        ProductDto GetById(Guid id);
        bool Add(ProductDto OrderDto);
        bool Update(ProductDto OrderDto);
        bool Delete(Guid id);
        bool softDelete(Guid id);
        IEnumerable<ProductDto> getAllNoQuery();

    }
}
