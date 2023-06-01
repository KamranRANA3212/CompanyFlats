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
       
        public async Task<Booking> CreateApproveRequestAsync(Booking booking)
        {
            booking.Id = Guid.NewGuid();
            booking.BookingRequestId= booking.Id;
            booking.Status = booking.Status;
            booking.FlatId= booking.FlatId;
            booking.CreatedAt= DateTime.Now;
            booking.UpdatedAt= DateTime.Now;
            return await bookingRepository.ApproveBookingAsync(booking);
        }
        public async Task<BookingRequest> CreateBookingRequestAsync(BookingRequest booking)
        {
            booking.Id = Guid.NewGuid();
            booking.CreatedAt = DateTime.Now;
            booking.UpdatedAt = DateTime.Now;
            booking.StartDate = booking.StartDate.Date.AddHours(15);
            booking.EndDate = booking.EndDate.Date.AddHours(10);
            booking.Status =Guid.Parse("28B63C39-AAFA-4EF3-BD44-2D56BB0F8DE5");
            TimeSpan duration = booking.EndDate - booking.StartDate;
            int countdays = (int)Math.Ceiling(duration.TotalDays);
            int weekendsCount = 0;
            for (DateTime date = booking.StartDate; date < booking.EndDate; date = date.AddDays(1))
            {
                if ((date.DayOfWeek == DayOfWeek.Saturday && (date.AddDays(1)).DayOfWeek == DayOfWeek.Sunday) ||
                    (date.DayOfWeek == DayOfWeek.Sunday && (date.AddDays(1)).DayOfWeek == DayOfWeek.Saturday))
                {
                    weekendsCount++;
                }
            }
            int days = countdays - weekendsCount;
            booking.Amount = days * 60;
            return await bookingRepository.CreateBookingRequestAsync(booking);
        }
        public async Task<List<Booking>> GetApprovedBookingsAsync()
        {
            return await bookingRepository.GetApprovedBookingsAsync();
        }
        public async Task<List<BookingRequest>> GetPendingBookingsAsync()
        {
            return await bookingRepository.GetRejectedBookingsAsync();
        }
        public async Task<List<BookingRequest>> GetRejectedBookingsAsync()
        {
            return await bookingRepository.GetRejectedBookingsAsync();
        }

      
    }
}
    