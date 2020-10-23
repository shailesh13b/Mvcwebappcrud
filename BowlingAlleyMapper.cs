using AutoMapper;
using BowlingAlleyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingAlleyWeb.Mapper
{
    public class BowlingAlleyMapper : Profile
    {
        public BowlingAlleyMapper()
        {
            CreateMap<Reservations, Models.Reservations>();
            CreateMap<ReservationDetails, Models.ReservationDetails>();
            CreateMap<BookingSlots,Models.BookingSlots>();
        }
    }
}
