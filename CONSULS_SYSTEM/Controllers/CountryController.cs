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
    public class CountryController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        IHostingEnvironment _hostingEnvironment;
        string controller_menu = "Country";

        public CountryController(CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Country
        public async Task<IActionResult> Index()
        {
            if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
            {
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                return View(await _context.MT_Country.Where(x => x.Active == true).ToListAsync());
            }
        }

        // GET: Country
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
                return View(await _context.MT_Country.ToListAsync());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Manage(MT_Country mT_Country)
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
                ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text", mT_Country.Active);
                var db = await _context.MT_Country.Where(x => (mT_Country.Active != null ? x.Active.Equals(mT_Country.Active) : true)).ToListAsync();

                return View(db);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }
        // GET: Country/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mT_Country = await _context.MT_Country
                .FirstOrDefaultAsync(m => m.Country_ID == id);
            if (mT_Country == null)
            {
                return NotFound();
            }

            return View(mT_Country);
        }

        // GET: Country/Create
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

        // POST: Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MT_Country mT_Country, List<IFormFile> uploadfile)
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
                        string folder_type = "images";
                        string folder_image = "Country";
                        string file_response = "", error_message = "";
                        //ข้อมูล User
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        mT_Country.Createby = list_user.User_ID;//"aof";//User Session
                        mT_Country.CreateDate = DateTime.Now;
                        mT_Country.Active = (mT_Country.Active == true ? true : false);
                        //Save file รูป
                        if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            mT_Country.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                        else
                        {
                            mT_Country.Img_Url = "";
                            ModelState.AddModelError("Img_Url", error_message);
                            ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                            goto ExitError;
                        }
                        _context.Add(mT_Country);
                        await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Manage));
                        string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                        HttpContext.Session.SetString("MyAlert", json);

                        //Save Log
                        cls.Save_Log(_context, "Country", "Create", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_Country));

                        return RedirectToAction(nameof(Create));
                    }
                    catch (Exception ex)
                    {
                        ViewBag.MyAlert = cls.MyAlert("false", "Error : " + ex.Message.ToString());
                    }
                }

            ExitError:;
                return View(mT_Country);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: Country/Edit/5
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
                var mT_Country = await _context.MT_Country.FindAsync(key_id);
                if (mT_Country == null)
                {
                    return NotFound();
                }
                return View(mT_Country);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Country_ID, MT_Country mT_Country, List<IFormFile> uploadfile)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                if (Country_ID != mT_Country.Country_ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        string folder_type = "images";
                        string folder_image = "Country";
                        string file_response = "", error_message = "";

                        //Save file รูป
                        if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            mT_Country.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                        mT_Country.Active = (mT_Country.Active == true ? true : false);
                        var entityProperties = mT_Country.GetType().GetProperties();
                        foreach (var ep in entityProperties)
                        {
                            //Field ที่ไม่ต้องการให้ Update
                            if (ep.Name == "Country_ID" || ep.Name == "Createby" || ep.Name == "CreateDate")
                            {
                                _context.Entry(mT_Country).Property(ep.Name).IsModified = false;
                            }
                            else
                            {
                                _context.Entry(mT_Country).Property(ep.Name).IsModified = true;
                            }
                        }

                        //_context.Update(mT_Country);
                        await _context.SaveChangesAsync();
                        ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");

                        //Save Log
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        cls.Save_Log(_context, "Country", "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_Country));

                    }
                    catch (Exception ex)
                    {
                        ViewBag.MyAlert = cls.MyAlert("false", "Error : " + ex.Message.ToString());
                    }
                    //return RedirectToAction(nameof(Manage));
                }
                else
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                }

                return View(mT_Country);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: Country/Delete/5
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
                    var mT_Country = await _context.MT_Country
                        .FirstOrDefaultAsync(m => m.Country_ID == key_id);
                    if (mT_Country == null)
                    {
                        return NotFound();
                    }
                    _context.MT_Country.Remove(mT_Country);
                    await _context.SaveChangesAsync();

                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                    HttpContext.Session.SetString("MyAlert", json);

                    //Save Log
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    cls.Save_Log(_context, "Country", "Delete", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(mT_Country));
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

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                var mT_Country = await _context.MT_Country.FindAsync(id);
                _context.MT_Country.Remove(mT_Country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        private bool MT_CountryExists(int id)
        {
            return _context.MT_Country.Any(e => e.Country_ID == id);
        }
    }
}
