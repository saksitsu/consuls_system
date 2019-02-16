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
using System.Transactions;

namespace CONSULS_SYSTEM.Controllers
{
    public class MessageToAdminController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        string controller_menu = "MessageToAdmin";

        public MessageToAdminController(CONSULS_Context context)
        {
            _context = context;
        }

        // GET: MessageToAdmin
        public async Task<IActionResult> Index()
        {
            if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
            {
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                return View(await _context.TB_MessageToAdmin.ToListAsync());
            }
            
        }

        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Manage(TB_MessageToAdmin tB_MessageToAdmin)
        {
            if (HttpContext.Session.GetString("MyAlert") != null)
            {
                ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                HttpContext.Session.Remove("MyAlert");
            }
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Text = "New",
                Value = "N"
            });
            list.Add(new
            {
                Text = "Readed",
                Value = "R"
            });

            ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text",tB_MessageToAdmin.Status);
            ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 3)).Distinct().ToList(), "Level_ID", "Name", tB_MessageToAdmin.Type);

            var db = await (from m in _context.TB_MessageToAdmin
                            join l in _context.MT_Level on m.Type equals l.Level_ID
                            where (tB_MessageToAdmin.Type != 0 ? m.Type.Equals(tB_MessageToAdmin.Type) : true)
                                    && (tB_MessageToAdmin.Status != null ? m.Status.Equals(tB_MessageToAdmin.Status) : true)
                            select new TB_MessageToAdmin
                            {
                                level = l,
                                ID = m.ID,
                                User_ID = m.User_ID,
                                Name = m.Name,
                                Createby = m.Createby,
                                CreateDate = m.CreateDate,
                                Email = m.Email,
                                Phone_Number = m.Phone_Number,
                                Message = m.Message,
                                Status = m.Status,
                                Title = m.Title,
                                Topic = m.Topic,
                                Type = m.Type
                            }).ToListAsync();
            return View(db);//await _context.TB_Member.Include(l => l.level).Include(c => c.country).ToListAsync()
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            if (HttpContext.Session.GetString("MyAlert") != null)
            {
                ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                HttpContext.Session.Remove("MyAlert");
            }
            List<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Text = "New",
                Value = "N"
            });
            list.Add(new
            {
                Text = "Readed",
                Value = "R"
            });

            ViewBag.ddlActive = new SelectList(list.ToList(), "Value", "Text");
            ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 3)).Distinct().ToList(), "Level_ID", "Name");

            var db = await (from m in _context.TB_MessageToAdmin
                            join l in _context.MT_Level on m.Type equals l.Level_ID
                            select new TB_MessageToAdmin
                            {
                                level = l,
                                ID = m.ID,
                                User_ID = m.User_ID,
                                Name = m.Name,
                                Createby = m.Createby,
                                CreateDate = m.CreateDate,
                                Email = m.Email,
                                Phone_Number = m.Phone_Number,
                                Message = m.Message,
                                Status = m.Status,
                                Title = m.Title,
                                Topic = m.Topic,
                                Type = m.Type
                            }).ToListAsync();
            return View(db);//await _context.TB_Member.Include(l => l.level).Include(c => c.country).ToListAsync()
        }

        // GET: MessageToAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_MessageToAdmin = await _context.TB_MessageToAdmin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tB_MessageToAdmin == null)
            {
                return NotFound();
            }

            return View(tB_MessageToAdmin);
        }

        // GET: MessageToAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageToAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Message,Createby,CreateDate,Status,Type,User_ID,Topic,Email,Name,Phone_Number")] TB_MessageToAdmin tB_MessageToAdmin)
        {
            if (ModelState.IsValid)
            {
                if (tB_MessageToAdmin.Topic == "")
                {
                    tB_MessageToAdmin.Topic = "บุคคลทั่วไป";
                }
                _context.Add(tB_MessageToAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tB_MessageToAdmin);
        }

        [HttpGet]
        public IActionResult PullMsg()
        {
            using (var t = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }))
            {
                var Member = _context.TB_MessageToAdmin.Where(x => x.Status.Equals("N") && x.Type == 1).Count();
                var Public = _context.TB_MessageToAdmin.Where(x => x.Status.Equals("N") && x.Type == 3).Count();

                List<dynamic> res = new List<dynamic>();
                res.Add(new
                {
                    Member,
                    Public
                    //,All = Member + Public
                });

                //var MsgToAdmin = _context.TB_MessageToAdmin.Where(x => x.Status.Equals("N")).ToList();
                //string StrMsgToAdmin = JsonConvert.SerializeObject(MsgToAdmin);
                //HttpContext.Session.SetString("StrMsgToAdmin", StrMsgToAdmin);

                return Ok(res);
            }
        }

        // GET: MessageToAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Class.Class_Resource cls = new Class.Class_Resource();
            int key_id = Convert.ToInt32(cls.Base64Decode(id.ToString()));
            var tB_MessageToAdmin = await _context.TB_MessageToAdmin.Where(x => x.ID == key_id).FirstOrDefaultAsync();
            if (tB_MessageToAdmin == null)
            {
                return NotFound();
            }
            tB_MessageToAdmin.Status = "R";
            _context.Update(tB_MessageToAdmin);
            await _context.SaveChangesAsync();
            ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 3)).Distinct().ToList(), "Level_ID", "Name", tB_MessageToAdmin.Type);
            ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name");

            var MsgToAdmin = _context.TB_MessageToAdmin.Where(x => x.Status.Equals("N")).ToList();
            string StrMsgToAdmin = JsonConvert.SerializeObject(MsgToAdmin);
            HttpContext.Session.SetString("StrMsgToAdmin", StrMsgToAdmin);

            return View(tB_MessageToAdmin);
        }

        // POST: MessageToAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TB_MessageToAdmin tB_MessageToAdmin,string MessageOther)
        {
            string msg = "";
            bool save_status = true;
            try
            {
                if (tB_MessageToAdmin.Country_ID == 0)//ต้องการส่งกลับหาคนที่ถาม
                {
                    if (tB_MessageToAdmin.Email != null)
                    {
                        //1.Send email to Country center
                        msg = "<b>Dear :</b> " + tB_MessageToAdmin.Name;
                        msg += "<br /><b>Message Other : </b>" + (MessageOther == null || MessageOther == "" ? "-" : MessageOther);
                        SendMail(msg, tB_MessageToAdmin.Email.Trim(), "", "");
                    }
                    else
                    {
                        msg = "104 : Error send user.";
                        save_status = false;
                        goto ExitLoop;
                    }
                }
                else//ส่งกลับหา
                {
                    var country = await _context.MT_Country.Where(x => x.Country_ID == tB_MessageToAdmin.Country_ID).FirstOrDefaultAsync();
                    if (country != null && country.Email != null)
                    {
                        //1.Send email to Country center
                        msg = "<b>From :</b> " + tB_MessageToAdmin.Name;
                        msg += "<br /><b>Email : </b>" + tB_MessageToAdmin.Email;
                        msg += "<br /><b>Tel. : </b>" + tB_MessageToAdmin.Phone_Number;
                        msg += "<br /><b>Title : </b>" + tB_MessageToAdmin.Title;
                        msg += "<br /><b>Topic : </b>" + tB_MessageToAdmin.Topic;
                        msg += "<br /><b>Message : </b>" + tB_MessageToAdmin.Message;
                        msg += "<br /><b>Message Other : </b>" + (MessageOther == null || MessageOther == "" ? "-" : MessageOther);
                        if (country.Email != null)
                        {
                            SendMail(msg, country.Email.Trim(), "", "");
                        }
                        else
                        {
                            msg = "102 : Error send email Country center";
                            save_status = false;
                            goto ExitLoop;
                        }

                        //2.Send Line notify
                        var line_list = await _context.TB_Member.Where(x => x.Level_ID == 1
                        && x.Active == true
                        && (!x.Line_Token.Equals("") && x.Line_Token != null)
                        && x.Country_ID == tB_MessageToAdmin.Country_ID).ToListAsync();
                        msg = "ขณะนี้มีผู้ติดต่อสอบถามจากคุณ " + tB_MessageToAdmin.Name + " ในเรื่อง " + tB_MessageToAdmin.Title + " กรุณาตรวจสอบข้อมูลทาง Email ";
                        if (line_list.Count > 0)
                        {
                            foreach (var item in line_list)
                            {
                                SendLine(msg, item.Line_Token, "");
                            }
                        }
                        else
                        {
                            msg = "102 : Error send line user contact";
                            save_status = false;
                            goto ExitLoop;
                        }

                        //3.Send email to User contact
                        msg = "<b>Dear :</b>" + tB_MessageToAdmin.Name;
                        msg += "<br /><br />Thank you for contact. The message has been sent to the relevant person. Please wait for a reply.";
                        msg += "<br /><br />---------------------------------------------------";
                        msg += "<br /><br />ขอบคุณที่ติดต่อหาเรา ขณะนี้ข้อความได้ถูกส่งไปยังผู้ที่เกี่ยวข้องแล้ว กรุณารอการติดต่อกลับจากผู้ที่เกี่ยวข้อง";
                        if (tB_MessageToAdmin.Email != null)
                        {
                            SendMail(msg, tB_MessageToAdmin.Email.Trim(), "", "");
                        }
                        else
                        {
                            msg = "103 : Error send email user contact";
                            save_status = false;
                            goto ExitLoop;
                        }


                    }
                    else
                    {
                        msg = "101 : Incorrect country information.";
                        save_status = false;
                        goto ExitLoop;
                    }
                //tB_MessageToAdmin.Status = (tB_MessageToAdmin.Status == null ? "N" : "R");
                //_context.Update(tB_MessageToAdmin);
                //await _context.SaveChangesAsync();

                
                }
                
                
            }
            catch(Exception ex)
            {
                msg = ex.Message.ToString();
                ViewBag.MyAlert = cls.MyAlert("false", msg);
            }

        ExitLoop:;
            if (save_status)
            {
                ViewBag.MyAlert = cls.MyAlert("true", "Forword completed.");
            }
            else
            {
                ViewBag.MyAlert = cls.MyAlert("false", msg);
            }

            //Save Log
            string session_user = HttpContext.Session.GetString("session_user");
            var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
            cls.Save_Log(_context, controller_menu, "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_MessageToAdmin));
            ViewBag.ddlLevel = new SelectList(_context.MT_Level.Where(x => x.Active == true && (x.Level_ID == 1 || x.Level_ID == 3)).Distinct().ToList(), "Level_ID", "Name", tB_MessageToAdmin.Type);
            ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name");

            return View(tB_MessageToAdmin);
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
            Task.Factory.StartNew(() => cls.SendmailMoreFile(mailTo, mailCC, ("HCAT New message."), message, fileAttach));
        }
        /// <summary>
        /// Async
        /// </summary>
        /// <param name="message"></param>
        /// <param name="Token"></param>
        private void SendLine(string message, string Token, string fileAttach)
        {
            Task.Factory.StartNew(() => cls.SendLineNotify(Token, message));
        }

        // GET: MessageToAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tB_MessageToAdmin = await _context.TB_MessageToAdmin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tB_MessageToAdmin == null)
            {
                return NotFound();
            }

            return View(tB_MessageToAdmin);
        }

        // POST: MessageToAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tB_MessageToAdmin = await _context.TB_MessageToAdmin.FindAsync(id);
            _context.TB_MessageToAdmin.Remove(tB_MessageToAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TB_MessageToAdminExists(int id)
        {
            return _context.TB_MessageToAdmin.Any(e => e.ID == id);
        }
    }
}
