using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CONSULS_SYSTEM.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZXing.QrCode;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using CONSULS_SYSTEM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using CONSULS_SYSTEM.DATA;
using Microsoft.Extensions.Configuration;

namespace CONSULS_SYSTEM.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SERVICESController : ControllerBase
    {
        private readonly CONSULS_Context _context;
        Class.Class_Resource cls = new Class.Class_Resource();
        IHostingEnvironment _hostingEnvironment;
        public static IConfiguration Configuration { get; set; }

        public SERVICESController(IOptions<Config> settings, CONSULS_Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        [ActionName("GET_TEST")]
        public IActionResult GET_TEST([FromQuery(Name = "testparam")] string testparam, [FromQuery(Name = "aof")] string aof)
        {
            return Ok(testparam + " - " + aof);
        }
        [ActionName("GET_TEST2")]
        public void GET_TEST2([FromQuery(Name = "testparam")] string testparam, [FromQuery(Name = "aof")] string aof)
        {
            //
        }

        [ActionName("GET_LINEQR/{size}/{userid}")]
        [HttpGet]
        public FileResult GET_LINEQR(string size, string userid)
        {
            if (size == null)
            {
                size = "200";
            }
            Class_Resource cls = new Class_Resource();
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

        [ActionName("LINE_REGISTER")]
        public IActionResult LINE_REGISTER([FromQuery(Name = "code")] string code, [FromQuery(Name = "state")] string state)
        {
            List<dynamic> response = new List<dynamic>();
            string result = "";
            bool status = false;
            if (code!=null && state != null)
            {
                if (code != "" && state != "")
                {
                    try
                    {
                        string LINE_CLIENT_ID = Configuration["LineSetting:LINE_CLIENT_ID"].ToString();
                        string LINE_CLIENT_SECRET = Configuration["LineSetting:LINE_CLIENT_SECRET"].ToString();
                        string LINE_CALLBACK_FNC = Configuration["LineSetting:LINE_CALLBACK_FNC"].ToString();

                        //Get Token by code.
                        WebClient wc = new WebClient();
                        string targetAddress = "https://notify-bot.line.me/oauth/token";
                        wc.Encoding = Encoding.UTF8;
                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        NameValueCollection nc = new NameValueCollection();
                        nc["grant_type"] = "authorization_code";
                        nc["code"] = code; //the value from Step 2
                        nc["redirect_uri"] = LINE_CALLBACK_FNC;// the url your fill in Step 1
                        nc["client_id"] = LINE_CLIENT_ID; // get from https://notify-bot.line.me
                        nc["client_secret"] = LINE_CLIENT_SECRET; //get from https://notify-bot.line.me
                        byte[] bResult = wc.UploadValues(targetAddress, nc);
                        result = Encoding.UTF8.GetString(bResult);
                        //Response.WriteAsync(result);//บางทีท่อนนี้อาจจะทำให้ตอน Register แล้ว Line ไม่ยอมปิด Browser และคา Token อยู่ให้เห็น เดี๋ยวลองเอาออกแล้ว Publish ใหม่
                    }
                    catch (Exception ex)
                    {
                        _context.TB_Log.Add(new TB_Log() { Message = result + "_" + ex.Message.ToString(), CreateDate = DateTime.Now, Createby = "LineRegister" });
                        _context.SaveChanges();
                    }
                    

                    if (result != "")
                    {
                        try
                        {
                            LineStatus ls = JsonConvert.DeserializeObject<LineStatus>(result);
                            //Update Token
                            var user = _context.TB_Member.Where(x => x.User_ID.Equals(state)).SingleOrDefault();
                            user.Line_Token = ls.access_token;
                            _context.Update(user);
                            _context.SaveChanges();
                        }
                        catch(Exception ex)
                        {
                            _context.TB_Log.Add(new TB_Log() { Message = "Insert_" + ex.Message.ToString(), CreateDate = DateTime.Now, Createby = "LineRegister" });
                            _context.SaveChanges();
                        }
                    }
                }
                else
                {
                    _context.TB_Log.Add(new TB_Log() { Message = "Req=" + code + "/" + state, CreateDate = DateTime.Now, Createby = "LineRegister" });
                    _context.SaveChanges();
                }
            }

            if (status)
            {
                response.Add(new
                {
                    status = status,
                    data = result
                });
            }
            else
            {
                response.Add(new
                {
                    status = status,
                    data = "ERROR"
                });
            }

            return Ok(response);
        }

        [ActionName("DOWNLOAD")]
        [HttpGet]
        public FileResult DOWNLOAD([FromQuery(Name = "file")] string file)
        {
            //http://localhost:54053/api/SERVICES/DOWNLOAD?file=Attachment\Minutes\20181219005424.png
            try
            {
                string decrypt_filenamme = cls.Base64Decode(file);
                if (file != null)
                {
                    String fileName = _hostingEnvironment.WebRootPath + "\\" + decrypt_filenamme;//file;
                    var result = System.IO.File.ReadAllBytes(fileName);
                    var stream = new MemoryStream(result);

                    string[] arr = decrypt_filenamme.Split("\\");

                    return File(stream.ToArray(), "application/octet-stream", arr[arr.Length - 1]);
                }
                else
                {
                    var stream = new MemoryStream(Encoding.ASCII.GetBytes("Error download file"));
                    return new FileStreamResult(stream, "text/plain")
                    {
                        FileDownloadName = "Error.txt"
                    };
                }
            }
            catch (Exception ex)
            {
                var stream = new MemoryStream(Encoding.ASCII.GetBytes(ex.Message.ToString()));
                return new FileStreamResult(stream, "text/plain")
                {
                    FileDownloadName = "Error.txt"
                };
            }
        }

        public class LineStatus
        {
            public int status;
            public string message;
            public string access_token;

        }
    }
}
//test