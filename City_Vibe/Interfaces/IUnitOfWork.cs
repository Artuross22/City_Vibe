namespace City_Vibe.Interfaces
{
    public interface IUnitOfWork :  IDisposable
    {
        IAppointmentRepository AppointmentRepository {get;}

        IAppUserRepository AppUserRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IClubCommentRepository ClubCommentRepository { get; }

        IClubRepository ClubRepository { get; }

        ICommentRepository CommentRepository { get; }

        IDashboardRepository DashboardRepository { get; }

        IEventRepository EventRepository { get; }

        IlikeClubRepository likeClubRepository { get; }

        ISaveClubRepository SaveClubRepository { get; }

        ISaveEventRepository SaveEventRepository { get;}

        int Save();
    }
}
