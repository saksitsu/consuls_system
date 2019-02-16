using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CONSULS_SYSTEM.DATA;
using CONSULS_SYSTEM.Models;

namespace CONSULS_SYSTEM.Controllers
{
    public class MT_LevelController : Controller
    {
        private readonly CONSULS_Context _context;

        public MT_LevelController(CONSULS_Context context)
        {
            _context = context;
        }

        // GET: MT_Level
        public async Task<IActionResult> Index()
        {
            return View(await _context.MT_Level.ToListAsync());
        }

        // GET: MT_Level/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_Level = await _context.MT_Level
                .FirstOrDefaultAsync(m => m.Level_ID == id);
            if (mT_Level == null)
            {
                return NotFound();
            }

            return View(mT_Level);
        }

        // GET: MT_Level/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MT_Level/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level_ID,Name,Active")] MT_Level mT_Level)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mT_Level);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mT_Level);
        }

        // GET: MT_Level/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_Level = await _context.MT_Level.FindAsync(id);
            if (mT_Level == null)
            {
                return NotFound();
            }
            return View(mT_Level);
        }

        // POST: MT_Level/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level_ID,Name,Active")] MT_Level mT_Level)
        {
            if (id != mT_Level.Level_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mT_Level);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MT_LevelExists(mT_Level.Level_ID))
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
            return View(mT_Level);
        }

        // GET: MT_Level/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_Level = await _context.MT_Level
                .FirstOrDefaultAsync(m => m.Level_ID == id);
            if (mT_Level == null)
            {
                return NotFound();
            }

            return View(mT_Level);
        }

        // POST: MT_Level/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mT_Level = await _context.MT_Level.FindAsync(id);
            _context.MT_Level.Remove(mT_Level);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MT_LevelExists(int id)
        {
            return _context.MT_Level.Any(e => e.Level_ID == id);
        }
    }
}
