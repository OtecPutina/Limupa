using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limupa.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }    
        public List<OrderItem> OrderItems { get; set; }
        [NotMapped]
        public List<int> ProductIds { get; set; }
    }
}
