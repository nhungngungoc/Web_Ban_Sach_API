using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace WebBanSachAPI.Controllers.VnPay
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        public string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public string returnUrl = "http://localhost:5058/api/VnPay/confirm";
        public string tmnCode = "15AAI3L0";
        public string hashSecret = "E5IIOTV4DX2RSQP67YIYX42TB2FDWWZ4";
        public VnPayController() { }
        [HttpGet]
        public IActionResult getVnp()
        {
            string hostName = System.Net.Dns.GetHostName();
            string clientIPAddress = System.Net.Dns.GetHostAddresses(hostName).GetValue(0).ToString();
            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); 
            pay.AddRequestData("vnp_Command", "pay"); 
            pay.AddRequestData("vnp_TmnCode", tmnCode); 
            pay.AddRequestData("vnp_Amount", "2000000000"); 
            pay.AddRequestData("vnp_BankCode", ""); 
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); 
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", clientIPAddress);
            pay.AddRequestData("vnp_Locale", "vn"); 
            pay.AddRequestData("vnp_OrderInfo", "thanh toán test");
            pay.AddRequestData("vnp_OrderType", "other"); 
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); 
            pay.AddRequestData("vnp_TxnRef", new Random().Next(1000, 100000).ToString()); 

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            
            return Ok(paymentUrl);
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
                        return Ok("Thành công");
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        return BadRequest("Lỗi");
                    }
                }
                else
                {
                    //phản hồi không khớp với chữ ký
                    return BadRequest("đường dẫn nếu phản hồi ko hợp lệ");
                }
            }
            //phản hồi không hợp lệ
            return BadRequest("Lỗi");
        }
        [NonAction]
        public bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
