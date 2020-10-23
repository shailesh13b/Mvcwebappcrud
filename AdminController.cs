using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using BowlingAlleyDAL;
using BowlingAlleyDAL.Models;
using BowlingAlleyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingAlleyWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly BowlingAlleyDBContext _context;
        BowlingAlleyRepository rep;

        private readonly IMapper _mapper;
        public AdminController(BowlingAlleyDBContext context, IMapper mapper)
        {
            _context = context;
            rep = new BowlingAlleyRepository(context);
            _mapper = mapper;
            
        }
        public IActionResult GetReservationDetails()
        {
            var lstReservationDetails = rep.GetReservedSlots();
            List<Models.Reservations> reservedSlots = new List<Models.Reservations>();
            foreach (var detail in lstReservationDetails)
            {
                reservedSlots.Add(_mapper.Map<Models.Reservations>(detail));
            }
            return View(reservedSlots);
        }
        public IActionResult Approve(int reservationsId)
        {
            //BowlingAlleyRepository rep = new BowlingAlleyRepository();
            rep.ApproveOrReject(reservationsId, 1);
            return View();
        }
        public IActionResult Reject(int reservationId)
        {
            string statusMsg = null;
            var status = rep.ApproveOrReject(reservationId, -1);
            if (status == 1)
                statusMsg = "Success";
            else if (status == 0)
                statusMsg = "Failed";
            else
                statusMsg = "Error";
            ViewBag.statusMsg = statusMsg;
            return View();
        }
        public IActionResult GetRejectedSlots()
        {
            //BowlingAlleyRepository rep = new BowlingAlleyRepository();
            var lstAllRejectedSlots = rep.GetAllRejectedSlots();
            List<Models.ReservationDetails> lstRejectedSlots = new List<Models.ReservationDetails>();
            foreach (var rejectedSlot in lstAllRejectedSlots)
            {
                lstRejectedSlots.Add(_mapper.Map<Models.ReservationDetails>(rejectedSlot));
            }
            return View(lstRejectedSlots);
        }
    }
}
