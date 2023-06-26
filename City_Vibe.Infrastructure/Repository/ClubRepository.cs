using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Infrastructure.Repository
{
    public class ClubRepository : GenericRepository<Club>, IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Club>> GetClubsByCategoryAndSliceAsync(Category category, int offset, int size)
        {
            var pageOffset = (1 - offset) * size;

            return await _context.Club
                .Include(i => i.Address)
                .Where(c => c.Category == category)
                .Skip(pageOffset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Club.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }
  

        public async Task<int> GetCountAsync()
        {
            return await _context.Club.CountAsync();
        }

        public async Task<IEnumerable<PostInfoInClub>> GetPostInfoInClubByClubId(int id)
        {
         return await _context.PostInfoInClub.Where(x => x.ClubId == id).ToListAsync();

        }

        public async Task<PostInfoInClub> FindByIdPostInfo(int id)
        {
            return await _context.PostInfoInClub.FindAsync(id);
        }

        public async Task<IEnumerable<Club>> GetSliceAsync(int page , int offset, int size)
        {
            var result = (  page - 1 ) * offset;
            return await _context.Club.Include(i => i.Address).Skip(result).Take(size).ToListAsync();
        }

        public async Task<int> GetCountByCategoryAsync(Category category)
        {
            return await _context.Club.CountAsync(c => c.Category == category);
        }

        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Club.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }


        public bool AddPostInfoInClub(PostInfoInClub postInfoInClub)
        {
            _context.Add(postInfoInClub);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
