using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.CartRepo;
using WebBanSachRepository.ProductRepo;

namespace WebBanSachService.Cart
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _repo; 
        private readonly IProductRepo _productRepo;
        private readonly IMapper mapper;
        public CartService(ICartRepo repo,IMapper mappers, IProductRepo productRepo)
        {
            _repo = repo;
            mapper = mappers;
            _productRepo = productRepo;
        }
        public bool Add(CartDto cartDto)
        {
            var find=this._repo.GetAll().Where(x=>x.ProductId==cartDto.ProductId && x.UserId==cartDto.UserId).FirstOrDefault();
            if(find==null)
            {
                return this._repo.Add(mapper.Map<WebBanSachModel.Entity.Cart>(cartDto));
            }
            find.Quantity = cartDto.Quantity;
            return this._repo.Update(mapper.Map<WebBanSachModel.Entity.Cart>(find));
        }

        public bool Delete(Guid id)
        {
            return this._repo.Delete(id);
        }

        public IEnumerable<CartDto> getAll()
        {
            return mapper.Map<List<CartDto>>(_repo.GetAll());
        }

        public IEnumerable<CartDetail> getCartDetailById(Guid userId)
        {
            var list = from carttbl in _repo.GetAll().Where(x => x.UserId == userId).ToList()
                       join producttbl in _productRepo.GetAll() on carttbl.ProductId equals producttbl.Id
                       select new CartDetail
                       {
                           CartId = carttbl.Id,
                           Image = producttbl.Image,
                           Price = producttbl.Gia,
                           ProductId = producttbl.Id,
                           ProductName = producttbl.TenSP,
                           Quantity = carttbl.Quantity,
                           UserId = userId
                       };
            return list.ToList();
        }
        public bool Update(CartDto cartDto)
        {
            return this._repo.Update(mapper.Map<WebBanSachModel.Entity.Cart>(cartDto));
        }
    }
}
