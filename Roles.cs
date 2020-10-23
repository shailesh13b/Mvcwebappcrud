using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingAlleyWeb.Models
{
    public class Roles
    {
        public Roles()
        { }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public bool RoleType { get; set; }

        public ICollection<Reservations> Reservations { get; set; }
    }
}
