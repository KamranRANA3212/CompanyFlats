using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Business
{
    public interface IBookingManager
    {
        Task<BookingRequest> CreateBookingRequestAsync(BookingRequest booking);
        Task<List<Booking>> GetApprovedBookingsAsync();
        Task<List<BookingRequest>> GetPendingBookingsAsync();
        Task<List<BookingRequest>> GetRejectedBookingsAsync();
        Task<Booking> CreateApproveRequestAsync(Booking booking);
    } 
}
