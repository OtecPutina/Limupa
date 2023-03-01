using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limupa.Models
{
    public class Statistics
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        public int Number { get; set; }
        public string ImageUrl { get; set; }
    }
}
