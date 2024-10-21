namespace Shop_Pet.Models.DataModels
{
    public class Cart
    {
        public int Id { get; set; }
        public string Tenpet { get; set; }
        public string Images { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public double Thanhtien => Quantity * Price;
    }
}
