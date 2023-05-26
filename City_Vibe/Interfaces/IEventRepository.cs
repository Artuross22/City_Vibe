using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<Event> GetByIdIncludeCategoryAndAddressAsync(int id);

        Task<Event> GetByIdAsyncNoTracking(int id);

        Task<Event> GetByIdIncludeCommentsAsync(int id);

        Task<List<Event>> ActiveEventBytime();

        int CheckingTheExistenceOfAnAppointment(int currentEventId, string curUserId);

        Appointment ReplyAppointment(int currentEventId, string curUserId);

        IQueryable<Event> ActiveEventBytimeIQueryable();

        IQueryable<Event> ActiveEventAllIQueryable();

        bool Save();
    }
}
