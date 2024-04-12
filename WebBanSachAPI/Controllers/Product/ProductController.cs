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
        [HttpGet]
        public IActionResult Get([FromQuery] ProductQuery? productQuery)
        {
            var data = this.service.GetAll(productQuery);
            return ResponseApiCommon.Success(data);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductVM provm)
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
            } catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm]ProductVM vm,Guid id)
        {
            var checkProduct = this.service.GetById(id);
            if(checkProduct==null)
            {
                return ResponseApiCommon.Error("Product không tồn tại",404);
            }    
            var dto = new ProductDto()
            {
                Id=id,
                CategoryId=vm.CategoryId,
                Gia=vm.Gia,
                MoTa=vm.MoTa,
                TenSP = vm.TenSP,
                TenTacGia = vm.TenTacGia
            };
            if (vm.file != null)
            {
                var url = this.enviroment.WebRootPath + "\\Upload\\product\\" + vm.file.FileName;
                using (FileStream stream = System.IO.File.Create(url))
                {
                    await vm.file.CopyToAsync(stream);
                }
                string hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                dto.Image = hostUrl + "/Upload/product/" + vm.file.FileName;
            }
            else
                dto.Image = checkProduct.Image;
            if(this.service.Update(dto))
            {
                return ResponseApiCommon.Success(dto,"Cập nhật sản phẩm thành công");
            }
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
