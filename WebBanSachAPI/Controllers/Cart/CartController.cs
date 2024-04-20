using Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachAPI.Controllers.Cart.VM;
using WebBanSachModel.Dto;
using WebBanSachService.Cart;

namespace WebBanSachAPI.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;
        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        [HttpPost]
        public IActionResult add(AddCartVM vm)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimsConstant.USER_ID)?.Value;
                if (userId == null)
                {
                    return ResponseApiCommon.Error("Token errro");
                }
                var dto = new CartDto()
                {
                    ProductId = vm.ProductId,
                    Quantity = Int32.Parse(vm.Quantity),
                    UserId = Guid.Parse(userId.ToString())
                };
                if(this.cartService.Add(dto))
                {
                    return ResponseApiCommon.Success(dto, "Thêm sản phẩm giỏ hàng thành công");
                }
                return ResponseApiCommon.Error("Thêm sản phẩm vào giỏ hàng thất bại");
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
        
        [HttpGet]
        public IActionResult get()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimsConstant.USER_ID)?.Value;
                if (userId == null)
                {
                    return ResponseApiCommon.Error("Token errro");
                }
                return ResponseApiCommon.Success(this.cartService.getCartDetailById(Guid.Parse(userId)));
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult detele(Guid id)
        {
            try
            {
                if(this.cartService.Delete(id))
                {
                    return ResponseApiCommon.Success(id,"Xóa sản phẩm giỏ hàng thành công");
                }
               return ResponseApiCommon.Error("Xóa cart lỗi");
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
    }
}
