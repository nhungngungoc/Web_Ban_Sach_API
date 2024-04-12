using Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachAPI.Controllers.Auth.VM;
using WebBanSachService.Auth;

namespace WebBanSachAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService Service)
        {
            this.authService = Service;
        }
        [HttpPost("login")]
        public IActionResult login([FromBody] LoginVM login)
        {
            try
            {
                var data = authService.checklogin(login.email, login.password);
                if (data != string.Empty)
                {
                    return ResponseApiCommon.Success(data, "Đăng nhập thành công");
                }
                return ResponseApiCommon.Error("Tài khoản mật khẩu không chính xác", 401);
            }
            catch (CommonException ex)
            {
                return ResponseApiCommon.Error(ex.Message, ex.StatusCode);
            }
        }
    }
}
