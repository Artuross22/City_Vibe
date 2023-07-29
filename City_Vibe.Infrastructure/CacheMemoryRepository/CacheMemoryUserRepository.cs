using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;
using Microsoft.Extensions.Caching.Memory;

namespace City_Vibe.Infrastructure.CacheRepository
{
    public class CacheMemoryUserRepository :  GenericRepository<AppUser>,  IAppUserRepository
    {
        private readonly IAppUserRepository decorated;
        private readonly IMemoryCache cache;

        public CacheMemoryUserRepository( 
            ApplicationDbContext applicationDbContext,
            IMemoryCache memoryCache,
            IAppUserRepository appUserRepository) : base(applicationDbContext)

        {
            decorated = appUserRepository;
            cache = memoryCache;

        }

        public Task<AppUser> GetUserByIdIncludeAdress(string id)
        {
            string key = $"member - {id}";

            return cache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                    return decorated.GetUserByIdIncludeAdress(id);
                });
        }

        public IQueryable<AppUser> GetAllUsersByIQueryable(string role)
        {
            return decorated.GetAllUsersByIQueryable(role);
        }


        public Task<IEnumerable<AppUser>> GetUsersByRole(string role)
        {
            return decorated.GetUsersByRole(role);
        }
    }
}
