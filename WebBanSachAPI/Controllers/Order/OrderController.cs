using Common.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using WebBanSachAPI.Common;
using WebBanSachAPI.Controllers.Order.VM;
using WebBanSachAPI.Controllers.VnPay;
using WebBanSachModel.Dto;
using WebBanSachService.Cart;
using WebBanSachService.Order;
using WebBanSachService.OrderDetail;
using WebBanSachService.Product;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WebBanSachAPI.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public string returnUrl = "http://localhost:5058/api/Order/confirm";
        public string tmnCode = "15AAI3L0";
        public string hashSecret = "E5IIOTV4DX2RSQP67YIYX42TB2FDWWZ4";
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
                    TypePay=orderVM.TypePay
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
        [Authorize]
        [HttpPost("VnPay")]
        public IActionResult getVnp(OrderAdd orderVM)
        {
            string hostName = System.Net.Dns.GetHostName();
            string clientIPAddress = System.Net.Dns.GetHostAddresses(hostName).GetValue(0).ToString();
            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", tmnCode);
            pay.AddRequestData("vnp_Amount", orderVM.Total.ToString()+"00");
            pay.AddRequestData("vnp_BankCode", "");
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", clientIPAddress);
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", orderVM.Phone+" "+orderVM.DiaChi);
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", returnUrl);
            pay.AddRequestData("vnp_TxnRef", new Random().Next(1000, 100000).ToString());

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return ResponseApiCommon.Success(paymentUrl);
        }


        [HttpGet("confirm")]
        public IActionResult confirm()
        {
            if (Request.QueryString.HasValue)
            {
                //lấy toàn bộ dữ liệu trả về
                var queryString = Request.QueryString.Value;
                var json = HttpUtility.ParseQueryString(queryString);

                long orderId = Convert.ToInt64(json["vnp_TxnRef"]); //mã hóa đơn
                string orderInfor = json["vnp_OrderInfo"].ToString(); //Thông tin giao dịch
                long vnpayTranId = Convert.ToInt64(json["vnp_TransactionNo"]); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = json["vnp_ResponseCode"].ToString(); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = json["vnp_SecureHash"].ToString(); //hash của dữ liệu trả về
                var pos = Request.QueryString.Value.IndexOf("&vnp_SecureHash");

                //return Ok(Request.QueryString.Value.Substring(1, pos-1) + "\n" + vnp_SecureHash + "\n"+ PayLib.HmacSHA512(hashSecret, Request.QueryString.Value.Substring(1, pos-1)));
                bool checkSignature = ValidateSignature(Request.QueryString.Value.Substring(1, pos - 1), vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?
                if (checkSignature && tmnCode == json["vnp_TmnCode"].ToString())
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        return ResponseApiCommon.Success("Thành công");
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        return ResponseApiCommon.Error("Lỗi");
                    }
                }
                else
                {
                    //phản hồi không khớp với chữ ký
                    return ResponseApiCommon.Error("đường dẫn nếu phản hồi ko hợp lệ");
                }
            }
            //phản hồi không hợp lệ
            return ResponseApiCommon.Error("Lỗi");
        }
        [NonAction]
        public bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
        [Authorize]
        [HttpGet("getListOrderByUserId")]
        public IActionResult getListOrderByUserId()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimsConstant.USER_ID)?.Value;
                if (userId == null)
                {
                    return ResponseApiCommon.Error("không có userId");
                }
                var data = this.service.getAllNoQuery().Where(x=>x.UserId==Guid.Parse(userId)).ToList();
                return ResponseApiCommon.Success(data);
            }
            catch(CommonException ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("getListOrderDetailByUserId/{orderId}")]
        public IActionResult getListOrderDetailByUserId(Guid orderId)
        {
            try
            {
                
                return ResponseApiCommon.Success(this.service.GetOrderDetails(orderId));
            }
            catch (CommonException ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
    }
}
