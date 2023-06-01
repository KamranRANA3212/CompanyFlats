﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grosvenor.Portal.Model
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid BookingRequestId { get; set; }
        public Guid Status { get; set; }
        public Guid FlatId { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }



    }
}
