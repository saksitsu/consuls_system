using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CONSULS_SYSTEM.DATA;
using CONSULS_SYSTEM.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CONSULS_SYSTEM.Controllers
{
    public class AboutUSController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        string controller_menu = "AboutUS";

        public AboutUSController(CONSULS_Context context)
        {
            _context = context;
        }

        // GET: AboutUS
        public async Task<IActionResult> Index()
        {
            if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
            {
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                return View(await _context.MT_AboutUS.Where(x => x.Active == true).ToListAsync());
            }
            
        }

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
                List<dynamic> list = new List<dynamic>();
                list.Add(new
                {
                    Text = "Active",
                    Value = true
                });
                list.Add(new
                {
                    Text = "Inactive",
                    Value = false
                });

                ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text");

                return View(await _context.MT_AboutUS.ToListAsync());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Manage(MT_AboutUS mT_AboutUS)
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
                List<dynamic> list = new List<dynamic>();
                list.Add(new
                {
                    Text = "Active",
                    Value = true
                });
                list.Add(new
                {
                    Text = "Inactive",
                    Value = false
                });
                ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text", mT_AboutUS.Active);
                var db = await _context.MT_AboutUS.Where(x => (mT_AboutUS.Active != null ? x.Active.Equals(mT_AboutUS.Active) : true)).ToListAsync();

                return View(db);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: AboutUS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_AboutUS = await _context.MT_AboutUS
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mT_AboutUS == null)
            {
                return NotFound();
            }

            return View(mT_AboutUS);
        }

        // GET: AboutUS/Create
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

        // POST: AboutUS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MT_AboutUS mT_AboutUS)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);

                        mT_AboutUS.Createby = list_user.User_ID;
                        mT_AboutUS.CreateDate = DateTime.Now;
                        mT_AboutUS.Active = (mT_AboutUS.Active == true ? true : false);
                        _context.Add(mT_AboutUS);
                        await _context.SaveChangesAsync();
                        //string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                        //HttpContext.Session.SetString("MyAlert", json);
                        //return RedirectToAction(nameof(Manage));
                        string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                        HttpContext.Session.SetString("MyAlert", json);

                        //Save Log
                        cls.Save_Log(_context, "AboutUS", "Create", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_AboutUS));

                        return RedirectToAction(nameof(Create));
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
                return View(mT_AboutUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: AboutUS/Edit/5
        public async Task<IActionResult> Edit(string id)
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

                Class.Class_Resource cls = new Class.Class_Resource();
                int key_id = Convert.ToInt32(cls.Base64Decode(id.ToString()));
                var mT_AboutUS = await _context.MT_AboutUS.FindAsync(key_id);
                if (mT_AboutUS == null)
                {
                    return NotFound();
                }
                return View(mT_AboutUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: AboutUS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,MT_AboutUS mT_AboutUS)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (id != mT_AboutUS.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var entityProperties = mT_AboutUS.GetType().GetProperties();
                        mT_AboutUS.Active = (mT_AboutUS.Active == true ? true : false);
                        foreach (var ep in entityProperties)
                        {
                            //Field ที่ไม่ต้องการให้ Update
                            if (ep.Name == "ID" || ep.Name == "Createby" || ep.Name == "CreateDate")
                            {
                                _context.Entry(mT_AboutUS).Property(ep.Name).IsModified = false;
                            }
                            else
                            {
                                _context.Entry(mT_AboutUS).Property(ep.Name).IsModified = true;
                            }
                        }
                        //_context.Update(mT_AboutUS);
                        await _context.SaveChangesAsync();
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                        //HttpContext.Session.SetString("MyAlert", json);
                        //return RedirectToAction(nameof(Manage));
                        ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");

                        //Save Log
                        cls.Save_Log(_context, "AboutUS", "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_AboutUS));

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
                return View(mT_AboutUS);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: AboutUS/Delete/5
        public async Task<IActionResult> Delete(string id)
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

                try
                {
                    Class.Class_Resource cls = new Class.Class_Resource();
                    int key_id = Convert.ToInt32(cls.Base64Decode(id.ToString()));
                    var mT_AboutUS = await _context.MT_AboutUS
                        .FirstOrDefaultAsync(m => m.ID == key_id);
                    if (mT_AboutUS == null)
                    {
                        return NotFound();
                    }
                    _context.MT_AboutUS.Remove(mT_AboutUS);
                    await _context.SaveChangesAsync();

                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                    HttpContext.Session.SetString("MyAlert", json);
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    //Save Log
                    cls.Save_Log(_context, "AboutUS", "Delete", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_AboutUS));
                }
                catch (Exception ex)
                {
                    string json = JsonConvert.SerializeObject(cls.MyAlert("false", "Error : " + ex.Message.ToString()));
                    HttpContext.Session.SetString("MyAlert", json);
                }

                return RedirectToAction(nameof(Manage));
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: AboutUS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                var mT_AboutUS = await _context.MT_AboutUS.FindAsync(id);
                _context.MT_AboutUS.Remove(mT_AboutUS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        private bool MT_AboutUSExists(int id)
        {
            return _context.MT_AboutUS.Any(e => e.ID == id);
        }
    }
}
