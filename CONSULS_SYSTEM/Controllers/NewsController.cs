using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CONSULS_SYSTEM.DATA;
using CONSULS_SYSTEM.Models;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace CONSULS_SYSTEM.Controllers
{
    public class NewsController : Controller
    {
        IHostingEnvironment _hostingEnvironment;
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        public static IConfiguration Configuration { get; set; }
        string controller_menu = "News";

        public class Model
        {
            public TB_News TB_News { get; set; }
            public List<MT_Level> MT_Level { get; set; }
            public List<MT_Country> MT_Country { get; set; }
        }

        public NewsController(CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
            {
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                List<TB_News> tb_news = new List<TB_News>();
                string session_user = HttpContext.Session.GetString("session_user");
                if (session_user == null)
                {
                    return View(await _context.TB_News.Where(x => x.Active == true && x.Type == 3).OrderByDescending(x => x.CreateDate).ToListAsync());
                }
                else
                {
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    tb_news = await _context.TB_News.Where(x => x.Active == true && (x.Type == 1 || x.Type == 3 || (x.Type == 4 && x.Country_ID == list_user.Country_ID))).OrderByDescending(x => x.CreateDate).ToListAsync();
                }

                return View(tb_news);
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

                var db = await (from m in _context.TB_News
                                join l in _context.MT_Level on m.Type equals l.Level_ID
                                join c in _context.MT_Country on m.Country_ID equals c.Country_ID into lj_m
                                from x in lj_m.DefaultIfEmpty()
                                select new TB_News
                                {
                                    level = l,
                                    country = x,
                                    ID = m.ID,
                                    Active = m.Active,
                                    Createby = m.Createby,
                                    CreateDate = m.CreateDate,
                                    Country_ID = m.Country_ID,
                                    Img_Url = m.Img_Url,
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
        public async Task<IActionResult> Manage(TB_News tB_News)
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

                ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text", tB_News.Active);
                ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true).Distinct().ToList(), "Level_ID", "Name", tB_News.Type);
                ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", tB_News.Country_ID);

                var db = await (from m in _context.TB_News
                                join l in _context.MT_Level on m.Type equals l.Level_ID
                                join c in _context.MT_Country on m.Country_ID equals c.Country_ID into lj_m
                                from x in lj_m.DefaultIfEmpty()
                                where
                                       (tB_News.Country_ID != 0 ? m.Country_ID.Equals(tB_News.Country_ID) : true)
                                    && (tB_News.Type != 0 ? m.Type.Equals(tB_News.Type) : true)
                                    && (tB_News.Active != null ? m.Active.Equals(tB_News.Active) : true)
                                select new TB_News
                                {
                                    level = l,
                                    country = x,
                                    ID = m.ID,
                                    Active = m.Active,
                                    Createby = m.Createby,
                                    CreateDate = m.CreateDate,
                                    Country_ID = m.Country_ID,
                                    Img_Url = m.Img_Url,
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

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
            {
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tB_News = await _context.TB_News
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (tB_News == null)
                {
                    return NotFound();
                }

                return View(tB_News);
            }
        }

        // GET: News/Create
        public IActionResult Create()
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                Model data = new Model();
                data.MT_Level = _context.MT_Level.ToList();
                data.MT_Country = _context.MT_Country.ToList();
                if (HttpContext.Session.GetString("MyAlert") != null)
                {
                    ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                    HttpContext.Session.Remove("MyAlert");
                }
                return View(data);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TB_News tB_News, List<IFormFile> uploadfile, bool sendnotify)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                Model data = new Model();
                data.TB_News = tB_News;
                data.MT_Level = _context.MT_Level.ToList();
                data.MT_Country = _context.MT_Country.ToList();

                try
                {
                    string folder_type = "images";
                    string folder_image = "News";
                    string file_response = "", error_message = "";

                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);

                    tB_News.Createby = list_user.User_ID;
                    tB_News.CreateDate = DateTime.Now;
                    tB_News.Status = "A";

                    if (tB_News.Type == 4 && tB_News.Country_ID == 0)
                    {
                        error_message = "Please select country";
                        ModelState.AddModelError("TB_News.Country_ID", error_message);
                        ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                        goto ExitError;
                    }
                    else if (tB_News.Type == 0)
                    {
                        error_message = "Please select Type";
                        ModelState.AddModelError("TB_News.Type", error_message);
                        ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                        goto ExitError;
                    }

                    if (uploadfile.Count > 0)
                    {
                        if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            tB_News.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                        else
                        {
                            tB_News.Img_Url = "";
                            ModelState.AddModelError("Img_Url", error_message);
                            ViewBag.MyAlert = cls.MyAlert("false", error_message);
                            goto ExitError;
                        }
                    }
                    tB_News.Active = (tB_News.Active == true ? true : false);
                    tB_News.Country_ID = (tB_News.Type == 4 && (tB_News.Country_ID != null && tB_News.Country_ID != 0) ? tB_News.Country_ID : 0);
                    _context.Add(tB_News);

                    //Then noti to user
                    string Attachment = "";// Url.Content(tB_News.Attachment);
                    string file_for_line = /*folder_Attachment + "\\" +*/ folder_image + "\\" + file_response;
                    if (sendnotify)
                    {
                        LoopSend(tB_News.Type, tB_News.Country_ID, tB_News.Description
                        , (Attachment == null || Attachment.Equals("") ? "" : Attachment)
                        , (Attachment == null || Attachment.Equals("") ? "" : file_for_line));
                    }

                    await _context.SaveChangesAsync();
                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                    HttpContext.Session.SetString("MyAlert", json);

                    //Save Log
                    cls.Save_Log(_context, controller_menu, "Create", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_News));
                    return RedirectToAction(nameof(Create));
                    //return RedirectToAction(nameof(Manage));
                }

                catch (Exception ex)
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Error : " + ex.Message.ToString());
                    _context.TB_Log.Add(new TB_Log() { Message = ex.ToString(), CreateDate = DateTime.Now });
                    await _context.SaveChangesAsync();
                }
            ExitError:;

                return View(data);
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
                Attachment = _hostingEnvironment.WebRootPath + $@"\" + Attachment.Replace("/", "\\");
                file_for_line = Configuration["Config:Domain"].ToString() + "/api/SERVICES/DOWNLOAD?file=" + file_for_line;
            }
            message = "News : " + message;

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
            Task.Factory.StartNew(() => cls.SendmailMoreFile(mailTo, mailCC, ("HCAT NEWS"), message, fileAttach));
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

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                string session_user = HttpContext.Session.GetString("session_user");
                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);

                if (id == null)
                {
                    return NotFound();
                }
                Class.Class_Resource cls = new Class.Class_Resource();
                int key_id = Convert.ToInt32(cls.Base64Decode(id.ToString()));
                var tB_News = await _context.TB_News.FindAsync(key_id);
                if (tB_News == null)
                {
                    return NotFound();
                }

                Model data = new Model();
                data.MT_Level = _context.MT_Level.ToList();
                data.MT_Country = _context.MT_Country.ToList();
                data.TB_News = tB_News;
                return View(data);
            }
            catch (Exception ex)
            {
                _context.TB_Log.Add(new TB_Log() { Message = ex.ToString(), CreateDate = DateTime.Now });
                await _context.SaveChangesAsync();
            }
            Model data2 = new Model();
            data2.MT_Level = _context.MT_Level.ToList();
            data2.MT_Country = _context.MT_Country.ToList();
            return View(data2);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TB_News tB_News, List<IFormFile> uploadfileEdit, bool sendnotify)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                try
                {
                    string folder_type = "images";
                    string folder_image = "News";
                    string file_response = "", error_message = "";

                    if (uploadfileEdit.Count > 0)
                    {
                        if (cls.MoveFile(uploadfileEdit, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            tB_News.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                    }
                    if (tB_News.Type == 4 && tB_News.Country_ID == 0)
                    {
                        error_message = "Please select country";
                        ModelState.AddModelError("TB_News.Country_ID", error_message);
                        ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                        goto ExitError;
                    }
                    else if (tB_News.Type == 0)
                    {
                        error_message = "Please select Type";
                        ModelState.AddModelError("TB_News.Type", error_message);
                        ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                        goto ExitError;
                    }
                    tB_News.Active = (tB_News.Active == true ? true : false);
                    tB_News.Country_ID = (tB_News.Type == 4 && (tB_News.Country_ID != null && tB_News.Country_ID != 0) ? tB_News.Country_ID : 0);
                    _context.Update(tB_News);

                    //Then noti to user
                    string Attachment = "";// Url.Content(tB_News.Attachment);
                    string file_for_line = /*folder_Attachment + "\\" +*/ folder_image + "\\" + file_response;
                    if (sendnotify)
                    {
                        LoopSend(tB_News.Type, tB_News.Country_ID, tB_News.Description
                        , (Attachment == null || Attachment.Equals("") ? "" : Attachment)
                        , (Attachment == null || Attachment.Equals("") ? "" : file_for_line));
                    }
                    await _context.SaveChangesAsync();
                    ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");

                    //Save Log
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    cls.Save_Log(_context, controller_menu, "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_News));
                }
                catch (Exception ex)
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Error : " + ex.Message.ToString());
                }
            //return RedirectToAction(nameof(Manage));

            ExitError:;
                Model data = new Model();
                data.TB_News = tB_News;
                data.MT_Level = _context.MT_Level.ToList();
                data.MT_Country = _context.MT_Country.ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }

        // GET: News/Delete/5
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
                    var tB_News = await _context.TB_News
                        .FirstOrDefaultAsync(m => m.ID == key_id);
                    if (tB_News == null)
                    {
                        return NotFound();
                    }

                    _context.TB_News.Remove(tB_News);
                    await _context.SaveChangesAsync();

                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                    HttpContext.Session.SetString("MyAlert", json);

                    //Save Log
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    cls.Save_Log(_context, controller_menu, "Delete", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_News));
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

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string menu_allow = "";
            bool allow = false;
            allow = cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context);
            if (allow == true && menu_allow == "W")
            {
                var tB_News = await _context.TB_News.FindAsync(id);
                _context.TB_News.Remove(tB_News);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        private bool TB_NewsExists(int id)
        {
            return _context.TB_News.Any(e => e.ID == id);
        }

        public string MoveFile(List<IFormFile> uploadfile, string folder_name)
        {
            string fileName = string.Empty;
            if (uploadfile.Count > 0)
            {
                try
                {

                    foreach (var file in uploadfile)
                    {
                        //ชื่อไฟล์ที่ Upload เข้ามา
                        fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        String TypeFile = System.IO.Path.GetExtension(file.FileName);
                        var Size = file.Length;
                        string fileName_save = _hostingEnvironment.WebRootPath + $@"\images\{folder_name}\{ fileName}";
                        using (FileStream fs = System.IO.File.Create(fileName_save))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }

                }
                catch (Exception ex)
                {
                    _context.TB_Log.Add(new TB_Log() { Message = ex.ToString(), CreateDate = DateTime.Now });
                    _context.SaveChangesAsync();
                }
            }
            return fileName;
        }
    }
}
