using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using $safeprojectname$.Data;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly YomanContext _context;

        public MeetingsController(YomanContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            ViewData["Date"] = startDate;
            ViewData["Date"] = endDate;

            var yomanContext = _context.Meeting.Include(m => m.MType);

            // The Date Filterring

            if (startDate != null && endDate != null)
            {
                var all = from m in _context.Meeting
                          where m.MeetingDate <= endDate && m.MeetingDate >= startDate
                          select m;
                return View(await all.AsNoTracking().ToListAsync());
            }

            if (startDate != null || endDate != null)
            {
                var all = from m in _context.Meeting
                          where m.MeetingDate <= endDate || m.MeetingDate >= startDate
                          select m;
                return View(await all.AsNoTracking().ToListAsync());
            }

            return View(await yomanContext.ToListAsync());
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .Include(m => m.MType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            MTypeDropDownList();
            // ViewData["MTypeID"] = new SelectList(_context.MType, "Id", "Id");
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MeetingDate,StartTime,EndTime,Day,MTypeID,Importancy")] Meeting meeting)
        {
            var meetings = from m in _context.Meeting
                           where m.MeetingDate == meeting.MeetingDate
                           select m;
            foreach (var s in meetings)
            {
                if (meeting.StartTime < s.EndTime && meeting.StartTime < s.StartTime)
                {
                    throw new ArithmeticException("הפגישה שניסית להזין חופפת לפגישה אחרת ועל כן לא ניתן להזין פגישה זו");
                }

                else if (meeting.EndTime < s.EndTime && meeting.StartTime < s.StartTime)
                {
                    throw new ArithmeticException("הפגישה שניסית להזין חופפת לפגישה אחרת ועל כן לא ניתן להזין פגישה זו");
                }

                else if (meeting.EndTime > s.EndTime && meeting.StartTime < s.EndTime)
                {
                    throw new ArithmeticException("הפגישה שניסית להזין חופפת לפגישה אחרת ועל כן לא ניתן להזין פגישה זו");
                }
            }

            if (!meeting.Importancy.Equals("חשובה מאוד"))
            {
                if (meeting.MeetingDate.DayOfWeek == DayOfWeek.Friday)
                {
                    throw new ArithmeticException("לא ניתן לקבוע פגישות שאינן חשובות מאוד בימי שישי ושבת");
                }

                if (meeting.MeetingDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    throw new ArithmeticException("לא ניתן לקבוע פגישות שאינן חשובות מאוד בימי שישי ושבת");
                }
            }
            else
            {
                if (meeting.MeetingDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    throw new ArithmeticException("לא ניתן לקבוע פגישות בימי שבת");
                }
            }

            // Checks if the appointment time duration is more than 2 hours in case of unImportant appointment

            TimeSpan time = new TimeSpan(2, 0, 0);

            var dur = meeting.EndTime - meeting.StartTime;

            if (meeting.Importancy == "לא חשובה")
            {
                if (time < dur)
                {
                    throw new ArithmeticException("פגישה לא חשובה לא יכולה להיערך יותר משעתיים!");
                }
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // ViewData["MTypeID"] = new SelectList(_context.MType, "Id", "Id", meeting.MTypeID);
            MTypeDropDownList(meeting.MTypeID);
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var meeting = await _context.Meeting.AsNoTracking().FirstOrDefaultAsync(m => m.MTypeID == id);
            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            // ViewData["MTypeID"] = new SelectList(_context.MType, "Id", "Id", meeting.MTypeID);
            MTypeDropDownList(meeting.MTypeID);
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MeetingDate,StartTime,EndTime,Day,MTypeID,Importancy")] Meeting meeting)
        {
            var meetings = from m in _context.Meeting
                           where m.MeetingDate == meeting.MeetingDate
                           select m;
            foreach (var s in meetings)
            {
                if (meeting.StartTime < s.EndTime && meeting.StartTime < s.StartTime)
                {
                    throw new ArithmeticException("הפגישה שניסית להזין חופפת לפגישה אחרת ועל כן לא ניתן להזין פגישה זו");
                }

                else if (meeting.EndTime < s.EndTime && meeting.StartTime < s.StartTime)
                {
                    throw new ArithmeticException("הפגישה שניסית להזין חופפת לפגישה אחרת ועל כן לא ניתן להזין פגישה זו");
                }

                else if (meeting.EndTime > s.EndTime && meeting.StartTime < s.EndTime)
                {
                    throw new ArithmeticException("הפגישה שניסית להזין חופפת לפגישה אחרת ועל כן לא ניתן להזין פגישה זו");
                }
            }

            if (!meeting.Importancy.Equals("חשובה מאוד"))
            {
                if (meeting.MeetingDate.DayOfWeek == DayOfWeek.Friday)
                {
                    throw new ArithmeticException("לא ניתן לקבוע פגישות שאינן חשובות מאוד בימי שישי ושבת");
                }

                if (meeting.MeetingDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    throw new ArithmeticException("לא ניתן לקבוע פגישות שאינן חשובות מאוד בימי שישי ושבת");
                }
            }
            else
            {
                if (meeting.MeetingDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    throw new ArithmeticException("לא ניתן לקבוע פגישות בימי שבת");
                }
            }

            // Checks if the appointment time duration is more than 2 hours in case of unImportant appointment

            TimeSpan time = new TimeSpan(2, 0, 0);

            var dur = meeting.EndTime - meeting.StartTime;

            if (meeting.Importancy == "לא חשובה")
            {
                if (time < dur)
                {
                    throw new ArithmeticException("הפגישה שניסית להזין חופפת לפגישה אחרת ועל כן לא ניתן להזין פגישה זו");
                }
            }


            if (id != meeting.Id)
            {
                return NotFound();
            }

            var meetingToUpdate = await _context.Meeting
                .FirstOrDefaultAsync(c => c.MTypeID == id);

            if (await TryUpdateModelAsync<Meeting>(meetingToUpdate,
                "",
                c => c.Name, c => c.MeetingDate, c => c.StartTime, c => c.EndTime, c => c.Day, c => c.MTypeID, c => c.Importancy))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.Id))
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
            // ViewData["MTypeID"] = new SelectList(_context.MType, "Id", "Id", meeting.MTypeID);
            MTypeDropDownList(meetingToUpdate.MTypeID);
            return View(meetingToUpdate);
        }

        private void MTypeDropDownList(object selectedType = null)
        {
            var typeQuery = from d in _context.MType
                            orderby d.MeetingType
                            select d;
            ViewBag.MTypeID = new SelectList(typeQuery.AsNoTracking(), "Id", "MeetingType", selectedType);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .Include(m => m.MType).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _context.Meeting.FindAsync(id);
            _context.Meeting.Remove(meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meeting.Any(e => e.Id == id);
        }
    }
}
