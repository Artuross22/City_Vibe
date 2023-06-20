using City_Vibe.Application.Interfaces;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace City_Vibe.Infrastructure.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
       // private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext appDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {
            appDbContext = applicationDbContext;
            AppointmentRepository = new AppointmentRepository(applicationDbContext);
            AppUserRepository = new AppUserRepository(applicationDbContext);
            CategoryRepository = new CategoryRepository(applicationDbContext);
            ClubCommentRepository = new ClubCommentRepository(applicationDbContext);
            ClubRepository = new ClubRepository(applicationDbContext);
            EventRepository = new EventRepository(applicationDbContext);
            CommentRepository = new CommentRepository(applicationDbContext);
            LikeClubRepository = new likeClubRepository(applicationDbContext);
            RoleRepository = new RoleRepository(applicationDbContext);


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

        public IlikeClubRepository LikeClubRepository { get; private set; }

        public ISaveClubRepository SaveClubRepository { get; private set; }

        public ISaveEventRepository SaveEventRepository { get; private set; }

        public IDashboardRepository DashboardRepository { get; private set; }

        public IRoleRepository RoleRepository { get; private set; }

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
