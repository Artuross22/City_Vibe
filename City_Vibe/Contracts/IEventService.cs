using City_Vibe.Domain.Models;
using City_Vibe.Services.Base;
using City_Vibe.ViewModels.EventController;

namespace City_Vibe.Contracts
{
    public interface IEventService
    {
        Task<EventFilterViewModel> Index(int? category, string? name);

        Task<EventDetailViewModel> DetailEvent(int currentEventId);

        CreateEventViewModel CreateEventGet(int? clubId);

        Task<Response> CreateEventPost(CreateEventViewModel eventVM);

        Task<EditEventViewModel> EditGet(int id);

        Task<Response> EditPost(EditEventViewModel eventVM);

        Task<Event> DeleteGet(int id);

        Task<Response> DeleteEventPost(int id);

        Task<Response> AddInterestingEvent(int eventId);

        TopUserEventsViewModel EventsSelectByTheUser();
    }
}
