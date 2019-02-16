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
    public class TB_Menu_InformationController : Controller
    {
        private readonly CONSULS_Context _context;

        public TB_Menu_InformationController(CONSULS_Context context)
        {
            _context = context;
        }

        // GET: TB_Menu_Information
        public async Task<IActionResult> Index()
        {
            return View(await _context.TB_Menu_Information.ToListAsync());
        }

        // GET: TB_Menu_Information/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_Menu_Information = await _context.TB_Menu_Information
                .FirstOrDefaultAsync(m => m.Menu_ID == id);
            if (tB_Menu_Information == null)
            {
                return NotFound();
            }

            return View(tB_Menu_Information);
        }

        // GET: TB_Menu_Information/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TB_Menu_Information/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Menu_ID,Name,Action,Controller,Sorting,ChildOfMenu,Active,Createby,CreateDate")] TB_Menu_Information tB_Menu_Information)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tB_Menu_Information);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tB_Menu_Information);
        }

        // GET: TB_Menu_Information/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_Menu_Information = await _context.TB_Menu_Information.FindAsync(id);
            if (tB_Menu_Information == null)
            {
                return NotFound();
            }
            return View(tB_Menu_Information);
        }

        // POST: TB_Menu_Information/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Menu_ID,Name,Action,Controller,Sorting,ChildOfMenu,Active,Createby,CreateDate")] TB_Menu_Information tB_Menu_Information)
        {
            if (id != tB_Menu_Information.Menu_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tB_Menu_Information);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TB_Menu_InformationExists(tB_Menu_Information.Menu_ID))
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
            return View(tB_Menu_Information);
        }

        // GET: TB_Menu_Information/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_Menu_Information = await _context.TB_Menu_Information
                .FirstOrDefaultAsync(m => m.Menu_ID == id);
            if (tB_Menu_Information == null)
            {
                return NotFound();
            }

            return View(tB_Menu_Information);
        }

        // POST: TB_Menu_Information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var tB_Menu_Information = await _context.TB_Menu_Information.FindAsync(id);
            _context.TB_Menu_Information.Remove(tB_Menu_Information);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TB_Menu_InformationExists(string id)
        {
            return _context.TB_Menu_Information.Any(e => e.Menu_ID == id);
        }
    }
}
