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

namespace CONSULS_SYSTEM.Controllers
{
    public class WebboardController : Controller
    {
        private readonly CONSULS_Context _context;
        IHostingEnvironment _hostingEnvironment;
        Class.Class_Resource cls = new Class.Class_Resource();
        string controller_menu = "Webboard";

        public class Model
        {
            public List<TB_Webboard> Lst_TB_Webboard { get; set; }
            public TB_Webboard TB_Webboard { get; set; }
            public List<TB_Comment> TB_Comment { get; set; }
            public string New_Comment { get; set; }
            public bool IsAdmin { get; set; }
            public string UserID { get; set; }
        }

        public WebboardController(CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Webboard
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

                    Model data = new Model();
                    string session_user = HttpContext.Session.GetString("session_user");
                    if (session_user == null)
                    {
                        ViewBag.MyAlert = cls.MyAlert("false", "Please login before create topic.");
                        return RedirectToAction("Index", "Login");
                    }

                    if (HttpContext.Session.GetString("MyAlert") != null)
                    {
                        ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                        HttpContext.Session.Remove("MyAlert");
                    }

                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    if (list_user.Level_ID == 2) data.IsAdmin = true;
                    data.UserID = list_user.User_ID;
                    //data.Lst_TB_Webboard = await _context.TB_Webboard.ToListAsync();
                    data.Lst_TB_Webboard = await (from w in _context.TB_Webboard
                                                  join m in _context.TB_Member on w.Createby equals m.User_ID
                                                  where w.Active == true
                                                  select new TB_Webboard
                                                  {
                                                      ID = w.ID,
                                                      Active = w.Active,
                                                      Createby = w.Createby,
                                                      CreateDate = w.CreateDate,
                                                      Description = w.Description,
                                                      Img_Gallary_ID = w.Img_Gallary_ID,
                                                      Img_Url = w.Img_Url,
                                                      Other = w.Other,
                                                      Title = w.Title,
                                                      image_user = m.Img_Url,
                                                      name_user = m.Name + " " + m.Sub_Name
                                                  }
                        ).OrderByDescending(o => o.CreateDate).ToListAsync();

                    return View(data);
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

                return View(await _context.TB_Webboard.OrderByDescending(x => x.CreateDate).ToListAsync());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Manage(TB_Webboard tB_Webboard)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
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

                return View(await _context.TB_Webboard.Where(x =>
                tB_Webboard.Active != null ? x.Active.Equals(tB_Webboard.Active) : true
                ).OrderByDescending(x => x.CreateDate).ToListAsync());
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: Webboard/Details/5
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
                    Model data = new Model();

                    string session_user = HttpContext.Session.GetString("session_user");
                    if (session_user == null)
                    {
                        ViewBag.MyAlert = cls.MyAlert("false", "Please login before create topic.");
                        return RedirectToAction("Index", "Login");
                    }

                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    if (list_user.Level_ID == 2)
                    {
                        data.IsAdmin = true;
                        ViewData["IsAdmin"] = "true";
                    }
                    else
                    {
                        ViewData["IsAdmin"] = "false";
                    }

                    // USER            
                    data.UserID = list_user.User_ID;

                    //WEBBOARD DATA
                    //data.TB_Webboard = await _context.TB_Webboard.Include(m => m.member)
                    //    .FirstOrDefaultAsync(m => m.ID == id);
                    data.TB_Webboard = await (from w in _context.TB_Webboard
                                              join m in _context.TB_Member on w.Createby equals m.User_ID
                                              where w.ID == id
                                              select new TB_Webboard
                                              {
                                                  ID = w.ID,
                                                  Active = w.Active,
                                                  Createby = w.Createby,
                                                  CreateDate = w.CreateDate,
                                                  Description = w.Description,
                                                  Img_Gallary_ID = w.Img_Gallary_ID,
                                                  Img_Url = w.Img_Url,
                                                  Other = w.Other,
                                                  Title = w.Title,
                                                  image_user = m.Img_Url,
                                                  name_user = m.Name + " " + m.Sub_Name
                                              }
                        ).SingleOrDefaultAsync();

                    //data.TB_Comment = await _context.TB_Comment.Where(x => x.Board_ID == id)
                    data.TB_Comment = await (from w in _context.TB_Comment
                                             join m in _context.TB_Member on w.Createby equals m.User_ID
                                             where w.Board_ID == id
                                             select new TB_Comment
                                             {
                                                 ID = w.ID,
                                                 Active = w.Active,
                                                 Createby = w.Createby,
                                                 CreateDate = w.CreateDate,
                                                 Board_ID = w.Board_ID,
                                                 Comment = w.Comment,
                                                 image_user = m.Img_Url,
                                                 name_user = m.Name + " " + m.Sub_Name
                                             }
                        ).OrderBy(o=>o.CreateDate).ToListAsync();

                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        public async Task<IActionResult> Details_admin(int? id)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;

                if (id == null)
                {
                    return NotFound();
                }
                Model data = new Model();

                string session_user = HttpContext.Session.GetString("session_user");
                if (session_user == null)
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Please login before create topic.");
                    return RedirectToAction("Index", "Login");
                }

                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                if (list_user.Level_ID == 2)
                {
                    data.IsAdmin = true;
                    ViewData["IsAdmin"] = "true";
                }
                else
                {
                    ViewData["IsAdmin"] = "false";
                }

