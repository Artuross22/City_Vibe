﻿using City_Vibe.Application.Interfaces;
using City_Vibe.Domain.Models;
using City_Vibe.Infrastructure.Data;
using City_Vibe.Infrastructure.Implement;
using Microsoft.EntityFrameworkCore;

namespace City_Vibe.Infrastructure.Repository
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext context;

        public EventRepository(ApplicationDbContext _context) : base(_context) => context = _context;
     
        public async Task<Event> GetByIdIncludeCategoryAndAddressAsync(int id)
        {
            return await context.Events.Include(i => i.Category).Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public int CheckingTheExistenceOfAnAppointment(int currentEventId, string currentUserId)
        {
            return context.Appointments.Where(x => x.AppUserId == currentUserId).Where(e => e.EventId == currentEventId).ToList().Count();            
        }

        public Appointment ReplyAppointment(int currentEventId, string currentUserId)
        {
            return context.Appointments.Include(x => x.ReplyAppointments).FirstOrDefault(x => x.AppUserId == currentUserId && x.EventId == currentEventId);         
        }
    }
}

