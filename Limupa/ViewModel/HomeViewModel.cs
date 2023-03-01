using Limupa.Models;

namespace Limupa.ViewModel
{
    public class HomeViewModel
    {
        public List<Setting> Settings { get; set; }    
        public List<Slider> Sliders  { get; set; }    
        public List<Statistics> Statistics  { get; set; }    
        public List<TeamMember> TeamMembers { get; set; }
        public List<Position> Positions { get; set; }
        public List<Banner> Banners { get; set; }
        public List<BigBanner> BigBanners { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Product> Products { get; set; }
    }
}
