using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limupa.Models
{
    public class BigBanner
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int Price { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Title1 { get; set; }
        [StringLength(50)]
        public string Title2 { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
