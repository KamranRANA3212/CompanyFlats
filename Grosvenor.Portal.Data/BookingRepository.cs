using Grosvenor.Portal.Data.Context;
using Grosvenor.Portal.Model;
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

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            var book= await context.Bookings.AddAsync(booking);
            return book;
        }
    }
}
