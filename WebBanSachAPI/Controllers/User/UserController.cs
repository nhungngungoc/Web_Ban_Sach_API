using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachModel.Dto;
using WebBanSachService.User;

namespace WebBanSachAPI.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IMapper mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            this.service = userService;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult Post(UserDto userDto)
        {
            userDto.Id = Guid.NewGuid();
            if (this.service.Add(userDto))
            {
                return ResponseApiCommon.Success(userDto);
            }
            return ResponseApiCommon.Error("Thêm User Thất Bại");
        }
        [HttpGet]
        public IActionResult Get([FromQuery] UserQuery? userQuery)
        {
            var data = this.service.GetAll(userQuery);
            return ResponseApiCommon.Success(data);
        }
        [HttpPut]
        public IActionResult Put(UserDto userDto) 
        {  
            if(this.service.Update(userDto))
                return ResponseApiCommon.Success(userDto);
            return ResponseApiCommon.Error("Cập nhật thất bại");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) 
        {
            if (this.service.Delete(id))
                return ResponseApiCommon.Success(id);
            return ResponseApiCommon.Error("Xóa thất bại");
        }
    }
}
