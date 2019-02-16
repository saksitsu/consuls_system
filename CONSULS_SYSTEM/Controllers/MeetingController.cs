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
    public class MeetingController : Controller
    {
        private readonly CONSULS_Context _context;

        public MeetingController(CONSULS_Context context)
        {
            _context = context;
        }

        // GET: Meeting
        public async Task<IActionResult> Index()
        {
            return View(await _context.TB_Meeting.ToListAsync());
        }

        // GET: Meeting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_Meeting = await _context.TB_Meeting
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tB_Meeting == null)
            {
                return NotFound();
            }

            return View(tB_Meeting);
        }

        // GET: Meeting/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Meeting_ID,Meeting_Title,Meeting_Date,Active,Createby,CreateDate")] TB_Meeting tB_Meeting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tB_Meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tB_Meeting);
        }

        // GET: Meeting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_Meeting = await _context.TB_Meeting.FindAsync(id);
            if (tB_Meeting == null)
            {
                return NotFound();
            }
            return View(tB_Meeting);
        }

        // POST: Meeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Meeting_ID,Meeting_Title,Meeting_Date,Active,Createby,CreateDate")] TB_Meeting tB_Meeting)
        {
            if (id != tB_Meeting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tB_Meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TB_MeetingExists(tB_Meeting.ID))
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
            return View(tB_Meeting);
        }

        // GET: Meeting/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_Meeting = await _context.TB_Meeting
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tB_Meeting == null)
            {
                return NotFound();
            }

            return View(tB_Meeting);
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tB_Meeting = await _context.TB_Meeting.FindAsync(id);
            _context.TB_Meeting.Remove(tB_Meeting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TB_MeetingExists(int id)
        {
            return _context.TB_Meeting.Any(e => e.ID == id);
        }
    }
}
