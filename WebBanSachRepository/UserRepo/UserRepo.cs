using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;

namespace WebBanSachRepository.UserRepo
{
    public class UserRepo : GenericRepository<User>, IUserRepo
    {
        public UserRepo(BanSachContext context) : base(context)
        {
        }
    }
}
