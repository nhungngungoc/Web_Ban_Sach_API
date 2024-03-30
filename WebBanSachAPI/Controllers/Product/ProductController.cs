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
        private readonly IWebHostEnvironment enviroment;

        public ProductController(IProductService productService, IMapper mapper, IWebHostEnvironment enviroment)
        {
            this.service = productService;
            this.mapper = mapper;
            this.enviroment = enviroment;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]ProductVM provm)
        {
            var dto = new ProductDto()
            {
                Id = Guid.NewGuid(),
                Gia = provm.Gia,
                CategoryId = provm.CategoryId,
                MoTa = provm.MoTa,
                TenSP = provm.TenSP,
                TenTacGia = provm.TenTacGia,
            };
            try
            {
                var url = this.enviroment.WebRootPath + "\\Upload\\product\\" + provm.file.FileName;
                using (FileStream stream = System.IO.File.Create(url))
                {
                    await provm.file.CopyToAsync(stream);
                }

                string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                dto.Image = hostUrl + "/Upload/product/" + provm.file.FileName;
                if (this.service.Add(dto))
                {
                    return ResponseApiCommon.Success(dto);
                }
                return ResponseApiCommon.Error("Thêm Sản Phẩm Thất Bại");
            }catch(Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
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
        [HttpGet("{id}")]
        public IActionResult getById(Guid id)
        {
            var data = this.service.GetById(id);
            if (data!=null)
                return ResponseApiCommon.Success(data);
            return ResponseApiCommon.Error("Productd không tồn tại",404);
        }
    }
}
