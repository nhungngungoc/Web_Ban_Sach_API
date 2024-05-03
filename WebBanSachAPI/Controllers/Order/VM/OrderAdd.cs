namespace WebBanSachAPI.Controllers.Order.VM
{
    public class OrderAdd
    {
        public string NguoiNhan { get; set; }
        public string Phone { get; set; }
        public string DiaChi { get; set; }
        public long Total { get; set; }
        public List<Guid> cartId { get; set; }
    }
}
