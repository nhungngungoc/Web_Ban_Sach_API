using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachModel.Entity;

namespace WebBanSachAPI.Controllers.Tinh
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinhController : ControllerBase
    {
        private readonly BanSachContext _dbcontext;
        public TinhController(BanSachContext db) {
            _dbcontext = db;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            try
            {
                return ResponseApiCommon.Success(_dbcontext.TINH.ToList());
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
                return ResponseApiCommon.Success(_dbcontext.TINH.Where(x=>x.MaTinh.Equals(id)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
    }
}
