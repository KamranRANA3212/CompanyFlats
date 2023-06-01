using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grosvenor.Portal.Test.Data
{
    public class BookingRepositoryTest:RepositoryBase
    {
        [Fact]
        public async Task ApproveBookingAsync()
        {
            //Arrange
            using var context = CreateContext();
            var repoistory = new BookingRepository(context);
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                BookingRequestId = Guid.NewGuid(),
                Status = Guid.NewGuid(),
                FlatId = Guid.NewGuid(),
                CreatedBy = Guid.NewGuid(),
                UpdatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            //Act
            await repoistory.ApproveBookingAsync(booking);

            //Assert
            using var testContext = CreateContext();
            Assert.True(testContext.Bookings.Any());

        }
        [Fact]
        public async Task CreateBookingRequestAsync()
        {
            //Arrange
            using var context = CreateContext();
            var repoistory = new BookingRepository(context);
            var bookingRequest = new BookingRequest
            {
                Id = Guid.NewGuid(),
                Title = "Test Title",
                Status = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                BookingType = Guid.NewGuid(),
                CostCenter="Cost Center",
                BookingNotes="This is testing notes",
                Amount=20,
                NumberOfFlats=1,
                CreatedBy = Guid.NewGuid(),
                UpdatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            //Act
            await repoistory.CreateBookingRequestAsync(bookingRequest);

            //Assert
            using var testContext = CreateContext();
            Assert.True(testContext.BookingRequests.Any());

        }
    }
}
