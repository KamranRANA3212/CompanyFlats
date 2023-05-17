using Grosvenor.Portal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Data
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBookingAsync(Booking booking);
    }
}
