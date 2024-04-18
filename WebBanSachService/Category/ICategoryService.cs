using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.common;
using WebBanSachService.Common;
using WebBanSachService.User;

namespace WebBanSachService.Category
{
    public interface ICategoryService
    {
        PageListResultBO<CategoryDto> GetAll(CategoryQuery? query);
        IEnumerable<CategoryDto> getAllNoQuery();
        CategoryDto GetById(Guid id);
        bool Add(CategoryDto OrderDto);
        bool Update(CategoryDto OrderDto);
        bool Delete(Guid id);
        bool softDelete(Guid id);
        List<SelectListItem> GetDropdown(string displayMember, string valueMember, object selected = null);
    }
}
