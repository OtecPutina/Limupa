using Limupa.Context;
using Limupa.Models;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Helpers
{
    public class BannerService
    {
        public BannerService(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; }

        public async Task<List<Banner>> GetSettingsAsync()
        {
            return await _context.Banners.ToListAsync();
        }
    }
}
