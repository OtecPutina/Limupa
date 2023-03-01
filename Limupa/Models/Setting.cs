using System.ComponentModel.DataAnnotations;

namespace Limupa.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Key { get; set; }
        [StringLength(100)]
        public string Value { get; set; }
    }
}
