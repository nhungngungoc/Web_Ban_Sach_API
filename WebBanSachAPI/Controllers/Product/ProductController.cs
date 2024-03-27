using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachModel.Dto;
using WebBanSachService.Product;
using WebBanSachService.User;

namespace WebBanSachAPI.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;
        private readonly IMapper mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            this.service = productService;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult Post(ProductDto productDto)
        {
            productDto.Id = Guid.NewGuid();
            if (this.service.Add(productDto))
            {
                return ResponseApiCommon.Success(productDto);
            }
            return ResponseApiCommon.Error("Thêm Sản Phẩm Thất Bại");
        }
        [HttpGet]
        public IActionResult Get([FromQuery] ProductQuery? productQuery)
        {
            var data = this.service.GetAll(productQuery);
            return ResponseApiCommon.Success(data);
        }
        [HttpPut]
        public IActionResult Put(ProductDto productDto)
        {
            if (this.service.Update(productDto))
                return ResponseApiCommon.Success(productDto);
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
