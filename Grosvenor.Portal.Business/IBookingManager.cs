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
        Task<Booking> CreateBookingAsync(Booking booking);
    }
}
