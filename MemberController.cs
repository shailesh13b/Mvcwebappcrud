using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BowlingAlleyDAL;
using BowlingAlleyDAL.Models;
using BowlingAlleyWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BowlingAlleyWeb.Controllers
{
    public class MemberController : Controller
    {
        private readonly BowlingAlleyDBContext _context;
        BowlingAlleyRepository rep;

        private readonly IMapper _mapper;
        public MemberController(BowlingAlleyDBContext context, IMapper mapper)
        {
            _context = context;
            rep = new BowlingAlleyRepository(context);
            _mapper = mapper;

        }
        public IActionResult GetFreeSlots()
        {
            var lstDalbookingSlots = rep.GetFreeSlots();
            List<Models.BookingSlots> bookingSlots = new List<Models.BookingSlots>();
            foreach (var slot in lstDalbookingSlots)
            {
                bookingSlots.Add(_mapper.Map<Models.BookingSlots>(slot));
            }
            return View(bookingSlots);
        }
        public IActionResult BookSlots(Models.BookingSlots slot)
        {
            return View(slot);
        }
        public IActionResult ConfirmBooking(IFormCollection frm)
        {
            int slotId = Convert.ToInt32(frm["slotId"]);
            int empId = Convert.ToInt32(frm["empId"]);
            int reservationId=0;
            reservationId = rep.BookSlots(slotId,empId);
            return View();
        }
    }
}
