using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachModel.Entity;

namespace WebBanSachAPI.Controllers.Xa
{
    [Route("api/[controller]")]
    [ApiController]
    public class XaController : ControllerBase
    {
        private readonly BanSachContext _dbcontext;
        public XaController(BanSachContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet("byMaHuyen/{maHuyen}")]
        public IActionResult get(string maHuyen)
        {
            try
            {
                return ResponseApiCommon.Success(_dbcontext.XA.Where(x => x.HuyenId.Equals(maHuyen)).ToList());
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult getById(string id)
        {
            try
            {
                return ResponseApiCommon.Success(_dbcontext.XA.Where(x => x.MaXa.Equals(id)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
    }
}
