namespace WebBanSachAPI.Controllers.Dashboard
{
    public class DoanhThuTheoTuan
    {
        public string name { get; set; }
        public long total { get; set; }

        public DoanhThuTheoTuan(string name, long total)
        {
            this.name = name;
            this.total = total;
        }    
    }
}
