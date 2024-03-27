using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;
using WebBanSachRepository.UserRepo;

namespace WebBanSachRepository.CategogyRepo
{
    public class CategogyRepo : GenericRepository<Category>, ICategogyRepo
    {
        public CategogyRepo(BanSachContext context) : base(context)
        {
        }
    }
}
