using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Model
{
    public class BookingRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid BookingType { get; set; }
        public Guid Status { get; set; }
        public string CostCenter { get; set; }
        public string BookingNotes { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfFlats { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
