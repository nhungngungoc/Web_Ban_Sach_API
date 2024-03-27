using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachModel.Dto;
using WebBanSachService.Category;
using WebBanSachService.Product;

namespace WebBanSachAPI.Controllers.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;
        private readonly IMapper mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.service = categoryService;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult Post(CategoryDto categoryDto)
        {
            categoryDto.Id = Guid.NewGuid();
            if (this.service.Add(categoryDto))
            {
                return ResponseApiCommon.Success(categoryDto);
            }
            return ResponseApiCommon.Error("Thêm Loại Sản Phẩm Thất Bại");
        }
        [HttpGet]
        public IActionResult Get([FromQuery] CategoryQuery? categoryQuery)
        {
            var data = this.service.GetAll(categoryQuery);
            return ResponseApiCommon.Success(data);
        }
        [HttpPut]
        public IActionResult Put(CategoryDto categoryDto)
        {
            if (this.service.Update(categoryDto))
                return ResponseApiCommon.Success(categoryDto);
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
