using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public class BookingManager : IBookingManager
    {
        public readonly IBookingRepository bookingRepository;
        public BookingManager(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public Task<Booking> CreateBookingAsync(Booking booking)
        {
            booking.StartDate = new DateTime(2023,1,1,15,0,0);
            booking.EndDate = new DateTime(2023, 1, 1, 10, 0, 0); 
            throw new NotImplementedException();
        }
    }
}
