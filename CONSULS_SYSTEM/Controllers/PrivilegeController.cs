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
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CONSULS_SYSTEM.Controllers
{
    public class PrivilegeController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        IHostingEnvironment _hostingEnvironment;
        public static IConfiguration Configuration { get; set; }
        string controller_menu = "Privilege";

        public PrivilegeController(CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // GET: Privilege
        public async Task<IActionResult> Index()
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

                    string session_user = HttpContext.Session.GetString("session_user");
                    if (session_user == null)
                    {
                        var cONSULS_Context = await _context.TB_Privilege
                       //.Include(t => t.country)
                       .Include(t => t.level)
                       .Where(x => x.Active == true && x.Type == 3)
                       .OrderByDescending(o => o.CreateDate)
                       .ToListAsync();
                        return View(cONSULS_Context);
                    }
                    else
                    {
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        var cONSULS_Context = await _context.TB_Privilege
                       //.Include(t => t.country)
                       .Include(t => t.level)
                       .Where(x => x.Active == true && (x.Type == 1 || x.Type == 3 || (x.Type == 4 && x.Country_ID == list_user.Country_ID)))
                       .OrderByDescending(o => o.CreateDate)
                       .ToListAsync();
                        return View(cONSULS_Context);
                    }
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
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
                ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true).Distinct().ToList(), "Level_ID", "Name");
                ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name");

                var db = await (from m in _context.TB_Privilege
                                join l in _context.MT_Level on m.Type equals l.Level_ID
                                join c in _context.MT_Country on m.Country_ID equals c.Country_ID into lj_m
                                from x in lj_m.DefaultIfEmpty()
                                select new TB_Privilege
                                {
                                    level = l,
                                    country = x,
                                    ID = m.ID,
                                    Active = m.Active,
                                    Createby = m.Createby,
                                    CreateDate = m.CreateDate,
                                    Country_ID = m.Country_ID,
                                    Img_Url = m.Img_Url,
                                    Attachment = m.Attachment,
                                    Description = m.Description,
                                    Status = m.Status,
                                    Title = m.Title,
                                    Type = m.Type
                                }).ToListAsync();
                return View(db);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Manage(TB_Privilege tB_Privilege)
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

                ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text", tB_Privilege.Active);
                ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true).Distinct().ToList(), "Level_ID", "Name", tB_Privilege.Type);
                ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", tB_Privilege.Country_ID);

                var db = await (from m in _context.TB_Privilege
                                join l in _context.MT_Level on m.Type equals l.Level_ID
                                join c in _context.MT_Country on m.Country_ID equals c.Country_ID into lj_m
                                from x in lj_m.DefaultIfEmpty()
                                where
                                       (tB_Privilege.Country_ID != 0 ? m.Country_ID.Equals(tB_Privilege.Country_ID) : true)
                                    && (tB_Privilege.Type != 0 ? m.Type.Equals(tB_Privilege.Type) : true)
                                    && (tB_Privilege.Active != null ? m.Active.Equals(tB_Privilege.Active) : true)
                                select new TB_Privilege
                                {
                                    level = l,
                                    country = x,
                                    ID = m.ID,
                                    Active = m.Active,
                                    Createby = m.Createby,
                                    CreateDate = m.CreateDate,
                                    Country_ID = m.Country_ID,
                                    Img_Url = m.Img_Url,
                                    Attachment = m.Attachment,
                                    Description = m.Description,
                                    Status = m.Status,
                                    Title = m.Title,
                                    Type = m.Type
                                }).ToListAsync();
                return View(db);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: Privilege/Details/5
        public async Task<IActionResult> Details(int? id)
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

                    if (id == null)
                    {
                        return NotFound();
                    }

                    var tB_Privilege = await _context.TB_Privilege
                        //.Include(t => t.country)
                        .Include(t => t.level)
                        .FirstOrDefaultAsync(m => m.ID == id);
                    if (tB_Privilege == null)
                    {
                        return NotFound();
                    }

                    return View(tB_Privilege);
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: Privilege/Create
        public IActionResult Create()
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                ViewData["Country_ID"] = new SelectList(_context.MT_Country, "Country_ID", "Name");
                ViewData["Type"] = new SelectList(_context.MT_Level, "Level_ID", "Name");
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

        // POST: Privilege/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TB_Privilege tB_Privilege, List<IFormFile> uploadfile, List<IFormFile> uploadfile_Attachment, bool sendnotify)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                ViewData["Country_ID"] = new SelectList(_context.MT_Country, "Country_ID", "Name", tB_Privilege.Country_ID);
                ViewData["Type"] = new SelectList(_context.MT_Level, "Level_ID", "Name", tB_Privilege.Type);

                if (ModelState.IsValid)
                {
                    try
                    {
                        string folder_type = "images";
                        string folder_Attachment = "Attachment";
                        string folder_image = "Privilege";
                        string file_response = "", error_message = "";
                        //ข้อมูล User
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        tB_Privilege.Createby = list_user.User_ID;//"aof";//User Session
                        tB_Privilege.CreateDate = DateTime.Now;
                        tB_Privilege.Status = "A";
                        tB_Privilege.Active = (tB_Privilege.Active == true ? true : false);
                        tB_Privilege.Country_ID = (tB_Privilege.Type == 4 && (tB_Privilege.Country_ID != null && tB_Privilege.Country_ID != 0) ? tB_Privilege.Country_ID : 0);

                        if (tB_Privilege.Type == 4 && tB_Privilege.Country_ID == 0)
                        {
                            error_message = "Please select country";
                            ModelState.AddModelError("Country_ID", error_message);
                            ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                            goto ExitError;
                        }
                        else if (tB_Privilege.Type == 0)
                        {
                            error_message = "Please select Type";
                            ModelState.AddModelError("Type", error_message);
                            ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                            goto ExitError;
                        }
                        //Save file รูป
                        if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            tB_Privilege.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                        else
                        {
                            tB_Privilege.Img_Url = "";
                            //ModelState.AddModelError("Img_Url", error_message);
                            //goto ExitError;
                        }
                        //Save file attachment
                        if (cls.MoveFile(uploadfile_Attachment, _hostingEnvironment, folder_Attachment, folder_image, ref file_response, ref error_message))
                        {
                            tB_Privilege.Attachment = string.Format("~/" + folder_Attachment + "/" + folder_image + "/{0}", file_response);
                        }
                        else
                        {
                            tB_Privilege.Attachment = "";
                            //ModelState.AddModelError("Attachment", error_message);
                            //goto ExitError;
                        }

                        _context.Add(tB_Privilege);

                        //Then noti to user
                        string Attachment = tB_Privilege.Attachment; //Url.Content(tB_Privilege.Attachment);
                        string file_for_line = folder_Attachment + "\\" + folder_image + "\\" + file_response;
                        if (sendnotify)
                        {
                            LoopSend(tB_Privilege.Type, tB_Privilege.Country_ID, tB_Privilege.Description
                            , (Attachment == null || Attachment.Equals("") ? "" : Attachment)
                            , (Attachment == null || Attachment.Equals("") ? "" : file_for_line));
                        }

                        await _context.SaveChangesAsync();
                        string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                        HttpContext.Session.SetString("MyAlert", json);

                        //Save Log
                        cls.Save_Log(_context, controller_menu, "Create", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Privilege));
                        return RedirectToAction(nameof(Create));
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
            ExitError:;

                return View(tB_Privilege);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }
        private void LoopSend(int Type, int Country_ID, string message, string Attachment, string file_for_line)
        {
            //ที่ทำแยกไว้แบบนี้เพราะเผื่ออนาคตจะมีให้ทำโน้นนี่นั่นในแต่ละประเภท
            if (Attachment != "")
            {
                Attachment = _hostingEnvironment.WebRootPath + $@"\" + Attachment.Replace("/", "\\").Replace("~\\", "");
                file_for_line = Configuration["Config:Domain"].ToString() + "/api/SERVICES/DOWNLOAD?file=" + cls.Base64Encode(file_for_line);
            }
            message = "Privilege : " + message;

            var all_member = _context.TB_Member.Where(x => x.Active == true).ToList();
            var all_Country_ID = all_member.Where(y => y.Country_ID == Country_ID).ToList();

            if (Type == 1)//1=Member => เห็นทุกคนที่เป็นสมาชิก
            {
                string email = "";
                if (all_member.Count > 0)
                {
                    foreach (var item in all_member)
                    {
                        if (email == "")
                        {
                            email = item.Email;
                        }
                        else
                        {
                            email += "," + item.Email;
                        }
                        //1.Line ==> อันนี้ต้อง Loop
                        if (Configuration["Config:EnableLine"].ToString().Equals("true"))
                        {
                            SendLine(message, item.Line_Token, file_for_line);
                        }

                    }
                    //2.Mail ==> อันนี้ส่งทีเดียว
                    if (Configuration["Config:EnableEmail"].ToString().Equals("true"))
                    {
                        SendMail(message, email, "", Attachment);
                    }
                }
            }
            else if (Type == 2)
            {
                //
            } //2=Admin => ตอนนี้ยังไม่ได้ทำอะไร
            else if (Type == 3) //3=Public => เป็นสาธารณะ ส่งหาสมาชิกทุกคน
            {
                string email = "";
                if (all_member.Count > 0)
                {
                    foreach (var item in all_member)
                    {
                        if (email == "")
                        {
                            email = item.Email;
                        }
                        else
                        {
                            email += "," + item.Email;
                        }
                        //1.Line ==> อันนี้ต้อง Loop
                        if (Configuration["Config:EnableLine"].ToString().Equals("true"))
                        {
                            SendLine(message, item.Line_Token, file_for_line);
                        }
                    }
                    //2.Mail ==> อันนี้ส่งทีเดียว
                    if (Configuration["Config:EnableEmail"].ToString().Equals("true"))
                    {
                        SendMail(message, email, "", Attachment);
                    }
                }
            }
            else if (Type == 4)//4.Country ==> เฉพาะสมาชิกที่อยู่ในประเทศนั้น ๆ
            {
                string email = "";
                if (all_Country_ID.Count > 0)
                {
                    foreach (var item in all_Country_ID)
                    {
                        if (email == "")
                        {
                            email = item.Email;
                        }
                        else
                        {
                            email += "," + item.Email;
                        }
                        //1.Line ==> อันนี้ต้อง Loop
                        if (Configuration["Config:EnableLine"].ToString().Equals("true"))
                        {
                            SendLine(message, item.Line_Token, file_for_line);
                        }
                    }
                    //2.Mail ==> อันนี้ส่งทีเดียว
                    if (Configuration["Config:EnableEmail"].ToString().Equals("true"))
                    {
                        SendMail(message, email, "", Attachment);
                    }
                }
            }
            else
            {
                // Error
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
            Task.Factory.StartNew(() => cls.SendmailMoreFile(mailTo, mailCC, ("HCAT PRIVILEGE"), message, fileAttach));
        }
        /// <summary>
        /// Async
        /// </summary>
        /// <param name="message"></param>
        /// <param name="Token"></param>
        private void SendLine(string message, string Token, string fileAttach)
        {
            Task.Factory.StartNew(() => cls.SendLineNotify(Token, (message + (fileAttach != "" ? " download on url > " + fileAttach : ""))));
        }

        // GET: Privilege/Edit/5
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
                var tB_Privilege = await _context.TB_Privilege.FindAsync(key_id);
                if (tB_Privilege == null)
                {
                    return NotFound();
                }
                ViewData["Country_ID"] = new SelectList(_context.MT_Country, "Country_ID", "Name", tB_Privilege.Country_ID);
                ViewData["Type"] = new SelectList(_context.MT_Level, "Level_ID", "Name", tB_Privilege.Type);
                return View(tB_Privilege);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: Privilege/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TB_Privilege tB_Privilege, List<IFormFile> uploadfile, List<IFormFile> uploadfile_Attachment, bool sendnotify)
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
                        string folder_Attachment = "Attachment";
                        string folder_image = "Privilege";
                        string file_response = "", error_message = "";
                        //ข้อมูล User
                        //string session_user = HttpContext.Session.GetString("session_user");
                        //var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        //tB_Privilege.Createby = list_user.User_ID;//"aof";//User Session
                        //tB_Privilege.CreateDate = DateTime.Now;

                        if (tB_Privilege.Type == 4 && tB_Privilege.Country_ID == 0)
                        {
                            error_message = "Please select country";
                            ModelState.AddModelError("Country_ID", error_message);
                            ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                            goto ExitError;
                        }
                        else if (tB_Privilege.Type == 0)
                        {
                            error_message = "Please select Type";
                            ModelState.AddModelError("Type", error_message);
                            ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                            goto ExitError;
                        }

                        //Save file รูป
                        if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            tB_Privilege.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                        //Save file attachment
                        if (cls.MoveFile(uploadfile_Attachment, _hostingEnvironment, folder_Attachment, folder_image, ref file_response, ref error_message))
                        {
                            tB_Privilege.Attachment = string.Format("~/" + folder_Attachment + "/" + folder_image + "/{0}", file_response);
                        }
                        tB_Privilege.Status = "A";
                        tB_Privilege.Active = (tB_Privilege.Active == true ? true : false);
                        tB_Privilege.Country_ID = (tB_Privilege.Type == 4 && (tB_Privilege.Country_ID != null && tB_Privilege.Country_ID != 0) ? tB_Privilege.Country_ID : 0);
                        _context.Update(tB_Privilege);

                        //Then noti to user
                        string Attachment = tB_Privilege.Attachment; //Url.Content(tB_Privilege.Attachment);
                        string file_for_line = folder_Attachment + "\\" + folder_image + "\\" + file_response;
                        if (sendnotify)
                        {
                            LoopSend(tB_Privilege.Type, tB_Privilege.Country_ID, tB_Privilege.Description
                            , (Attachment == null || Attachment.Equals("") ? "" : Attachment)
                            , (Attachment == null || Attachment.Equals("") ? "" : file_for_line));
                        }

                        await _context.SaveChangesAsync();
                        ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");

                        //Save Log
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        cls.Save_Log(_context, controller_menu, "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Privilege));
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

            ExitError:;
                ViewData["Country_ID"] = new SelectList(_context.MT_Country, "Country_ID", "Name", tB_Privilege.Country_ID);
                ViewData["Type"] = new SelectList(_context.MT_Level, "Level_ID", "Name", tB_Privilege.Type);
                return View(tB_Privilege);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: Privilege/Delete/5
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
                    var tB_Privilege = await _context.TB_Privilege
                        //.Include(t => t.country)
                        //.Include(t => t.level)
                        .FirstOrDefaultAsync(m => m.ID == key_id);
                    if (tB_Privilege == null)
                    {
                        return NotFound();
                    }

                    _context.TB_Privilege.Remove(tB_Privilege);
                    await _context.SaveChangesAsync();

                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                    HttpContext.Session.SetString("MyAlert", json);

                    //Save Log
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    cls.Save_Log(_context, controller_menu, "Delete", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Privilege));
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

        // POST: Privilege/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                var tB_Privilege = await _context.TB_Privilege.FindAsync(id);
                _context.TB_Privilege.Remove(tB_Privilege);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        private bool TB_PrivilegeExists(int id)
        {
            return _context.TB_Privilege.Any(e => e.ID == id);
        }
    }
}
