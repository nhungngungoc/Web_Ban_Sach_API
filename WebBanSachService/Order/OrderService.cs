using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.Order;

namespace WebBanSachService.Order
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepo _repo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepo orderService, IMapper mapper)
        {
            _repo = orderService;
            _mapper = mapper;
        }

        public bool Add(OrderDto OrderDto)
        {
            return this._repo.Add(_mapper.Map<WebBanSachModel.Entity.Order>(OrderDto));
        }
        public IEnumerable<OrderDto> getAllNoQuery()
        {
            return _mapper.Map<List<OrderDto>>(this._repo.GetAll());
        }
    }
}
