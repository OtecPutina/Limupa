using Limupa.Context;
using Limupa.Models;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Helpers
{
    public class SettingService
    {
        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; }

        public async Task<List<Setting>> GetSettingsAsync()
        {
            return await _context.Settings.ToListAsync();
        }
    }       
}
