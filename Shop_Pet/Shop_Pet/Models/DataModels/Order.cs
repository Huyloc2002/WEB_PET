namespace Shop_Pet.Models.DataModels
{
    public class Order
    {
        public List<Cart> Carts { get; set; }
        public Account Account { get; set; }
    }
}
