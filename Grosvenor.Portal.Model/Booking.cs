using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Model
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string RequesterPhone { get; set; }
        public string HostName { get; set; }
        public string HostEmail { get; set; }
        public string HostPhone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Lookup BookingType { get; set; }
        public int LookUpId { get; set; }
        public string CostCenter { get; set; }
        public string BookingNotes { get; set; }


    }
}
