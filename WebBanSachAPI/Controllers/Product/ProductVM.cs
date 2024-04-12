namespace WebBanSachAPI.Controllers.Product
{
    public class ProductVM
    {
        public string TenSP { get; set; }
        public IFormFile? file { get; set; }
        public string MoTa { get; set; }
        public long Gia { get; set; }
        public string TenTacGia { get; set; }
        public Guid CategoryId { get; set; }
    }
}
