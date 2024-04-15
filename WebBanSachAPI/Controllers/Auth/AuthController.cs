using Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachAPI.Controllers.Auth.VM;
using WebBanSachModel.Dto;
using WebBanSachService.Auth;
using WebBanSachService.User;

namespace WebBanSachAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        public AuthController(IAuthService Service, IUserService userService)
        {
            this.authService = Service;
            this.userService = userService;
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
        [HttpPost("register")]
        public IActionResult register(UserDto dto)
        {
            try
            {
                dto.Id = Guid.NewGuid();
                dto.Quyen = "User";
                dto.MatKhau = Helper.hashPassword(dto.MatKhau);
                if (this.userService.getAllNoQuery().Where(x=>x.Email.Equals(dto.Email)).FirstOrDefault()!=null)
                {
                   return  ResponseApiCommon.Error("Tài khoản đã tồn tại",400);
                }
                if (this.userService.Add(dto))
                {
                    return ResponseApiCommon.Success(dto, "Đăng ký tài khoản thành công");
                }
                else
                {
                   return ResponseApiCommon.Error("Lỗi đăng ký tài khoản");
                }
            }
            catch (CommonException ex)
            {
                return ResponseApiCommon.Error(ex.Message, ex.StatusCode);
            }
        }
    }
}