                // USER            
                data.UserID = list_user.User_ID;

                //WEBBOARD DATA
                //data.TB_Webboard = await _context.TB_Webboard.Include(m => m.member)
                //    .FirstOrDefaultAsync(m => m.ID == id);
                data.TB_Webboard = await (from w in _context.TB_Webboard
                                          join m in _context.TB_Member on w.Createby equals m.User_ID
                                          where w.ID == id
                                          select new TB_Webboard
                                          {
                                              ID = w.ID,
                                              Active = w.Active,
                                              Createby = w.Createby,
                                              CreateDate = w.CreateDate,
                                              Description = w.Description,
                                              Img_Gallary_ID = w.Img_Gallary_ID,
                                              Img_Url = w.Img_Url,
                                              Other = w.Other,
                                              Title = w.Title,
                                              image_user = m.Img_Url,
                                              name_user = m.Name + " " + m.Sub_Name
                                          }
                    ).SingleOrDefaultAsync();

                //data.TB_Comment = await _context.TB_Comment.Where(x => x.Board_ID == id)
                data.TB_Comment = await (from w in _context.TB_Comment
                                         join m in _context.TB_Member on w.Createby equals m.User_ID
                                         where w.Board_ID == id
                                         select new TB_Comment
                                         {
                                             ID = w.ID,
                                             Active = w.Active,
                                             Createby = w.Createby,
                                             CreateDate = w.CreateDate,
                                             Board_ID = w.Board_ID,
                                             Comment = w.Comment,
                                             image_user = m.Img_Url,
                                             name_user = m.Name + " " + m.Sub_Name
                                         }
                    ).OrderBy(o => o.CreateDate).ToListAsync();

                return View(data);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        //public async Task<IActionResult> Comment(Model data)
        //{
        //    string session_user = HttpContext.Session.GetString("session_user");
        //    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);

        //    int boardid = data.TB_Webboard.ID;

        //    _context.Add(new TB_Comment() { Board_ID = boardid, Comment = data.New_Comment, CreateDate = DateTime.Now, Createby = list_user.User_ID });
        //    await _context.SaveChangesAsync();

        //    data.TB_Webboard = await _context.TB_Webboard
        //        .FirstOrDefaultAsync(m => m.ID == boardid);
        //    data.TB_Comment = await _context.TB_Comment.Where(x => x.Board_ID == boardid)
        //        .ToListAsync();
        //    data.New_Comment = string.Empty;

        //    return View("Details", data);
        //}

        [HttpPost, ActionName("Comment")]
        public async Task<IActionResult> Comment(int Wb_id, string text)
        {
            int count_text = text.Length;
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;

                string session_user = HttpContext.Session.GetString("session_user");
                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                var tB_Comment = new TB_Comment() { Board_ID = Wb_id, Comment = text, CreateDate = DateTime.Now, Createby = list_user.User_ID };

                bool State = true;
                try
                {
                    _context.Add(tB_Comment);

                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    State = false;
                }
                

                //Save Log
                cls.Save_Log(_context, controller_menu, "Comment", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Comment));
                return Ok(State);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        // GET: Webboard/Create
        public IActionResult Create()
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;

                string session_user = HttpContext.Session.GetString("session_user");
                if (session_user == null)
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Please login before create topic.");
                    return RedirectToAction("Index", "Login");
                }

                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                if (list_user.Level_ID == 2)
                {
                    ViewData["IsAdmin"] = "true";
                }
                else
                {
                    ViewData["IsAdmin"] = "false";
                }

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

