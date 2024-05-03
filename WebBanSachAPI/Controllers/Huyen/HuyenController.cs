using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachModel.Entity;

namespace WebBanSachAPI.Controllers.Huyen
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuyenController : ControllerBase
    {
        private readonly BanSachContext _dbcontext;
        public HuyenController(BanSachContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [HttpGet("byMaTinh/{maTinh}")]
        public IActionResult Get(string maTinh)
        {
            try
            {
                return ResponseApiCommon.Success(_dbcontext.HUYEN.Where(x => x.TinhId.Equals(maTinh)).ToList());
            }catch(Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult getById(string id)
        {
            try
            {
                return ResponseApiCommon.Success(_dbcontext.HUYEN.Where(x => x.MaHuyen.Equals(id)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
    }
}
