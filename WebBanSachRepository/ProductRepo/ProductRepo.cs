using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Entity;
using WebBanSachRepository.CategogyRepo;

namespace WebBanSachRepository.ProductRepo
{
    public class ProductRepo : GenericRepository<Product>, IProductRepo
    {
        public ProductRepo(BanSachContext context) : base(context)
        {
        }
    }
}
