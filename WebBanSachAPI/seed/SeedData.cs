using Azure;
using System.Data;
using System.Reflection;
using WebBanSachModel.Entity;
using Common.Helper;

namespace WebBanSachAPI.seed
{
    public class SeedData
    {
        public static void Seed(BanSachContext dbContext)
        {
            if (!dbContext.users.Any())
            {
                var user1 = new User { Id = Guid.NewGuid(), Email = "nhung@gmail.com", MatKhau = Helper.hashPassword("t12345678"), NgaySinh = "04/01/2002", HoVaTen = "Nguyễn Thị Nhung", Phone = "0941590356", Quyen = "Admin", CreateAt = DateTime.Now };
                var user2 = new User { Id = Guid.NewGuid(), Email = "nhung1@gmail.com", MatKhau = Helper.hashPassword("t12345678"), NgaySinh = "04/01/2002", HoVaTen = "Nguyễn Thị Nhung", Phone = "0941590356", Quyen = "User", CreateAt = DateTime.Now };
                dbContext.users.Add(user1);
                dbContext.users.Add(user2);
                dbContext.SaveChanges();
            }    
        }
    }
}
