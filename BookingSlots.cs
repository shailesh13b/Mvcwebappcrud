using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingAlleyWeb.Models
{
    public class BookingSlots
    {
        public BookingSlots()
        { }
        public int SlotId { get; set; }
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }

        public ICollection<Reservations> Reservations { get; set; }
    }
}
