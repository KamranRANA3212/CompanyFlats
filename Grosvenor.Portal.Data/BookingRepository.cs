using Grosvenor.Portal.Data.Context;
using Grosvenor.Portal.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data
{
    public class BookingRepository : IBookingRepository
    {
        public readonly PortalContext context;
        public BookingRepository(PortalContext context)
        {
            this.context = context;
        }

        public async Task<Booking> ApproveBookingAsync(Booking booking)
        {
          await context.Bookings.AddAsync(booking);
          await context.SaveChangesAsync();
          return booking;
        }

        public async Task<BookingRequest> CreateBookingRequestAsync(BookingRequest booking)
        {
            await context.BookingRequests.AddAsync(booking);
            await context.SaveChangesAsync();
            return booking;
            
        }

        public async Task<List<Booking>> GetApprovedBookingsAsync()
        {
            var approvedGuid = Guid.Parse("abc09809d");
            var approvedBooking= await context.Bookings.Where(x=>x.Status==approvedGuid).ToListAsync();
            return approvedBooking;

        }
        public async Task<List<BookingRequest>> GetPendingBookingsAsync()
        {
            var pendingGuid = Guid.Parse("uiidsad098");
            var pendingBooking = await context.BookingRequests.Where(x => x.Status == pendingGuid).ToListAsync();
            return pendingBooking;

        }
        public async Task<List<BookingRequest>> GetRejectedBookingsAsync()
        {
            var rejectedGuid = Guid.Parse("uiidsad07dsf8");
            var rejectedBooking = await context.BookingRequests.Where(x => x.Status == rejectedGuid).ToListAsync();
            return rejectedBooking;

        }
    }
}
