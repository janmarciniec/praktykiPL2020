using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Praktyki.Models;

namespace Praktyki.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.Group).Include(r => r.Hour).Include(r => r.Room).Include(r => r.Subject).Include(r => r.Teacher).Include(r => r.Day);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/RoomReservations/id
        public async Task<IActionResult> RoomReservations(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            /*var applicationDbContext = _context.Reservations.Include(r => r.Group)
                .Include(r => r.Hour).Include(r => r.Room).Include(r => r.Subject)
                .Include(r => r.Teacher).Where(r => r.Room.Id == id);*/

            var room = await _context.Rooms.Include(x => x.Reservations).ThenInclude(x => x.Group)
                 .Include(x => x.Reservations).ThenInclude(x => x.Hour)
                 .Include(x => x.Reservations).ThenInclude(x => x.Subject)
                 .Include(x => x.Reservations).ThenInclude(x => x.Teacher)
                 .Include(x => x.Reservations).ThenInclude(x => x.Day)
                 .Where(x => x.Id == id).ToListAsync();


            return View(room);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Room.Id == id);


            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            ViewData["HourId"] = new SelectList(_context.Hours, "Id", "Hours");
            ViewData["RoomId"] = id;
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["DayId"] = new SelectList(_context.Days, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname");
            return View(reservation);
        }


        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId, DayId, HourId,TeacherId,GroupId,SubjectId")] Reservation reservation)
        {
            ViewData["RoomId"] = reservation.RoomId;
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", reservation.GroupId);
            ViewData["HourId"] = new SelectList(_context.Hours, "Id", "Hours", reservation.HourId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", reservation.SubjectId);
            ViewData["DayId"] = new SelectList(_context.Days, "Id", "Name", reservation.DayId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", reservation.TeacherId);

            if (_context.Reservations.Any(x => x.DayId == reservation.DayId && x.HourId == reservation.HourId && x.RoomId == reservation.RoomId))
            {
                ModelState.AddModelError("DayId", "W ten dzień o tej godzinie odbywają się zajęcia w tej sali");
                ModelState.AddModelError("HourId", "W ten dzień o tej godzinie odbywają się zajęcia w tej sali");
            }

            if (_context.Reservations.Any(x => x.TeacherId == reservation.TeacherId && x.DayId == reservation.DayId && x.HourId == reservation.HourId))
            {
                ModelState.AddModelError("TeacherId", "Ten prowadzący jest zajęty w ten dzień o tej godzinie.");
            }

            if (_context.Reservations.Any(x => x.GroupId == reservation.GroupId && x.DayId == reservation.DayId && x.HourId == reservation.HourId))
            {
                ModelState.AddModelError("GroupId", "Ta grupa jest zajęta w ten dzień o tej godzinie.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            return View(reservation);
        }


        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", reservation.GroupId);
            ViewData["HourId"] = new SelectList(_context.Hours, "Id", "Hours", reservation.HourId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Nr", reservation.RoomId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", reservation.SubjectId);
            ViewData["DayId"] = new SelectList(_context.Days, "Id", "Name", reservation.DayId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", reservation.TeacherId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DayId,RoomId,HourId,TeacherId,GroupId,SubjectId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", reservation.GroupId);
            ViewData["HourId"] = new SelectList(_context.Hours, "Id", "Hours", reservation.HourId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Nr", reservation.RoomId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", reservation.SubjectId);
            ViewData["DayId"] = new SelectList(_context.Days, "Id", "Name", reservation.DayId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Fullname", reservation.TeacherId);

            if (_context.Reservations.Any(x => x.DayId == reservation.DayId && x.HourId == reservation.HourId && x.RoomId == reservation.RoomId))
            {
                ModelState.AddModelError("DayId", "W ten dzień o tej godzinie odbywają się zajęcia w tej sali");
                ModelState.AddModelError("HourId", "W ten dzień o tej godzinie odbywają się zajęcia w tej sali");
            }
            if (_context.Reservations.Any(x => x.TeacherId == reservation.TeacherId && x.DayId == reservation.DayId && x.HourId == reservation.HourId))
            {
                ModelState.AddModelError("TeacherId", "Ten prowadzący jest zajęty w ten dzień o tej godzinie.");
            }
            if (_context.Reservations.Any(x => x.GroupId == reservation.GroupId && x.DayId == reservation.DayId && x.HourId == reservation.HourId))
            {
                ModelState.AddModelError("GroupId", "Ta grupa jest zajęta w ten dzień o tej godzinie.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Group)
                .Include(r => r.Hour)
                .Include(r => r.Room)
                .Include(r => r.Subject)
                .Include(r => r.Teacher)
                .Include(r => r.Day)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
     
    }
}
