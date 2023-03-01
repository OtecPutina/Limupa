using System.ComponentModel.DataAnnotations;

namespace Limupa.Models
{
    public class Position
    {
        public int Id { get; set; }
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
    }
}
