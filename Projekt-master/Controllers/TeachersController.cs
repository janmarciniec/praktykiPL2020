using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Praktyki.Models;

namespace Praktyki.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teachers.ToListAsync());
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }
    

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,FirstName,SecondName")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,FirstName,SecondName")] Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Search
        public IActionResult Search()
        {
            ViewData["Days"] = new SelectList(_context.Days, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search([FromForm] Teacher teacher, int days)
        {
            ViewData["Days"] = new SelectList(_context.Days, "Id", "Name");



            if (_context.Teachers.Any(x => x.FirstName == teacher.FirstName && x.SecondName == teacher.SecondName) == false)
            {
                ModelState.AddModelError("SecondName", $"Nauczyciel: {teacher.FirstName} {teacher.SecondName} nie istnieje.");
                return View(teacher);
            }

            var reservation = await _context.Teachers.Include(x => x.Reservations).ThenInclude(x => x.Group)
                   .Include(x => x.Reservations).ThenInclude(x => x.Day)
                   .Include(x => x.Reservations).ThenInclude(x => x.Room)
                   .Include(x => x.Reservations).ThenInclude(x => x.Hour)
                   .Include(x => x.Reservations).ThenInclude(x => x.Subject)
                   .Include(x => x.Reservations).ThenInclude(x => x.Teacher)
                   .Where(x => x.FirstName == teacher.FirstName && x.SecondName == teacher.SecondName)
                   .ToListAsync();

            var result = await (from t in _context.Teachers
                                join r in _context.Reservations on t.Id equals r.TeacherId
                                join d in _context.Days on r.DayId equals d.Id
                                join room in _context.Rooms on r.RoomId equals room.Id
                                join h in _context.Hours on r.RoomId equals h.Id
                                join s in _context.Subjects on r.RoomId equals s.Id
                                where t.FirstName == teacher.FirstName && t.SecondName == teacher.SecondName && d.Id == days
                                select t).ToListAsync();

            if (result.Count == 0)
            {
                ModelState.AddModelError("SecondName", $"Nauczyciel: {teacher.FirstName} {teacher.SecondName} nie ma w tym dniu zajęć.");
                return View(teacher);
            }

            return View("SearchResults", result);
        }


        // GET: Teacher/SearchResults
        public IActionResult SearchResults(List<Teacher> teachers)
        {                          
            if(teachers.Count() == 0)
            {
                return RedirectToAction(nameof(Search));
            }

            if (teachers.Count() != 0)
            {
                
                return View(teachers);
            }
            return RedirectToAction(nameof(Search));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
