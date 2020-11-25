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
    public class MTypesController : Controller
    {
        private readonly YomanContext _context;

        public MTypesController(YomanContext context)
        {
            _context = context;
        }

        // GET: MTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MType.ToListAsync());
        }

        // GET: MTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mType = await _context.MType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mType == null)
            {
                return NotFound();
            }

            return View(mType);
        }

        // GET: MTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MeetingType")] MType mType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mType);
        }

        // GET: MTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mType = await _context.MType.FindAsync(id);
            if (mType == null)
            {
                return NotFound();
            }
            return View(mType);
        }

        // POST: MTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MeetingType")] MType mType)
        {
            if (id != mType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MTypeExists(mType.Id))
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
            return View(mType);
        }

        // GET: MTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mType = await _context.MType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mType == null)
            {
                return NotFound();
            }

            return View(mType);
        }

        // POST: MTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mType = await _context.MType.FindAsync(id);
            _context.MType.Remove(mType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MTypeExists(int id)
        {
            return _context.MType.Any(e => e.Id == id);
        }
    }
}
