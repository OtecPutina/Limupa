using System.ComponentModel.DataAnnotations.Schema;

namespace Limupa.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
