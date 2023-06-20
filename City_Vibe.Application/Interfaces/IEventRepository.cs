

using City_Vibe.Domain.Models;

namespace City_Vibe.Application.Interfaces
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<Event> GetByIdIncludeCategoryAndAddressAsync(int id);

        int CheckingTheExistenceOfAnAppointment(int currentEventId, string curUserId);

        Appointment ReplyAppointment(int currentEventId, string curUserId);

    }
}
