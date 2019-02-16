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
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace CONSULS_SYSTEM.Controllers
{
    public class ContactUSController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        IHostingEnvironment _hostingEnvironment;
        string controller_menu = "ContactUS";

        public ContactUSController(CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: MasterContactUS
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("MyAlert") != null)
            {
                ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                HttpContext.Session.Remove("MyAlert");
            }

            var details_contact = _context.MT_ContactUS.Where(x => x.Active == true).FirstOrDefault();
            ViewData["details_contact"] = details_contact;

            if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
            {
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(TB_MessageToAdmin ms,bool IsMember, List<IFormFile> uploadfile)
        {
            if (ModelState.IsValid)
            {
                //Member
                if (IsMember)
                {
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    ms.Createby = list_user.User_ID;//"aof";//User Session
                    ms.CreateDate = DateTime.Now;
                    ms.Status = "N";//Wait
                }
                else//Non Member
                {
                    bool IsError = false;
                    if (ms.Email == null)
                    {
                        ModelState.AddModelError("Email", "This field is required");
                        IsError = true;
                    }

                    if (ms.Name == null)
                    {
                        ModelState.AddModelError("Name", "This field is required");
                        IsError = true;
                    }

                    if(IsError == true)
                    {
                        goto ExitError;
                    }

                    ms.Createby = "Non member";
                    ms.CreateDate = DateTime.Now;
                    ms.Status = "N";//Wait
                }
                ////Save file รูป
                //string folder_type = "images";
                //string folder_image = "Contact";
                //string file_response = "", error_message = "";
                //if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                //{
                //    ms.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                //}
                //else
                //{
                //    ms.Img_Url = "";
                //    ModelState.AddModelError("Img_Url", error_message);
                //    goto ExitError;
                //}
                _context.Add(ms);
                await _context.SaveChangesAsync();

                string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Send completed. Thank you for contact."));
                HttpContext.Session.SetString("MyAlert", json);

                return RedirectToAction(nameof(Index));
            }

            ExitError:;
            ViewBag.MyAlert = cls.MyAlert("false", "This field is required.");
            var details_contact = _context.MT_ContactUS.Where(x => x.Active == true).FirstOrDefault();
            ViewData["details_contact"] = details_contact;
            return View(ms);
        }
       
        // GET: MasterContactUS
        public async Task<IActionResult> Manage()
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                if (HttpContext.Session.GetString("MyAlert") != null)
                {
                    ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                    HttpContext.Session.Remove("MyAlert");
                }
                var model = await _context.MT_ContactUS.SingleOrDefaultAsync();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    MT_ContactUS mT = new MT_ContactUS();
                    return View(mT);
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Manage(MT_ContactUS mT_ContactUS)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                if (ModelState.IsValid)
                {
                    try
                    {
                        //ข้อมูล User
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        mT_ContactUS.Createby = list_user.User_ID;//"aof";//User Session
                        mT_ContactUS.CreateDate = DateTime.Now;
                        mT_ContactUS.Active = true;

                        if (MT_ContactUSExists(mT_ContactUS.ID))//True => สร้างไว้แล้ว
                        {
                            _context.Update(mT_ContactUS);
                        }
                        else//ยังไม่เคยสร้างไว้
                        {
                            _context.Add(mT_ContactUS);
                        }
                        await _context.SaveChangesAsync();

                        ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");

                        //Save Log
                        cls.Save_Log(_context, "ContactUS", "Manage", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_ContactUS));
                    }
                    catch (Exception ex)
                    {
                        ViewBag.MyAlert = cls.MyAlert("false", "Error : " + ex.Message.ToString());
                    }
                }
                else
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                }
                return View(mT_ContactUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }
        // GET: MasterContactUS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_ContactUS = await _context.MT_ContactUS
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mT_ContactUS == null)
            {
                return NotFound();
            }

            return View(mT_ContactUS);
        }

        // GET: MasterContactUS/Create
        public IActionResult Create()
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (HttpContext.Session.GetString("MyAlert") != null)
                {
                    ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                    HttpContext.Session.Remove("MyAlert");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: MasterContactUS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MT_ContactUS mT_ContactUS)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (HttpContext.Session.GetString("MyAlert") != null)
                {
                    ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                    HttpContext.Session.Remove("MyAlert");
                }

                if (ModelState.IsValid)
                {
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                 
                    mT_ContactUS.Createby = list_user.User_ID;
                    mT_ContactUS.CreateDate = DateTime.Now;
                    _context.Add(mT_ContactUS);
                    await _context.SaveChangesAsync();

                    //Save Log
                    cls.Save_Log(_context, "ContactUS", "Create", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_ContactUS));
                    return RedirectToAction(nameof(Manage));
                }
                return View(mT_ContactUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: MasterContactUS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var mT_ContactUS = await _context.MT_ContactUS.FindAsync(id);
                if (mT_ContactUS == null)
                {
                    return NotFound();
                }
                return View(mT_ContactUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }

        // POST: MasterContactUS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Address,Email_1,Email_2,Email_3,Phone_Number1,Phone_Number2,Phone_Number3,Google_Map_URL,Active,Createby,CreateDate")] MT_ContactUS mT_ContactUS)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (id != mT_ContactUS.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mT_ContactUS);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MT_ContactUSExists(mT_ContactUS.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Manage));
                }
                return View(mT_ContactUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }

        // GET: MasterContactUS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var mT_ContactUS = await _context.MT_ContactUS
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (mT_ContactUS == null)
                {
                    return NotFound();
                }

                return View(mT_ContactUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: MasterContactUS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                var mT_ContactUS = await _context.MT_ContactUS.FindAsync(id);
                _context.MT_ContactUS.Remove(mT_ContactUS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        private bool MT_ContactUSExists(int id)
        {
            return _context.MT_ContactUS.Any(e => e.ID == id);
        }
    }
}
