using CONSULS_SYSTEM.DATA;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CONSULS_SYSTEM.Class
{
    public class Class_Resource
    {
        public static IConfiguration Configuration { get; set; }

        public Class_Resource()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string EncodeHMAC_SHA512(string input)
        {
            string key = "Aof&Long_Developer";
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] keyByte = encoding.GetBytes(key);

            HMACSHA512 myhmacsha512 = new HMACSHA512(keyByte);

            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            var hashValue = myhmacsha512.ComputeHash(byteArray);

            return Convert.ToBase64String(hashValue);

        }

        public bool MoveFile(List<IFormFile> uploadfile, IHostingEnvironment _hostingEnvironment,string folder_type,string folder_image,ref string file_response,ref string error_message)
        {
            bool res = false;
            if (uploadfile.Count > 0)
            {
                try
                {
                    foreach (var file in uploadfile)
                    {
                        //ชื่อไฟล์ที่ Upload เข้ามา(ของเก่า)
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        //Type = .doc , .jpg , .png , etc
                        string TypeFile = System.IO.Path.GetExtension(file.FileName);
                        //ชื่อไฟล์ที่เก็บเฉพาะใน DB
                        file_response = System.DateTime.Now.ToString("yyyyMMddHHmmss", new System.Globalization.CultureInfo("en-US")) + TypeFile;
                        //ขนาดของไฟล์ เอาไว้อนาคตเช็คว่าต้อง Upload ได้ไม่เกิน
                        var Size = file.Length;
                        //ชื่อที่จะเก็บใน Folder(ของใหม่)
                        fileName = _hostingEnvironment.WebRootPath + $@"\" + folder_type + "\\" + folder_image + "\\" + file_response;//"{fileName}";
                        using (FileStream fs = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                    res = true;
                }
                catch (Exception ex)
                {
                    file_response = "";
                    error_message = "ERROR IMG 101 : " + ex.Message.ToString();
                }
            }
            else
            {
                error_message = "ERROR IMG 102 : Image null";
            }

            return res;
        }

        /// <summary>
        /// Function ใช้สำหรับส่ง Email
        /// </summary>
        /// <param name="Tos"></param>
        /// <param name="CCs"></param>
        /// <param name="Subjects"></param>
        /// <param name="Bodys"></param>
        /// <param name="pathfilenameAttachServer"></param>
        public async void SendmailMoreFile(String Tos, String CCs, String Subjects, String Bodys, String pathfilenameAttachServer)
        {
            if (!Tos.Equals(""))
            {
                String LocationFile = ""; //System.Configuration.ConfigurationSettings.AppSettings["LocationFile"].ToString();

                bool send = true;
                SmtpClient Smtpclient1 = new SmtpClient();
                MailMessage objmail = new MailMessage();

                String MAILFROM = Configuration["EmailSetting:MAILFROM"].ToString();
                String MAILFORNAME = Configuration["EmailSetting:MAILFORNAME"].ToString();
                String USERSMTP = Configuration["EmailSetting:USERSMTP"].ToString();
                String PASSSMTP = Configuration["EmailSetting:PASSSMTP"].ToString();
                String SMTPSSL = Configuration["EmailSetting:SMTPSSL"].ToString();
                String SMTP = Configuration["EmailSetting:SMTP"].ToString();
                String SMTPPORT = Configuration["EmailSetting:SMTPPORT"].ToString();

                string Header = "<b>" + Subjects + "</b><br /><br />";
                string Footer = "<br /><br /><b>Best Regards,</b><br />";
                Footer += "HCAT SYSTEM";

                string message = Header + Bodys + Footer;


                MailAddress s = new MailAddress(MAILFROM, MAILFORNAME);
                try
                {
                    objmail.From = s;
                    if (Tos != "")
                    {
                        objmail.To.Add(Tos);
                    }

                    if (CCs != "")
                    {
                        objmail.CC.Add(CCs);
                    }

                    objmail.Subject = Subjects;
                    objmail.Body = message;
                    objmail.IsBodyHtml = true;
                    objmail.Priority = MailPriority.Normal;

                    if (USERSMTP != "")
                    {
                        Smtpclient1.Credentials = new System.Net.NetworkCredential(USERSMTP, PASSSMTP);
                    }

                    if (SMTPSSL == "TRUE")
                    {
                        Smtpclient1.EnableSsl = true;
                    }
                    else
                    {
                        Smtpclient1.EnableSsl = false;
                    }

                    if (pathfilenameAttachServer != "" && pathfilenameAttachServer != null)
                    {
                        string[] bb = pathfilenameAttachServer.Split(',');

                        for (int i = 0; i < bb.Length; i++)
                        {
                            string AttFile = LocationFile + bb[i].ToString();
                            Attachment attachment = new Attachment(AttFile);
                            objmail.Attachments.Add(attachment);
                        }
                    }

                    Smtpclient1.Timeout = 60000;
                    Smtpclient1.Host = SMTP;
                    Smtpclient1.Port = Convert.ToInt32(SMTPPORT);
                    await Smtpclient1.SendMailAsync(objmail);
                }
                catch (Exception ex)
                {
                    string MsgErr = ex.Message.ToString();
                    string datenow = System.DateTime.Now.ToString("yyyyMMdd_HHmmss", new System.Globalization.CultureInfo("en-US"));
                    string path_log = Configuration["EmailSetting:path_log"].ToString();

                    using (System.IO.StreamWriter w = System.IO.File.AppendText(path_log + "LOGFILE_" + datenow + ".txt"))
                        w.WriteLine(MsgErr + " FILE : " + pathfilenameAttachServer);
                    send = false;
                }
            }
        }

        /// <summary>
        /// Function สำหรับส่ง Line Notify
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Bodys"></param>
        public async void SendLineNotify(String Token, String Bodys)
        {
            if (!Token.Equals(""))
            {
                if (Bodys.Equals(""))
                {
                    Bodys = "Bodys null";
                }
                string response = "";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify?message=" + Bodys);
                httpWebRequest.Timeout = -1;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Accept = "application/json"; // Determines the response type as XML or JSON etc
                                                            //Pass Header value
                httpWebRequest.Headers.Add("Authorization", "Bearer " + Token);

                try
                {
                    // Connecting to the server. Sending request and receiving response
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write("");//เผื่อไว้ส่งด้วย Json Form body
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd(); //เอาResponse ไว้ใช้อย่างอื่นต่อได้นะจ๊ะ
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse err = ex.Response as HttpWebResponse;
                        if (err != null)
                        {
                            string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                            response = string.Format("{0} {1}", err.StatusDescription, htmlResponse);
                        }
                    }

                }
                catch (Exception ex)
                {
                    response = "error : " + ex.Message.ToString();
                }
            }
           
        }

        /// <summary>
        /// เป็น Function ใช้ในการแปลงข้อมูลไปใช้ในการ Alert
        /// ข่อมูลStatus (AlertType) มีดังนี้
        /// info
        /// success
        /// warning
        /// error
        /// black
        /// white
        /// </summary>
        /// <param name="AlertType"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public Models.MyAlert MyAlert(string AlertType, string Message)
        {
            Models.MyAlert alert = new Models.MyAlert();
            alert.Status = AlertType;
            alert.Message = Message.Replace("\r", "").Replace("\n", "").Replace(";", "").Replace("'", "");

            return alert;
        }

        /// <summary>
        /// Check Authorize Menu แบบ 1 เช็คแค่ Controller
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="Json_menu"></param>
        /// <param name="Json_user"></param>
        /// <param name="menu_allow"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool chk_Authorize(string menu, string Json_menu, string Json_user, ref string menu_allow, CONSULS_Context context)
        {
            bool res = true;

            var List_Menu = JsonConvert.DeserializeObject<List<Models.TB_Menu_Authorize>>(Json_menu);
            var List_User = JsonConvert.DeserializeObject<Models.TB_Member>(Json_user);

            int level = List_User.Level_ID;
            var Menu_Authorize = List_Menu.Where(x => x.menu_info.Controller.ToUpper() == menu.ToUpper() && x.Level_ID == level).ToList();

            //1. เช็ค user จาก Session กับ DB
            var user_online = context.TB_Member.Where(x => x.User_ID == List_User.User_ID).ToList();
            if (user_online == null)
            {
                res = false;
                goto Exit;
            }
            else
            {
                if (user_online.Count <= 0)
                {
                    res = false;
                    goto Exit;
                }
            }

            //2. เช็คสิทธิ Menu
            if (Menu_Authorize == null)
            {
                res = false;
                goto Exit;
            }
            else
            {
                if (Menu_Authorize.Count <= 0)
                {
                    res = false;
                    goto Exit;
                }
                else
                {
                    //Return menu_allow กลับ ใช้ตรวจสอบว่าแต่ละเมนูสามารถแก้ไขได้หรือไม่
                    /// W = สามารถแก้ไขได้
                    /// R = ดูได้อย่างเดียว
                    menu_allow = Menu_Authorize[0].Allow.ToString();

                    //ถ้า > 0 แสดงว่ามีสิทธิเข้าใช้งาน
                }
            }

        Exit:;
            return res;
        }
        /// <summary>
        /// /// Check Authorize Menu แบบ 2 เช็ค Controller และ Action
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="action"></param>
        /// <param name="Json_menu"></param>
        /// <param name="Json_user"></param>
        /// <param name="menu_allow"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool chk_Authorize(string menu, string action, string Json_menu, string Json_user, ref string menu_allow, CONSULS_Context context)
        {
            bool res = true;

            var List_Menu = JsonConvert.DeserializeObject<List<Models.TB_Menu_Authorize>>(Json_menu);
            var List_User = JsonConvert.DeserializeObject<Models.TB_Member>(Json_user);

            int level = List_User.Level_ID;
            var Menu_Authorize = List_Menu.Where(x => x.menu_info.Controller == menu && x.menu_info.Action == action && x.Level_ID == level).ToList();

            //1. เช็ค user จาก Session กับ DB
            var user_online = context.TB_Member.Where(x => x.User_ID == List_User.User_ID).ToList();
            if (user_online == null)
            {
                res = false;
                goto Exit;
            }
            else
            {
                if (user_online.Count <= 0)
                {
                    res = false;
                    goto Exit;
                }
            }

            //2. เช็คสิทธิ Menu
            if (Menu_Authorize == null)
            {
                res = false;
                goto Exit;
            }
            else
            {
                if (Menu_Authorize.Count <= 0)
                {
                    res = false;
                    goto Exit;
                }
                else
                {
                    //Return menu_allow กลับ ใช้ตรวจสอบว่าแต่ละเมนูสามารถแก้ไขได้หรือไม่
                    /// W = สามารถแก้ไขได้
                    /// R = ดูได้อย่างเดียว
                    menu_allow = Menu_Authorize[0].Allow.ToString();

                    //ถ้า > 0 แสดงว่ามีสิทธิเข้าใช้งาน
                }
            }

        Exit:;
            return res;
        }

        /// <summary>
        /// ป่องกันกรณี Login ด้วย Admin แล้วเข้าเมนูของ Member ทำให้เรียก Layout ผิด
        /// </summary>
        /// <param name="Json_user"></param>
        /// <returns></returns>
        public bool chk_Level_Layout(string Json_user)
        {
            //True = Admin(Level2)
            //False = Member/Non member
            bool response = false;
            if(Json_user!=null && Json_user != "")
            {
                var List_User = JsonConvert.DeserializeObject<Models.TB_Member>(Json_user);
                if (List_User != null)
                {
                    if(List_User.Level_ID == 2)
                    {
                        response = true;
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Function for Save log
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Controller"></param>
        /// <param name="Function_Name"></param>
        /// <param name="IP_Address"></param>
        /// <param name="Mac_Address"></param>
        /// <param name="Createby"></param>
        /// <param name="Role_Name"></param>
        /// <param name="Level_ID"></param>
        /// <param name="Message"></param>
        public void Save_Log(CONSULS_Context context
            ,string Controller
            ,string Function_Name
            ,string IP_Address
            ,string Mac_Address
            ,string Createby
            ,string Role_Name
            ,int? Level_ID
            ,string Message
            )
        {
            try
            {
                Models.TB_Log tB_Log = new Models.TB_Log();
                tB_Log.Controller = Controller;
                tB_Log.Function_Name = Function_Name;
                tB_Log.IP_Address = IP_Address;
                tB_Log.Mac_Address = Mac_Address;
                tB_Log.Createby = Createby;
                tB_Log.Role_Name = Role_Name;
                tB_Log.Level_ID = Level_ID;
                tB_Log.Message = Message;
                tB_Log.CreateDate = System.DateTime.Now;
                context.Add(tB_Log);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                string res = e.Message.ToString();
            }
        }

        public string get_mac_address()
        {
            string res = "";
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            for (int j = 0; j <= 1; j++)
            {
                PhysicalAddress address = nics[j].GetPhysicalAddress();
                byte[] bytes = address.GetAddressBytes();
                for (int i = 0; i < bytes.Length; i++)
                {
                    res = res + bytes[i].ToString("X2");
                    if (i != bytes.Length - 1)
                    {
                        res = res + ("-");
                    }
                }
            }

            return res;
        }

        //GET IPADDRESS CLIENT
        //**********************************
        public string GetIPAddress()
        {
            //string xxx = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString();
            string IPAddress = "";

            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = Environment.MachineName;

            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
    }
}
