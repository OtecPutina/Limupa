namespace Limupa.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SalePrice { get; set; }
        public int DiscountPercent { get; set; }
        public int Count { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }    
    }
}
