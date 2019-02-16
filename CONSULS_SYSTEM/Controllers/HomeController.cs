using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CONSULS_SYSTEM.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using CONSULS_SYSTEM.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ZXing.QrCode;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CONSULS_SYSTEM.Controllers
{
    public class HomeController : Controller
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        IHostingEnvironment _hostingEnvironment;
        public static IConfiguration Configuration { get; set; }
        string controller_menu = "Home";

        public HomeController(CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        public IActionResult Index()
        {
            //Test commit branch dev.
            string session_user = HttpContext.Session.GetString("session_user");
            if (session_user != null)
            {
                var user = JsonConvert.DeserializeObject<TB_Member>(session_user);
            }
            else//No login
            {
                //ถ้า User ทั่วไปจะไม่ได้ Login จะ Hardcode Level user ระดับ 0 ซึ่งจะดึง Menu Default จาก Database
                var menu_non_member = _context.TB_Menu_Authorize.Include(i => i.menu_info)
                    .Where(x => x.Level_ID == 0 && x.menu_info.Active == true)
                    .Distinct()
                    .OrderBy(o => o.menu_info.Sorting)
                    .ToList();
                string Json_non_menu_member = JsonConvert.SerializeObject(menu_non_member);
                HttpContext.Session.SetString("session_menu", Json_non_menu_member);
            }
            if (HttpContext.Session.GetString("MyAlert") != null)
            {
                ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                HttpContext.Session.Remove("MyAlert");
            }
            ViewBag.Partner = _context.MT_Partner.Where(x => x.Active == true).ToList();
            ViewBag.Home = _context.MT_Home.Where(x => x.Active == true).SingleOrDefault();

            if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
            {
                return RedirectToAction("Manage", "MessageToAdmin");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Manage()
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                ViewData["menu_allow"] = menu_allow;

                var model = _context.MT_Home.SingleOrDefault();
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    MT_Home h = new MT_Home();
                    return View(h);
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }
        [HttpGet]
        public string GET_Content()
        {
            var model = _context.MT_Home.Where(x => x.Active == true).SingleOrDefault();
            if (model != null)
            {
                return model.Message.ToString();
            }
            else
            {
                return "Content";
            }
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Manage(MT_Home h, List<IFormFile> uploadfile)
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
                        string folder_image = "Home";
                        string file_response = "", error_message = "";
                        //ข้อมูล User
                        string session_user = HttpContext.Session.GetString("session_user");
                        var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                        h.Createby = list_user.User_ID;//"aof";//User Session
                        h.CreateDate = DateTime.Now;
                        h.Active = true;
                        //Save file รูป
                        if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                        {
                            h.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                        }
                        //else
                        //{
                        //    h.Img_Url = "";
                        //    ModelState.AddModelError("Img_Url", error_message);
                        //    goto ExitError;
                        //}

                        if (TB_HomeExists(h.ID))//True => สร้างไว้แล้ว
                        {
                            _context.Update(h);
                        }
                        else//ยังไม่เคยสร้างไว้
                        {
                            _context.Add(h);
                        }
                        _context.SaveChanges();

                        ViewBag.MyAlert = cls.MyAlert("true", "Save data completed.");
                        //Save Log
                        cls.Save_Log(_context, "Home", "Manage", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(h));
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

            ExitError:;
                return View(h);
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }

        public IActionResult Profile()
        {
            string menu_allow = "";
            if (cls.chk_Authorize(controller_menu, HttpContext.Session.GetString("session_menu"), HttpContext.Session.GetString("session_user"), ref menu_allow, _context))
            {
                if (cls.chk_Level_Layout(HttpContext.Session.GetString("session_user")))
                {
                    return RedirectToAction("Manage", "MessageToAdmin");
                }
                else
                {
                    if (HttpContext.Session.GetString("MyAlert") != null)
                    {
                        ViewBag.MyAlert = JsonConvert.DeserializeObject<MyAlert>(HttpContext.Session.GetString("MyAlert"));
                        HttpContext.Session.Remove("MyAlert");
                    }

                    ViewData["param_qr"] = Configuration["Config:Domain"].ToString() + "/Home/GET_LINEQR";
                    var Json_user = HttpContext.Session.GetString("session_user");
                    var List_user = new CONSULS_SYSTEM.Models.TB_Member();
                    if (Json_user != null)
                    {
                        List_user = JsonConvert.DeserializeObject<CONSULS_SYSTEM.Models.TB_Member>(Json_user);
                    }

                    ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", List_user.Country_ID);
                    return View(List_user);
                }
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "Error : This page is not allowed. Please check your authorize." });
            }
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Profile(TB_Member tB_Member, List<IFormFile> uploadfile, string ChangePassword, string ConfirmPassword)
        {
            string msg = "";
            bool status = true;
            if (ModelState.IsValid)
            {
                try
                {
                    //1.Check password
                    if (ChangePassword != null && ConfirmPassword != null)//Incase user change password
                    {
                        if (ChangePassword != ConfirmPassword)//Incase user input password mismatch
                        {
                            ModelState.AddModelError("Password", "New password mismatch. Please check");
                            msg = "New password mismatch. Please check";
                            status = false;
                            goto ExitError;
                        }                       
                        else
                        {
                            tB_Member.Password = cls.EncodeHMAC_SHA512(ConfirmPassword);//Incase user change matching
                        }
                    }

                    //2.Save file รูป
                    string folder_type = "images";
                    string folder_image = "Member";
                    string file_response = "", error_message = "";
                    if (cls.MoveFile(uploadfile, _hostingEnvironment, folder_type, folder_image, ref file_response, ref error_message))
                    {
                        tB_Member.Img_Url = string.Format("~/" + folder_type + "/" + folder_image + "/{0}", file_response);
                    }

                    string Json_User = JsonConvert.SerializeObject(tB_Member);
                    HttpContext.Session.SetString("session_user", Json_User);
                }
                catch (Exception ex)
                {

                }

                ////เป็นไรไม่รู้
                //var data_add = tB_Member;
                ////Delete
                //try
                //{
                //    _context.Remove(tB_Member);
                //    await _context.SaveChangesAsync();
                //}
                //catch(Exception ex) { }
                ////Insert
                //try
                //{
                //    _context.Add(tB_Member);
                //    await _context.SaveChangesAsync();
                //}
                //catch(Exception ex) { }

                _context.Update(tB_Member);
                await _context.SaveChangesAsync();

                msg = "Update Profile completed.";

                //Save Log
                string session_user = HttpContext.Session.GetString("session_user");
                var list_user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                cls.Save_Log(_context, "Events", "Edit", cls.GetIPAddress(), cls.get_mac_address(), (list_user == null ? "" : list_user.User_ID), "-", (list_user == null ? 0 : list_user.Level_ID), JsonConvert.SerializeObject(tB_Member));
            }
            else
            {
                msg = "This field is required.";
                status = false;
            }

        ExitError:;
            ViewData["param_qr"] = Configuration["Config:Domain"].ToString() + "/Home/GET_LINEQR";
            //var Json_user = HttpContext.Session.GetString("session_user");
            //var List_user = new CONSULS_SYSTEM.Models.TB_Member();
            //if (Json_user != null)
            //{
            //    List_user = JsonConvert.DeserializeObject<CONSULS_SYSTEM.Models.TB_Member>(Json_user);
            //}
            ViewBag.ddlCountry = new SelectList(_context.MT_Country.Where(x => x.Active == true).Distinct().ToList(), "Country_ID", "Name", tB_Member.Country_ID);
            ViewBag.MyAlert = cls.MyAlert(status.ToString().ToLower(), msg);

            return View(tB_Member);
        }
        private bool TB_MemberExists_Email(string Email)
        {
            return _context.TB_Member.Any(e => e.Email == Email);
        }
        [ActionName("GET_LINEQR")]
        [HttpGet]
        public FileResult GET_LINEQR()//string size, string userid
        {
            string size="250"; string userid="";
            //Test commit branch dev.
            string session_user = HttpContext.Session.GetString("session_user");
            if (session_user != null)
            {
                var user = JsonConvert.DeserializeObject<TB_Member>(session_user);
                userid = user.User_ID;
            }
            string LINE_CLIENT_ID = Configuration["LineSetting:LINE_CLIENT_ID"].ToString();
            string LINE_CALLBACK_FNC = Configuration["LineSetting:LINE_CALLBACK_FNC"].ToString();
            string param = @"https://notify-bot.line.me/oauth/authorize?response_type=code&client_id=" + LINE_CLIENT_ID + "&redirect_uri=" + LINE_CALLBACK_FNC + "&scope=notify&state=" + userid;

            try
            {
                //Create QRCode
                var qrWriter = new ZXing.BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions { Height = Convert.ToInt32(size), Width = Convert.ToInt32(size), Margin = 0 }
                };

                var pixelData = qrWriter.Write(param);

                using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                using (var ms = new MemoryStream())
                {
                    // lock the data area for fast access
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                       System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    Bitmap QRBBitmap;
                    Bitmap QRBImage = bitmap;
                    QRBBitmap = QRBImage;
                    ms.Flush();
                    var graphics = Graphics.FromImage(QRBImage);
                    graphics.CompositingMode = CompositingMode.SourceOver;

                    //int overlaywidth = overlayImage.Width / 2;
                    //int overlayheight = overlayImage.Height / 2;

                    //graphics.DrawImage(overlayImage, (Convert.ToInt32(size) - 50) / 2, (Convert.ToInt32(size) - 50) / 2);
                    graphics.Save();
                    // save to stream as PNG
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                    return File(ms.ToArray(), "image/png"); //,"QRCode_Generate.png" หากจะDownload ลง
                }
            }
            catch
            {
                var stream = new MemoryStream(Encoding.ASCII.GetBytes("Error!"));
                return new FileStreamResult(stream, "text/plain")
                {
                    FileDownloadName = "Error.txt"
                };
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(returnUrl); //LocalRedirect
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string msg)
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            string session = HttpContext.Session.GetString("session_user");
            if (session != null)
            {
                clear_session();

                if (msg == null || msg == "")
                {
                    //Error อย่างอื่น
                    var error = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                    string json = JsonConvert.SerializeObject(cls.MyAlert("false", "Error : " + error.RequestId.ToString()));
                    HttpContext.Session.SetString("MyAlert", json);
                }
                else//มีการลักลอบเข้าเมนู
                {
                    string json = JsonConvert.SerializeObject(cls.MyAlert("false", msg));
                    HttpContext.Session.SetString("MyAlert", json);
                }
            }
            else
            {
                //หลุด Conn
                var error = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
                string json = JsonConvert.SerializeObject(cls.MyAlert("false", "Error : You're being timed out due to inactivity."));
                HttpContext.Session.SetString("MyAlert", json);
            }
            return RedirectToAction("Index", "Home");
        }

        private bool TB_HomeExists(int ID)
        {
            return _context.MT_Home.Any(e => e.ID == ID);
        }

        private void clear_session()
        {
            //มี Error ล้าง Cookie ให้หมด
            HttpContext.Session.Clear();
            //foreach (var cookie in Request.Cookies.Keys)
            //{
            //    if (cookie.ToString().IndexOf("SESSION_") > -1)
            //    {
            //        Response.Cookies.Delete(cookie);
            //    }
            //}
        }
    }
}
