using City_Vibe.Data;
using City_Vibe.Interfaces;
using City_Vibe.Repository;

namespace City_Vibe.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext appDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            appDbContext = applicationDbContext;
            AppointmentRepo = new AppointmentRepository(applicationDbContext);
            AppUserRepo = new AppUserRepository(applicationDbContext);
            CategoryRepo = new CategoryRepository(applicationDbContext);
            ClubCommentRepo = new ClubCommentRepository(applicationDbContext);
            ClubRepo = new ClubRepository(applicationDbContext);
            EventRepo = new EventRepository(applicationDbContext);
            CommentRepo = new CommentRepository(applicationDbContext);
            likeClubRepo = new likeClubRepository(applicationDbContext);

            SaveClubRepo = new SaveClubRepository(applicationDbContext, httpContextAccessor);
            SaveEventRepos = new SaveEventRepository(applicationDbContext , httpContextAccessor);
            DashboardRepo = new DashboardRepository(applicationDbContext, httpContextAccessor);

        }

        public IAppointmentRepository AppointmentRepo { get; private set; }

        public IAppUserRepository AppUserRepo { get; private set; }

        public ICategoryRepository CategoryRepo { get; private set; }

        public IClubCommentRepository ClubCommentRepo { get; private set; }

        public IClubRepository ClubRepo { get; private set; }

        public ICommentRepository CommentRepo { get; private set; }

        public IEventRepository EventRepo { get; private set; }

        public IlikeClubRepository likeClubRepo { get; private set; }

        public ISaveClubRepository SaveClubRepo { get; private set; }

        public ISaveEventRepository SaveEventRepos { get; private set; }

        public IDashboardRepository DashboardRepo { get; private set; }
   
        public int Save()
        {
            return appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            appDbContext.Dispose();
        }
    }
}
