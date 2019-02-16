using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CONSULS_SYSTEM.DATA;
using CONSULS_SYSTEM.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace CONSULS_SYSTEM.Controllers
{
    public class PartnerController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        IHostingEnvironment _hostingEnvironment;

        public PartnerController(CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Partner
        public async Task<IActionResult> Index()
        {
            return View(await _context.MT_Partner.ToListAsync());
        }
        public async Task<IActionResult> Manage()
        {
            return View(await _context.MT_Partner.ToListAsync());
        }

        // GET: Partner/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_Partner = await _context.MT_Partner
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mT_Partner == null)
            {
                return NotFound();
            }

            return View(mT_Partner);
        }

        // GET: Partner/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MT_Partner mT_Partner, List<IFormFile> uploadfile)
        {
            if (ModelState.IsValid)
            {
                string folder_type = "images";
                string folder_image = "Partner";
                string file_response = "", error_message = "";
                //ข้อมูล User
                string session_user = HttpContext.Session.GetString("session_user");
                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                mT_Partner.Createby = list_user.User_ID;//"aof";//User Session
                mT_Partner.CreateDate = DateTime.Now;
                //Save file รูป
                if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                {
                    mT_Partner.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                }
                else
                {
                    mT_Partner.Img_Url = "";
                    ModelState.AddModelError("Img_Url", error_message);
                    goto ExitError;
                }

                _context.Add(mT_Partner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }

            ExitError:;
            return View(mT_Partner);
        }

        // GET: Partner/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_Partner = await _context.MT_Partner.FindAsync(id);
            if (mT_Partner == null)
            {
                return NotFound();
            }
            return View(mT_Partner);
        }

        // POST: Partner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Img_Url,Link_Url,Active,Createby,CreateDate")] MT_Partner mT_Partner)
        {
            if (id != mT_Partner.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mT_Partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MT_PartnerExists(mT_Partner.ID))
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
            return View(mT_Partner);
        }

        // GET: Partner/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_Partner = await _context.MT_Partner
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mT_Partner == null)
            {
                return NotFound();
            }

            return View(mT_Partner);
        }

        // POST: Partner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mT_Partner = await _context.MT_Partner.FindAsync(id);
            _context.MT_Partner.Remove(mT_Partner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MT_PartnerExists(int id)
        {
            return _context.MT_Partner.Any(e => e.ID == id);
        }
    }
}
