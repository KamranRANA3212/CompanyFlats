using Grosvenor.Portal.Business;
using Grosvenor.Portal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public readonly IBookingManager bookingManager;
        public BookingController(IBookingManager bookingManager)
        {
            this.bookingManager = bookingManager;
        }
        [HttpPost("AddBookingRequest")]
        
        public async Task<BookingRequest> AddBookingAsync(BookingRequest booking)
        {
            booking.CreatedBy = User.GetUserId();
            booking.UpdatedBy = User.GetUserId();
            BookingRequest bookingRequest = await bookingManager.CreateBookingRequestAsync(booking);
            if (bookingRequest == null) return null;
            return bookingRequest;
        }
        [HttpPost("ApproveRequest")]
        public async Task<Booking> ApproveRequest(Booking booking)
        {
            return await bookingManager.CreateApproveRequestAsync(booking);
        }
        [HttpGet("GetApprovedBooking")]
        public async Task<List<Booking>> GetApprovedBookingByStatus()
        {
            return await bookingManager.GetApprovedBookingsAsync();
        }
        [HttpGet("GetPendingBooking")]
        public async Task<List<BookingRequest>> GetRejectedBookingAsync()
        {
            return await bookingManager.GetRejectedBookingsAsync();
        }
        [HttpGet("GetRejectedBooking")]
        public async Task<List<BookingRequest>> GetPendingBookingAsync()
        {
            return await bookingManager.GetPendingBookingsAsync();
        }
    }
}
