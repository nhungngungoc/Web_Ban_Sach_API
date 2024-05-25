using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.Order;
using WebBanSachRepository.OrderDetail;
using WebBanSachRepository.ProductRepo;

namespace WebBanSachService.Order
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepo _repo;
        private readonly IOrderDetailRepo _orderDetailRepo;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepo orderService, IMapper mapper, IOrderDetailRepo orderDetailRepo, IProductRepo productRepo)
        {
            _repo = orderService;
            _mapper = mapper;
            _orderDetailRepo = orderDetailRepo;
            _productRepo = productRepo;
        }

        public bool Add(OrderDto OrderDto)
        {
            return this._repo.Add(_mapper.Map<WebBanSachModel.Entity.Order>(OrderDto));
        }
        public IEnumerable<OrderDto> getAllNoQuery()
        {
            return _mapper.Map<List<OrderDto>>(this._repo.GetAll());
        }

        public IEnumerable<OrderDetail> GetOrderDetails(Guid orderId)
        {
            var productAll=_productRepo.GetAll();
            var orderDetail=_orderDetailRepo.GetAll();
            var list = from ordertbl in this._repo.GetAll().Where(x => x.Id == orderId).ToList()
                       join orderDetailtbl in orderDetail on ordertbl.Id equals orderDetailtbl.OrderId
                       join productbl in productAll on orderDetailtbl.ProductId equals productbl.Id
                       select new OrderDetail
                       {
                           Image=productbl.Image,
                           Price= orderDetailtbl.Price,
                           ProductId=productbl.Id,
                           ProductName=productbl.TenSP,
                           Quantity= orderDetailtbl.Quantity
                       };
            return list.ToList();
        }
    }
}
