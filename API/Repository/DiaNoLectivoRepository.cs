using DesignAPI.Data;
using DesignAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using DesignAPI.Models.Entity;

namespace DesignAPI.Repository
{
    public class DiaNoLectivoRepository : IDiaNoLectivoRepository
    {
        private readonly TimeSpanConverter _context;
        private readonly IMemoryCache _cache;
        private readonly string CacheKey = "DiaNoLectivoCache";
        private readonly int CacheExpirationTime = 3600;

        public DiaNoLectivoRepository(TimeSpanConverter context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<ICollection<DiaNoLectivoEntity>> GetAllAsync()
        {
            if (_cache.TryGetValue(CacheKey, out ICollection<DiaNoLectivoEntity> cached))
                return cached;

            var fromDb = await _context.DiasNoLectivos.OrderBy(d => d.Fecha).ToListAsync();
            _cache.Set(CacheKey, fromDb, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(CacheExpirationTime)
            });

            return fromDb;
        }

        public async Task<DiaNoLectivoEntity> GetAsync(int id)
        {
            return await _context.DiasNoLectivos.FirstOrDefaultAsync(d => d.ID == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.DiasNoLectivos.AnyAsync(d => d.ID == id);
        }

        public async Task<bool> CreateAsync(DiaNoLectivoEntity entity)
        {
            _context.DiasNoLectivos.Add(entity);
            return await Save();
        }

        public async Task<bool> UpdateAsync(DiaNoLectivoEntity entity)
        {
            _context.DiasNoLectivos.Update(entity);
            return await Save();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
                return false;

            _context.DiasNoLectivos.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync() >= 0;
            if (saved)
                ClearCache();
            return saved;
        }

        public void ClearCache()
        {
            _cache.Remove(CacheKey);
        }
    }
}