using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachService.Common;

namespace WebBanSachService.User
{
    public interface IUserService
    {
        PageListResultBO<UserDto> GetAll(UserQuery? query);
        UserDto GetById(Guid id);
        bool Add(UserDto OrderDto);
        bool Update(UserDto OrderDto);
        bool Delete(Guid id);
        bool softDelete(Guid id);
    }
}
