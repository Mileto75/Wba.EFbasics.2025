namespace Wba.EFbasics.Web.ViewModels
{
    public class CartIndexViewModel
    {
        //list of cartitems
        public List<CartItemModel> CartItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }
    }
}
