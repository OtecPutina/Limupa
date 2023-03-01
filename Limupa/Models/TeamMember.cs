using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limupa.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(300)]
        public string FacebookLink { get; set; }
        [StringLength(300)]
        public string TwitterLink { get; set; }
        [StringLength(300)]
        public string LinkedinLink { get; set; }
        [StringLength(300)] 
        public string GooglePlusLink { get; set; }
        public Position? Position { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}
