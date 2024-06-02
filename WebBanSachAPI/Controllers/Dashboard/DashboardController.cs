using Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanSachAPI.Common;
using WebBanSachService.Order;
using WebBanSachService.Product;
using WebBanSachService.User;

namespace WebBanSachAPI.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        public DashboardController(IProductService IproductService,IUserService IuserService, IOrderService order) 
        {
            productService = IproductService;
            userService = IuserService;
            orderService = order;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var countProduct = productService.getAllNoQuery().Count();
                var countUser=userService.getAllNoQuery().Count();

                var countOrder=orderService.getAllNoQuery().Count();

                var doanhThuHomNay = orderService.getAllNoQuery().Where(x => x.NgayDat.Date.Equals(DateTime.Now.Date)).ToList().Sum(x=>x.Total);

                DateTime currentDate = DateTime.Now;
                DateTime startOfWeek = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);

                List<DoanhThuTheoTuan> list = new List<DoanhThuTheoTuan>();
                for (int i = 0; i < 7; i++)
                {
                    DateTime currentDay = startOfWeek.AddDays(i);
                    long totalRevenue = orderService.getAllNoQuery()
                        .Where(order => order.NgayDat.Date == currentDay.Date)
                        .Sum(order => order.Total);

                    DoanhThuTheoTuan doanhThuTheoTuan = currentDay.DayOfWeek switch
                    {
                        DayOfWeek.Monday => new DoanhThuTheoTuan("Thứ 2", totalRevenue),
                        DayOfWeek.Tuesday => new DoanhThuTheoTuan("Thứ 3", totalRevenue),
                        DayOfWeek.Wednesday => new DoanhThuTheoTuan("Thứ 4", totalRevenue),
                        DayOfWeek.Thursday => new DoanhThuTheoTuan("Thứ 5", totalRevenue),
                        DayOfWeek.Friday => new DoanhThuTheoTuan("Thứ 6", totalRevenue),
                        DayOfWeek.Saturday => new DoanhThuTheoTuan("Thứ 7", totalRevenue),
                        DayOfWeek.Sunday => new DoanhThuTheoTuan("Chủ Nhật", totalRevenue),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    list.Add(doanhThuTheoTuan);
                }

                return ResponseApiCommon.Success(new
                {
                    countProduct,
                    countUser,
                    countOrder,
                    doanhThuHomNay,
                    list
                });
            }
            catch(CommonException ex)
            {
                return ResponseApiCommon.Error(ex.Message);
            }
        }
    }
}
