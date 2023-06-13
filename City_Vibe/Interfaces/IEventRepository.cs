using City_Vibe.Models;

namespace City_Vibe.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<Event> GetByIdIncludeCategoryAndAddressAsync(int id);

        int CheckingTheExistenceOfAnAppointment(int currentEventId, string curUserId);

        Appointment ReplyAppointment(int currentEventId, string curUserId);

        IQueryable<Event> AllActiveEventIQueryable();

    }
}
