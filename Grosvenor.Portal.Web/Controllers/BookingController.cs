using Grosvenor.Portal.Business;
using Grosvenor.Portal.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("AddBookings")]
        public async Task<Booking>  AddBookingAsync(Booking booking)
        {
            Booking book = await bookingManager.CreateBookingAsync(booking);
            if (book == null) return null;
            return book;
        }
    }
}
