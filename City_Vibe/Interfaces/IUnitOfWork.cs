namespace City_Vibe.Interfaces
{
    public interface IUnitOfWork :  IDisposable
    {
        IAppointmentRepository AppointmentRepo {get;}

        IAppUserRepository AppUserRepo { get; }

        ICategoryRepository CategoryRepo { get; }

        IClubCommentRepository ClubCommentRepo { get; }

        IClubRepository ClubRepo { get; }

        ICommentRepository CommentRepo { get; }

        IDashboardRepository DashboardRepo { get; }

        IEventRepository EventRepo { get; }

        IlikeClubRepository likeClubRepo { get; }

        ISaveClubRepository SaveClubRepo { get; }

        ISaveEventRepository SaveEventRepos { get;}
    }
}
