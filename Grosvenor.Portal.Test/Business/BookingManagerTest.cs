using Grosvenor.Portal.Business;
using Grosvenor.Portal.Data;
using Grosvenor.Portal.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Grosvenor.Portal.Test.Business
{
    public class BookingManagerTest
    {
      
            private readonly IBookingManager manager;
            private readonly Mock<IBookingRepository> repository;
            public BookingManagerTest()
            {
                this.repository = new Mock<IBookingRepository>();
                this.manager = new BookingManager(this.repository.Object);
            }

            [Fact]
            public async Task CreateBookingAsync()
            {
                BookingRequest request = new BookingRequest();
                //Arrange
                this.repository.Setup(s => s.CreateBookingRequestAsync(request)).ReturnsAsync( new BookingRequest
                {
                    Id = Guid.NewGuid(),
                    Title = "Test Title",
                    Status = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    BookingType = Guid.NewGuid(),
                    CostCenter = "Cost Center",
                    BookingNotes = "This is testing notes",
                    Amount = 20,
                    NumberOfFlats = 1,
                    CreatedBy = Guid.NewGuid(),
                    UpdatedBy = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });

                //Act
                BookingRequest bookingRequest = new BookingRequest();
                var response = await this.manager.CreateBookingRequestAsync(bookingRequest);

                //Assert
                this.repository.Verify(r => r.CreateBookingRequestAsync(request));
                Assert.NotNull(response);
                Assert.Equal(bookingRequest, response);
            }
        }
    
}

