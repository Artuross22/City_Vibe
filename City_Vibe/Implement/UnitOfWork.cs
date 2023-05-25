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
            AppointmentRepository = new AppointmentRepository(applicationDbContext);
            AppUserRepository = new AppUserRepository(applicationDbContext);
            CategoryRepository = new CategoryRepository(applicationDbContext);
            ClubCommentRepository = new ClubCommentRepository(applicationDbContext);
            ClubRepository = new ClubRepository(applicationDbContext);
            EventRepository = new EventRepository(applicationDbContext);
            CommentRepository = new CommentRepository(applicationDbContext);
            likeClubRepository = new likeClubRepository(applicationDbContext);

            SaveClubRepository = new SaveClubRepository(applicationDbContext, httpContextAccessor);
            SaveEventRepository = new SaveEventRepository(applicationDbContext , httpContextAccessor);
            DashboardRepository = new DashboardRepository(applicationDbContext, httpContextAccessor);
        }

        public IAppointmentRepository AppointmentRepository { get; private set; }

        public IAppUserRepository AppUserRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IClubCommentRepository ClubCommentRepository { get; private set; }

        public IClubRepository ClubRepository { get; private set; }

        public ICommentRepository CommentRepository { get; private set; }

        public IEventRepository EventRepository { get; private set; }

        public IlikeClubRepository likeClubRepository { get; private set; }

        public ISaveClubRepository SaveClubRepository { get; private set; }

        public ISaveEventRepository SaveEventRepository { get; private set; }

        public IDashboardRepository DashboardRepository { get; private set; }
   
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
