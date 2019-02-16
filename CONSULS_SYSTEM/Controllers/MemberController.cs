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
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CONSULS_SYSTEM.Controllers
{
    public class MemberController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        public static IConfiguration Configuration { get; set; }
        string controller_menu = "Member";

        public MemberController(CONSULS_Context context)
        {
            _context = context;
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        [HttpGet]
        public IActionResult Index()
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
                {
                    return RedirectToAction(nameof(Manage));
                }
                else
                {
                    ViewData["menu_allow"] = menu_allow;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }
        // GET: TB_Member
        public async Task<IActionResult> Manage()
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                try
                {
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
                    ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 2)).Distinct().ToList(), "Level_ID", "Name");
                    ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name");

                    var db = await (from m in _context.TB_Member
                                    join l in _context.MT_Level on m.Level_ID equals l.Level_ID
                                    join c in _context.MT_Country on m.Country_ID equals c.Country_ID into lj_m
                                    from x in lj_m.DefaultIfEmpty()
                                    select new TB_Member
                                    {
                                        level = l,
                                        country = x,
                                        ID = m.ID,
                                        User_ID = m.User_ID,
                                        Name = m.Name,
                                        Sub_Name = m.Sub_Name,
                                        Level_ID = m.Level_ID,
                                        Active = m.Active,
                                        Address = m.Address,
                                        Createby = m.Createby,
                                        CreateDate = m.CreateDate,
                                        Email = m.Email,
                                        Phone_Number = m.Phone_Number,
                                        Position = m.Position,
                                        Education = m.Education,
                                        Country_ID = m.Country_ID,
                                        Img_Url = m.Img_Url,
                                        Line_Token = m.Line_Token,
                                        Password = m.Password
                                    }).ToListAsync();
                    return View(db);//await _context.TB_Member.Include(l => l.level).Include(c => c.country).ToListAsync()
                }
                catch (Exception ex)
                {
                    _context.TB_Log.Add(new TB_Log() { Message = ex.ToString(), CreateDate = DateTime.Now });
                    await _context.SaveChangesAsync();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Manage(TB_Member tB_Member)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                try
                {
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

                    ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text", tB_Member.Active);
                    ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 2)).Distinct().ToList(), "Level_ID", "Name", tB_Member.Level_ID);
                    ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", tB_Member.Country_ID);

                    var db = await (from m in _context.TB_Member
                                    join l in _context.MT_Level on m.Level_ID equals l.Level_ID
                                    join c in _context.MT_Country on m.Country_ID equals c.Country_ID into lj_m
                                    from x in lj_m.DefaultIfEmpty()
                                    where
                                    (tB_Member.Country_ID != 0 ? m.Country_ID.Equals(tB_Member.Country_ID) : true)
                                    && (tB_Member.Level_ID != 0 ? m.Level_ID.Equals(tB_Member.Level_ID) : true)
                                    && (tB_Member.Active != null ? m.Active.Equals(tB_Member.Active) : true)
                                    select new TB_Member
                                    {
                                        level = l,
                                        country = x,
                                        ID = m.ID,
                                        User_ID = m.User_ID,
                                        Name = m.Name,
                                        Sub_Name = m.Sub_Name,
                                        Level_ID = m.Level_ID,
                                        Active = m.Active,
                                        Address = m.Address,
                                        Createby = m.Createby,
                                        CreateDate = m.CreateDate,
                                        Email = m.Email,
                                        Phone_Number = m.Phone_Number,
                                        Position = m.Position,
                                        Education = m.Education,
                                        Country_ID = m.Country_ID,
                                        Img_Url = m.Img_Url,
                                        Line_Token = m.Line_Token,
                                        Password = m.Password
                                    }).ToListAsync();
                    return View(db);//await _context.TB_Member.Include(l => l.level).Include(c => c.country).ToListAsync()
                }
                catch (Exception ex)
                {
                    _context.TB_Log.Add(new TB_Log() { Message = ex.ToString(), CreateDate = DateTime.Now });
                    await _context.SaveChangesAsync();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: TB_Member/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_Member = await _context.TB_Member
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tB_Member == null)
            {
                return NotFound();
            }

            return View(tB_Member);
        }

        // GET: TB_Member/Create
        public IActionResult Create()
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 2)).Distinct().ToList(), "Level_ID", "Name");
                ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name");
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

        // POST: TB_Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TB_Member tB_Member)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 2)).Distinct().ToList(), "Level_ID", "Name", tB_Member.Level_ID);
                ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", tB_Member.Country_ID);

                if (ModelState.IsValid)
                {
                    try
                    {
                        if (TB_MemberExists(tB_Member.User_ID))//True = มีซ้ำอยู่ในระบบ
                        {
                            ModelState.AddModelError("User_ID", "User ID already exists.");
                            ViewBag.MyAlert = cls.MyAlert("false", "User ID already exists.");
                        }
                        if (TB_MemberExists_Email(tB_Member.Email))//True = มีซ้ำอยู่ในระบบ
                        {
                            ModelState.AddModelError("Email", "Email already exists.");
                            ViewBag.MyAlert = cls.MyAlert("false", "Email already exists.");
                        }
                        else if (tB_Member.Level_ID == 1 && tB_Member.Country_ID == 0)
                        {
                            ModelState.AddModelError("Country_ID", "Please select country");
                            ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                        }
                        else
                        {
                            string session_user = HttpContext.Session.GetString("session_user");
                            var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                            tB_Member.Createby = list_user.User_ID;//"aof";//User Session
                            tB_Member.CreateDate = System.DateTime.Now;
                            tB_Member.Password = cls.EncodeHMAC_SHA512("000000");///tB_Member.Password
                            tB_Member.Line_Token = "";
                            tB_Member.Active = (tB_Member.Active == true ? true : false);
                            tB_Member.Country_ID = (tB_Member.Level_ID == 4 && (tB_Member.Country_ID != null && tB_Member.Country_ID != 0) ? tB_Member.Country_ID : 0);
                            _context.Add(tB_Member);
                            await _context.SaveChangesAsync();
                            string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                            HttpContext.Session.SetString("MyAlert", json);

                            //Send mail
                            string user_encode = cls.Base64Encode(tB_Member.User_ID);
                            string message = "Your account : " + tB_Member.User_ID + "<br />";
                            message += "Please continue to change password <a href='" + Configuration["Config:Domain"].ToString() + "/Login/change_password?u=" + user_encode + "'> Click </a>";
                            SendMail(message, tB_Member.Email, "", "");

                            //Save Log
                            cls.Save_Log(_context, "Member", "Create", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Member));
                            return RedirectToAction(nameof(Create));
                            //return RedirectToAction(nameof(Manage));
                        }
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

                return View(tB_Member);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }
        /// <summary>
        /// Async => Send mail
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mailTo"></param>
        /// <param name="mailCC"></param>
        /// <param name="fileAttach"></param>
        private void SendMail(string message, string mailTo, string mailCC, string fileAttach)
        {
            Task.Factory.StartNew(() => cls.SendmailMoreFile(mailTo, mailCC, ("HCAT NEW ACCOUNT"), message, fileAttach));
        }
        // GET: TB_Member/Edit/5
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

                var tB_Member = await _context.TB_Member.FindAsync(key_id);
                if (tB_Member == null)
                {
                    return NotFound();
                }

                ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 2)).Distinct().ToList(), "Level_ID", "Name", tB_Member.Level_ID);
                ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", tB_Member.Country_ID);

                return View(tB_Member);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: TB_Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TB_Member tB_Member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Class.Class_Resource cls = new Class.Class_Resource();

                    if (tB_Member.Level_ID == 1 && tB_Member.Country_ID == 0)
                    {
                        ModelState.AddModelError("Country_ID", "Please select country");
                        ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                    }
                    //if (TB_MemberExists_Email(tB_Member.Email))//True = มีซ้ำอยู่ในระบบ
                    //{
                    //    ModelState.AddModelError("Email", "Email already exists.");
                    //    ViewBag.MyAlert = cls.MyAlert("false", "Email already exists.");
                    //}
                    else
                    {
                        if (tB_Member.Line_Token == null)
                        {
                            tB_Member.Line_Token = "";
                        }
                        tB_Member.Active = (tB_Member.Active == true ? true : false);
                        tB_Member.Country_ID = (tB_Member.Level_ID == 1 && (tB_Member.Country_ID != null && tB_Member.Country_ID != 0) ? tB_Member.Country_ID : 0);
                        _context.Update(tB_Member);
                        await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Manage));
                        ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");

                        //Save Log
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        cls.Save_Log(_context, "Member", "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Member));
                    }

                    await _context.SaveChangesAsync();
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
            ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 2)).Distinct().ToList(), "Level_ID", "Name", tB_Member.Level_ID);
            ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", tB_Member.Country_ID);
            return View(tB_Member);
        }

        // GET: TB_Member/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                try
                {
                    int key_id = Convert.ToInt32(cls.Base64Decode(id.ToString()));
                    var tB_Member = await _context.TB_Member.FindAsync(key_id);
                    if (tB_Member == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.TB_Member.Remove(tB_Member);
                        await _context.SaveChangesAsync();
                        string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                        HttpContext.Session.SetString("MyAlert", json);

                        //Save Log
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        cls.Save_Log(_context, "Member", "Delete", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Member));
                    }
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

        //// POST: TB_Member/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tB_Member = await _context.TB_Member.FindAsync(id);
        //    _context.TB_Member.Remove(tB_Member);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Manage));
        //}

        private bool TB_MemberExists(string User_ID)
        {
            return _context.TB_Member.Any(e => e.User_ID == User_ID);
        }
        private bool TB_MemberExists_Email(string Email)
        {
            return _context.TB_Member.Any(e => e.Email == Email);
        }
    }
}
