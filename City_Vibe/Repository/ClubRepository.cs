using City_Vibe.Data;
using City_Vibe.Implement;
using City_Vibe.Interfaces;
using City_Vibe.Models;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace City_Vibe.Repository
{
    public class ClubRepository : GenericRepository<Club>, IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Club>> GetAll()
        //{
        //    return await _context.Club.ToListAsync();
        //}

        public async Task<Club> GetByIdIncludeAddressAsync(int id)
        {
            return await _context.Club.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Club.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Club.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task< IEnumerable<Event>> GetClubsByEventId(int id)
        {
            var result = await _context.Events.Where(c => c.ClubId == id).Include(x => x.Address).Include( x=> x.Category).ToListAsync();
            return result;
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Club.CountAsync();
        }

        //public bool Add(Club club)
        //{
        //    _context.Add(club);
        //    return Save();
        //}

        //public bool Delete(Club club)
        //{
        //    _context.Remove(club);
        //    return Save();
        //}

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

        //public bool Update(Club club)
        //{
        //    _context.Update(club);
        //    return Save();
        //}

        public async Task<IEnumerable<PostInfoInClub>> GetPostInfoInClubByClubId(int id)
        {
         return await _context.PostInfoInClub.Where(x => x.ClubId == id).ToListAsync();

        }

        public async Task<PostInfoInClub> FindByIdPostInfo(int id)
        {
            return await _context.PostInfoInClub.FindAsync(id);
        }

        public async Task<IEnumerable<Club>> GetSliceAsync(int offset, int size)
        {
            return await _context.Club.Include(i => i.Address).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<int> GetCountByCategoryAsync(Category category)
        {
            return await _context.Club.CountAsync(c => c.Category == category);
        }

        public async Task<IEnumerable<Club>> GetClubsByCategoryAndSliceAsync(Category category, int offset, int size)
        {
            return await _context.Club
                .Include(i => i.Address)
                .Where(c => c.Category == category)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }


    }
}