        public IActionResult Create_admin()
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;

                string session_user = HttpContext.Session.GetString("session_user");
                if (session_user == null)
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Please login before create topic.");
                    return RedirectToAction("Index", "Login");
                }

                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                if (list_user.Level_ID == 2)
                {
                    ViewData["IsAdmin"] = "true";
                }
                else
                {
                    ViewData["IsAdmin"] = "false";
                }

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
        // POST: Webboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TB_Webboard tB_Webboard, List<IFormFile> uploadfile)
        {
            //string menu_allow = "";
            //if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            //{
            //    ViewData["menu_allow"] = menu_allow;

            //}
            //else
            //{
            //    return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            //}
            try
            {
                if (ModelState.IsValid)
                {
                    string folder_type = "images";
                    string folder_image = "Webboard";
                    string file_response = "", error_message = "";

                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);

                    tB_Webboard.Active = true;
                    tB_Webboard.Createby = list_user.User_ID;
                    tB_Webboard.CreateDate = DateTime.Now;

                    if (uploadfile.Count > 0)
                    {
                        if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            tB_Webboard.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                        else
                        {
                            tB_Webboard.Img_Url = "";
                            ModelState.AddModelError("Img_Url", error_message);
                            goto ExitError;
                        }
                    }
                    //tB_Webboard.member = GetUserById(tB_Webboard.Createby);
                    _context.Add(tB_Webboard);

                    await _context.SaveChangesAsync();


                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Save data completed."));
                    HttpContext.Session.SetString("MyAlert", json);
                    //Save Log
                    cls.Save_Log(_context, controller_menu, "Create", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Webboard));

                ExitError:;
                    if(list_user.Level_ID == 2)
                    {
                        return RedirectToAction(nameof(Create_admin));
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _context.TB_Log.Add(new TB_Log() { Message = ex.Message.ToString(), CreateDate = DateTime.Now });
                await _context.SaveChangesAsync();
            }

            return View(tB_Webboard);
        }

        public TB_Member GetUserById(string user_id)
        {
            var data = _context.TB_Member.Where(x => x.User_ID.Equals(user_id)).FirstOrDefault();

            return data;
        }

        // GET: Webboard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                string session_user = HttpContext.Session.GetString("session_user");
                if (session_user == null)
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Please login before create topic.");
                    return RedirectToAction("Index", "Login");
                }

                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                if (list_user.Level_ID == 2)
                {
                    ViewData["IsAdmin"] = "true";
                }
                else
                {
                    ViewData["IsAdmin"] = "false";
                }

                if (id == null)
                {
                    return NotFound();
                }

                var tB_Webboard = await _context.TB_Webboard.FindAsync(id);
                if (tB_Webboard == null)
                {
                    return NotFound();
                }
                return View(tB_Webboard);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
            
        }

        public async Task<IActionResult> Edit_admin(int? id)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                string session_user = HttpContext.Session.GetString("session_user");
                if (session_user == null)
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Please login before create topic.");
                    return RedirectToAction("Index", "Login");
                }

                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                if (list_user.Level_ID == 2)
                {
                    ViewData["IsAdmin"] = "true";
                }
                else
                {
                    ViewData["IsAdmin"] = "false";
                }

                if (id == null)
                {
                    return NotFound();
                }

                var tB_Webboard = await _context.TB_Webboard.FindAsync(id);
                if (tB_Webboard == null)
                {
                    return NotFound();
                }
                return View(tB_Webboard);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }

        }

        // POST: Webboard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,Title,Description,Other,Img_Url,Img_Gallary_ID,Active,Createby,CreateDate")] TB_Webboard tB_Webboard, List<IFormFile> uploadfileEdit)
        {
            string menu_allow = "", status = "false", msg = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                if (ModelState.IsValid)
                {
                    //Function Save
                    if (Edit_data(id, tB_Webboard, uploadfileEdit, ref msg, ref status))
                    {
                        string json = JsonConvert.SerializeObject(cls.MyAlert(status, msg));
                        HttpContext.Session.SetString("MyAlert", json);
                        return RedirectToAction(nameof(Index));
                    }
                }

                ViewBag.MyAlert = cls.MyAlert(status, msg);
                return View(tB_Webboard);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_admin(int id, [Bind("ID,Title,Description,Other,Img_Url,Img_Gallary_ID,Active,Createby,CreateDate")] TB_Webboard tB_Webboard, List<IFormFile> uploadfileEdit)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                string status = "", msg = "";
                if (ModelState.IsValid)
                {
                    //Function Save
                    Edit_data(id, tB_Webboard, uploadfileEdit, ref msg, ref status);
                }

                ViewBag.MyAlert = cls.MyAlert(status, msg);
                return View(tB_Webboard);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        private bool Edit_data(int id,TB_Webboard tB_Webboard, List<IFormFile> uploadfileEdit,ref string msg,ref string status)
        {
            bool res = true;

            string session_user = HttpContext.Session.GetString("session_user");
            var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
            status = "true";

            if (ModelState.IsValid)
            {
                try
                {
                    string folder_type = "images";
                    string folder_image = "Webboard";
                    string file_response = "", error_message = "";

                    if (uploadfileEdit.Count > 0)
                    {
                        if (cls.MoveFile(uploadfileEdit, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            tB_Webboard.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                    }
                    tB_Webboard.Active = (tB_Webboard.Active == true ? true : false);
                    _context.Update(tB_Webboard);
                    _context.SaveChanges();

                    msg = "Save data completed.";
                    //Save Log
                    cls.Save_Log(_context, controller_menu, "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Webboard));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    msg = "Error : " + ex.Message.ToString();
                    status = "false";
                    if (!TB_WebboardExists(tB_Webboard.ID))
                    {
                        res = false;
                    }
                    else
                    {
                        _context.TB_Log.Add(new TB_Log() { Message = ex.ToString(), CreateDate = DateTime.Now });
                        _context.SaveChanges();
                    }
                }
            }

            return res;
        }

        // POST: Webboard/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;
                TB_Webboard tB_Webboard = new TB_Webboard();
                List<TB_Comment> tB_Comment = new List<TB_Comment>();
                bool chk_transaction = true;
                var transaction = _context.Database.BeginTransaction();
                //Webboard
                try
                {
                    tB_Webboard = await _context.TB_Webboard.FindAsync(id);
                    _context.TB_Webboard.Remove(tB_Webboard);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    chk_transaction = false;
                    goto ExitSave;
                }
                //Comment
                try
                {
                    tB_Comment = await _context.TB_Comment.Where(x => x.Board_ID == id).ToListAsync();
                    _context.TB_Comment.RemoveRange(tB_Comment);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    chk_transaction = false;
                    goto ExitSave;
                }
                //Save Transaction ทั้งหมด
                ExitSave:;
                if (chk_transaction == true)
                {
                    transaction.Commit();
                    ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");

                    //Save Log
                    string session_user = HttpContext.Session.GetString("session_user");
                    var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                    cls.Save_Log(_context, controller_menu, "Delete", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), (JsonConvert.SerializeObject(tB_Webboard) + " - " + JsonConvert.SerializeObject(tB_Comment)));
                }
                else
                {
                    transaction.Rollback();
                    ViewBag.MyAlert = cls.MyAlert("false", "The field is required.");
                }
                return View(tB_Webboard);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        [HttpPost, ActionName("DeleteComment")]
        public async Task<IActionResult> DeleteCommentConfirmed(int Wb_id, int id)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;

                var tB_Comment = await _context.TB_Comment.FindAsync(id);
                _context.TB_Comment.Remove(tB_Comment);
                await _context.SaveChangesAsync();

                //Save Log
                string session_user = HttpContext.Session.GetString("session_user");
                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                cls.Save_Log(_context, controller_menu, "DeleteComment", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), (JsonConvert.SerializeObject(tB_Comment)));
                return Ok();
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }


        [HttpPost, ActionName("EditComment")]
        public async Task<IActionResult> EditComment(int Wb_id, int id , string text)
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;

                var tB_Comment = await _context.TB_Comment.Where(x => x.Board_ID == Wb_id && x.ID == id).FirstOrDefaultAsync();
                tB_Comment.Comment = text;
                tB_Comment.CreateDate = DateTime.Now;
                await _context.SaveChangesAsync();
                //Save Log
                string session_user = HttpContext.Session.GetString("session_user");
                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                cls.Save_Log(_context, controller_menu, "EditComment", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), (JsonConvert.SerializeObject(tB_Comment)));
                return Ok();
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        private bool TB_WebboardExists(int id)
        {
            return _context.TB_Webboard.Any(e => e.ID == id);
        }
    }
}
