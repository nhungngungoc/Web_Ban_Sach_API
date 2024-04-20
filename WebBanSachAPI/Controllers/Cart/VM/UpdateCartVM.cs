namespace WebBanSachAPI.Controllers.Cart.VM
{
    public class UpdateCartVM
    {
        public Guid Id { get; set; }
        public string Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
