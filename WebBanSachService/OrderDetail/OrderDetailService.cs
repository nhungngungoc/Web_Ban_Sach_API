using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachRepository.OrderDetail;

namespace WebBanSachService.OrderDetail
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IOrderDetailRepo repo;
        private readonly IMapper mapper;
        public OrderDetailService(IOrderDetailRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public bool Add(OrderDetailDto dto)
        {
            return this.repo.Add(mapper.Map<WebBanSachModel.Entity.OrderDetails>(dto));
        }
    }
}
