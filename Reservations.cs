using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingAlleyWeb.Models
{
    public class Reservations
    {
        public int ReservationId { get; set; }
        public int ReservedBy { get; set; }
        public DateTime ReservedOn { get; set; }
        public int? Status { get; set; }
        public int SlotId { get; set; }

        public Roles ReservedByNavigation { get; set; }
        public BookingSlots Slot { get; set; }
    }
}
