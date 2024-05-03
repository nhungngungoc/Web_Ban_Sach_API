using Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachAPI.Controllers.Order.VM;
using WebBanSachModel.Dto;
using WebBanSachService.Cart;
using WebBanSachService.Order;
using WebBanSachService.OrderDetail;
using WebBanSachService.Product;

namespace WebBanSachAPI.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;
        private readonly IOrderDetailService orderDetailService;
        private readonly IProductService productService;
        private readonly ICartService cartService;
        public OrderController(IOrderService service, IOrderDetailService orderDetailService, IProductService productService, ICartService cartService)
        {
            this.service = service;
            this.orderDetailService = orderDetailService;
            this.productService = productService;
            this.cartService = cartService;
        }
        [Authorize]
        [HttpPost]
        public IActionResult post(OrderAdd orderVM)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimsConstant.USER_ID)?.Value;
                if (userId == null)
                {
                    return ResponseApiCommon.Error("không có userId");
                }
                var orderDto = new OrderDto()
                {
                    DiaChi = orderVM.DiaChi,
                    Id = Guid.NewGuid(),
                    NgayDat = DateTime.Now,
                    NguoiNhan = orderVM.NguoiNhan,
                    Phone = orderVM.Phone,
                    Total = orderVM.Total,
                    TrangThai = 0,
                    UserId = Guid.Parse(userId),
                };
                if (this.service.Add(orderDto))
                {
                    foreach (var item in orderVM.cartId)
                    {
                        var findcart = this.cartService.getAll().Where(x=>x.Id.Equals(item)).FirstOrDefault();
                        if (findcart == null)
                        {
                            return ResponseApiCommon.Error("Không tìm thấy cart");
                        }
                        this.cartService.Delete(findcart.Id);
                        var orderDetail = new OrderDetailDto()
                        {
                            Id = Guid.NewGuid(),
                            OrderId = orderDto.Id,
                            Price = int.Parse(this.productService.GetById(findcart.ProductId).Gia.ToString()),
                            ProductId = findcart.ProductId,
                            Quantity = findcart.Quantity,
                        };
                        if (!this.orderDetailService.Add(orderDetail))
                        {
                            return ResponseApiCommon.Error("Đặt hàng lỗi");
                        }
                    }
                    return ResponseApiCommon.Success(orderVM, "Đặt hàng thành công");
                }
                return ResponseApiCommon.Error("Lỗi thêm vào order");
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
    }
}
