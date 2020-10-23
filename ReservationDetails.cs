using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingAlleyWeb.Models
{
    public class ReservationDetails
    {
        public int ReservationId { get; set; }
        public string EmpName { get; set; }
        public DateTime ReservedOn { get; set; }
        public int SlotId { get; set; }
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }
    }
}
