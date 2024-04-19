using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.CartRepo;

namespace WebBanSachService.Cart
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _repo; 
        private readonly IMapper mapper;
        public CartService(ICartRepo repo,IMapper mappers)
        {
            _repo = repo;
            mapper = mappers;
        }
        public bool Add(CartDto cartDto)
        {
            var find=this._repo.GetAll().Where(x=>x.ProductId==cartDto.ProductId && x.UserId==cartDto.UserId).FirstOrDefault();
            if(find==null)
            {
                return this._repo.Add(mapper.Map<WebBanSachModel.Entity.Cart>(cartDto));
            }
            find.Quantity += 1;
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

        public bool Update(CartDto cartDto)
        {
            return this._repo.Update(mapper.Map<WebBanSachModel.Entity.Cart>(cartDto));
        }
    }
}
