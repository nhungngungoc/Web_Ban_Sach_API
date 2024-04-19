using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;
using WebBanSachRepository.CategogyRepo;

namespace WebBanSachRepository.CartRepo
{
    public class CartRepo : GenericRepository<Cart>, ICartRepo
    {
        public CartRepo(BanSachContext context) : base(context)
        {
        }
    }
}
