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
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http.Features;

namespace CONSULS_SYSTEM.Controllers
{
    public class LoginController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        public static IConfiguration Configuration { get; set; }

        public LoginController(CONSULS_Context context)
        {
            _context = context;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // GET: Login
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("MyAlert") != null)
            {
                ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                HttpContext.Session.Remove("MyAlert");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Username,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.TB_Member.Include(l => l.level).Where(x => x.Active.Equals(true) && x.User_ID.Equals(login.Username) && x.Password.Equals(cls.EncodeHMAC_SHA512(login.Password))).FirstOrDefaultAsync();

                //User+Pass ตรง ให้เก็บค่าใน Session
                if (user != null)
                {
                    string Json_User = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("session_user", Json_User);

                    var menu_member = await _context.TB_Menu_Authorize.Include(i => i.menu_info)
                        .Where(x => x.Level_ID == user.Level_ID && x.menu_info.Active == true)
                        .Distinct()
                        .OrderBy(o => o.menu_info.Sorting)
                        .ToListAsync();
                    string Json_menu_member = JsonConvert.SerializeObject(menu_member);
                    HttpContext.Session.SetString("session_menu", Json_menu_member);

                    //Save Log
                    cls.Save_Log(_context, "Login", "Index", cls.GetIPAddress(), cls.get_mac_address(), user.User_ID, "-", user.Level_ID, Json_User);

                    if (user.Level_ID == 2)//Admin
                    {
                        var MsgToAdmin = _context.TB_MessageToAdmin.Where(x => x.Status.Equals("N")).ToList();
                        string StrMsgToAdmin = JsonConvert.SerializeObject(MsgToAdmin);
                        HttpContext.Session.SetString("StrMsgToAdmin", StrMsgToAdmin);

                        return RedirectToAction("Manage", "MessageToAdmin");
                    }
                    else//Member or etc.
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.MyAlert = cls.MyAlert("false", "Username or password is invalid.");
                    ModelState.AddModelError("Validation", "Username or Passowrd is invalid.");
                }
            }
            
            return View(login);
        }

        public IActionResult forgot_password()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> forgot_password(Login login)
        {
            ModelState.Clear();
            if (login.email == null)
            {
                ModelState.AddModelError("email", "This field is required.");
                ViewBag.MyAlert = cls.MyAlert("false", "This field is required.");
                goto ExitError;
            }
            else
            {
                var user = await _context.TB_Member.Where(x => x.Email.Equals(login.email) && x.Active == true).FirstOrDefaultAsync();
                if (user != null)
                {
                    string user_encode = cls.Base64Encode(user.User_ID);
                    string message = "Your account : " + user.User_ID + "<br />";
                    message += "Please continue to change password <a href='" + Configuration["Config:Domain"].ToString() + "/Login/change_password?u=" + user_encode + "'> Click </a>";
                    SendMail(message, user.Email, "", "");

                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Please check your E-mail address."));
                    HttpContext.Session.SetString("MyAlert", json);

                    //Save Log
                    cls.Save_Log(_context, "Login", "forgot_password", cls.GetIPAddress(), cls.get_mac_address(), (user == null ? "" : user.User_ID), "-", (user == null ? 0 : user.Level_ID), message);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("email", "Data not found.");
                    ViewBag.MyAlert = cls.MyAlert("false", "Data not found.");
                    //Save Log
                    cls.Save_Log(_context, "Login", "forgot_password", cls.GetIPAddress(), cls.get_mac_address(), (user == null ? "" : user.User_ID), "-", (user == null ? 0 : user.Level_ID), "Data not found.");
                    goto ExitError;
                }
            }

        ExitError:;
            return View(login);
        }
        public IActionResult change_password(string u)
        {
            if (u != null)
            {
                if (u != "")
                {
                    string user_decode = cls.Base64Decode(u);
                    Login l = new Login();
                    l.Username = user_decode;

                    return View(l);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> change_password(Login login,string confirm)
        {
            if (ModelState.IsValid)
            {
                if(confirm == login.Password)
                {
                    var user = await _context.TB_Member.Where(x => x.User_ID.Equals(login.Username) && x.Active == true).FirstOrDefaultAsync();
                    user.Password = cls.EncodeHMAC_SHA512(login.Password);

                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    string json = JsonConvert.SerializeObject(cls.MyAlert("true", "Change new password completed."));
                    HttpContext.Session.SetString("MyAlert", json);
                    //Save Log
                    cls.Save_Log(_context, "Login", "change_password", cls.GetIPAddress(), cls.get_mac_address(), (user == null ? "" : user.User_ID), "-", (user == null ? 0 : user.Level_ID), json);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Validation", "Password mismatch");
                    ViewBag.MyAlert = cls.MyAlert("false", "Password mismatch");
                    goto ExitError;
                }
            }

        ExitError:;
            return View(login);
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
            Task.Factory.StartNew(() => cls.SendmailMoreFile(mailTo, mailCC, ("HCAT FORGOT PASSWORD"), message, fileAttach));
        }
        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }
        
    }
}
