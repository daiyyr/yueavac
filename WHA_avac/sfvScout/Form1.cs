using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;

namespace WHA_avac
{
    public partial class Form1 : Form
    {

        bool debug = true;

        string reg = "";
        Match myMatch;
 
        string gViewstate = "";
        string gEventvalidation = "";
        string gVerificationCode = "";
        int gTicket = 1;

        CookieCollection gCookieContainer = null;



        string gVACity_raw = "beijing",     //  30 for guangzhou;29 for shanghai;28 for beijing
               gVACity = "",
               gCategory = "from combobox",          //13 for general, 17 for work and holiday
               gTitle = "MR.",
               gContactNumber = "05923333433",
               gEmail = "3fe3333@qq.com",
               gFirstName = "jinping",
               gLastName = "xi",
               gMobile = "139034000",
               gPassport = "E7743779483",
               gSTDCode = "0533";
        List<String> gDays = new List<string>(); //5721 means 2015.08.31, the number of days since 2000.01.01
                                                 //默认选择最晚日期

        string gTime = "02";    //02~24, 15分钟一个时段，某个时段满了则后面的时段号均-1
                                //例如
                                /**
                                 *  0730 02
                                    0800 03
                                    0845 04
                                    0915 05
                                    0930 06
                                    0945 07
                                    1030 08
                                    1045 09
                                    1100 10
                                    1115 11
                                    1130 12
                                    1145 13
                                    1200 14
                                    1215 15
                                    1230 16
                                    1245 17
                                    1300 18
                                    1330 19
                                    1345 20
                                    1400 21
                                 **/


 //       Thread gAlarm = null;
 //       string gnrnodeGUID = "";
 //       string gViewStateGenerator = "";
  //      string user = "dudeea";
  //      string password = "Dd123456";


        public delegate void setLog(string str1);
        public void setLogT(string s)
        {
            if (logT.InvokeRequired)
            {
                // 实例一个委托，匿名方法，
                setLog sl = new setLog(delegate(string text)
                {
                    logT.AppendText(DateTime.Now.ToString() + " " + text + Environment.NewLine);
                });
                // 把调用权交给创建控件的线程，带上参数
                logT.Invoke(sl, s);
            }
            else
            {
                logT.AppendText(DateTime.Now.ToString() + " " + s + Environment.NewLine);
            }
        }

        public void setLogtRed(string s)
        {
            if (logT.InvokeRequired)
            {
                setLog sl = new setLog(delegate(string text)
                {
                    logT.AppendText(DateTime.Now.ToString() + " " + text + Environment.NewLine);
                    int i = logT.Text.LastIndexOf("\n", logT.Text.Length - 2);
                    if (i > 1)
                    {
                        logT.Select(i, logT.Text.Length);
                        logT.SelectionColor = Color.Red;
                        logT.Select(i, logT.Text.Length);
                        logT.SelectionFont = new Font(Font, FontStyle.Bold);
                    }
                });
                logT.Invoke(sl, s);
            }
            else
            {
                logT.AppendText(DateTime.Now.ToString() + " " + s + Environment.NewLine);
                int i = logT.Text.LastIndexOf("\n", logT.Text.Length - 2);
                if (i > 1)
                {
                    logT.Select(i, logT.Text.Length);
                    logT.SelectionColor = Color.Red;
                    logT.Select(i, logT.Text.Length);
                    logT.SelectionFont = new Font(Font, FontStyle.Bold);
                }
            }
        }

        public delegate void DSetTestLog(HttpWebRequest req, string respHtml);
        public void setTestLog(HttpWebRequest req, string respHtml)
        {
            if (testLog.InvokeRequired)
            {
                DSetTestLog sl = new DSetTestLog(delegate(HttpWebRequest req1, string text)
                {
                    testLog.Text = Environment.NewLine + "返回的HTML源码：";
                    testLog.Text += Environment.NewLine + text;
                });
                testLog.Invoke(sl, req, respHtml);
            }
            else
            {
                testLog.Text = Environment.NewLine + "返回的HTML源码：";
                testLog.Text += Environment.NewLine + respHtml;
            }
        }

        public Form1()
        {
            InitializeComponent();

            if (debug)
            {
                comboBox1.SelectedIndex = 1; //normal
                button2.Visible = true;
                testLog.Visible = true;
            }
            else {
                comboBox1.SelectedIndex = 0; //work and holiday 
            }
            

            if (gVACity_raw.Equals("guangzhou"))
            {
                gVACity = "30";
            }
            else if (gVACity_raw.Equals("shanghai"))
            {
                gVACity = "29";
            }else{
                gVACity = "28";
            }
            gEmail = gEmail.Replace("@","%40");
            if (File.Exists(System.Environment.CurrentDirectory + "\\" + "urlList"))
            {
                string[] lines = File.ReadAllLines(System.Environment.CurrentDirectory + "\\" + "urlList");
                foreach (string line in lines)
                {
                    urlList.Items.Add(line);
                }
            }
        }
        /*
        public void alarm()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(WHA_avac.Properties.Resources.mtl);
            player.Load();
            player.PlayLooping();
        }
         */
        public static string ToUrlEncode(string strCode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(strCode); //默认是System.Text.Encoding.Default.GetBytes(str)  
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
            for (int i = 0; i < byStr.Length; i++)
            {
                string strBy = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strBy))
                {
                    //是字母或者数字则不进行转换    
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16));
                }
            }
            return (sb.ToString());
        }

        public void writeFile(string file, string content)
        {
            FileStream aFile = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile);
            sw.Write(content);
            sw.Close();
        }
        
        public int downloadHtml(string html)
        {
            string fileName = System.Environment.CurrentDirectory + "\\" +
                gLastName + gFirstName + System.DateTime.Now.ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
            string fileFullName = fileName + ".html";
            int i = 2;
            while (true)
            {
                if (File.Exists(fileFullName))
                {
                    fileFullName = fileName + "_" + i.ToString();
                    i++;
                    continue;
                }
                break;
            }
            
            writeFile(fileFullName, html);
            return 1;
        }
        /*
        public int HtmlHandler(HttpWebResponse resp)
        {            
            string url = resp.ResponseUri.ToString();
            string html = resp2html(resp);
            if (html.Equals(""))
            {
                return -1;
            }
            string validHtml = "";
            string lastSection = "";
            bool have_APM_DO_NOT_TOUCH = false;
            string P = @"(?<=\/)[^\/]+?(?=$|\/$|\?)";
            Match found = (new Regex(P)).Match(url);
            if (found.Success)
            {
                lastSection = found.Groups[0].Value;
            }
            if (html.Contains("APM_DO_NOT_TOUCH"))//得到的是带JS乱码的页
            {
                have_APM_DO_NOT_TOUCH = true;
                P = @"(?<=</APM_DO_NOT_TOUCH>)[\s\S]+(?=$)";
                found = (new Regex(P)).Match(html);
                if (found.Success)
                {
                    validHtml = found.Groups[0].Value;
                }
            }
            else
            {
                validHtml = html;
            }
            validHtml = Regex.Replace(validHtml, @"<div id=""dateTime"">.+?<\/div>", "");
            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory);
            FileInfo[] allFile = dir.GetFiles();
            bool isNewContent = true;
            bool isNewURL = true;
            foreach (FileInfo fi in allFile)
            {                
                if (!fi.Name.Contains(lastSection))
                {
                    continue;
                }
                else
                {
                    string fileContent = System.IO.File.ReadAllText(fi.FullName);
                    string urlInFile = "";
                    P = @"(?<=URL:).+?(?=\r\n)";
                    found = (new Regex(P)).Match(fileContent);
                    if (found.Success)
                    {
                        urlInFile = found.Groups[0].Value;
                    }
                    if (!urlInFile.Equals(url))
                    {
                        continue;
                    }
                    else//找到url相同的文件
                    {
                        isNewURL = false;
                        string validHtmlInFile = "";
                        if (have_APM_DO_NOT_TOUCH)
                        {
                            P = @"(?<=</APM_DO_NOT_TOUCH>)[\s\S]+(?=$)";
                            found = (new Regex(P)).Match(fileContent);
                            if (found.Success)
                            {
                                validHtmlInFile = found.Groups[0].Value;
                            }
                        }
                        else
                        {
                            P = @"(?<=\r\nHTML:\r\n)[\s\S]+(?=$)";
                            found = (new Regex(P)).Match(fileContent);
                            if (found.Success)
                            {
                                validHtmlInFile = found.Groups[0].Value;
                            }
                        }
                        validHtmlInFile = Regex.Replace(validHtmlInFile, @"<div id=""dateTime"">.+?<\/div>", "");
                        if (validHtmlInFile.Equals(validHtml))//有效内容也相同，则认为页面无变更
                        {
                            isNewContent = false;
                            break;
                        }
                        else //有效内容不同，有可能与早期文件内容不同，与新文件相同，继续遍历
                        {
                            continue;
                        }
                    }
                }
            }
            if (isNewURL)//认为是新增的地址，进行第一次下载
            {
                downloadHtml(url, html);
                setLogT("new url: " + url );
                setLogT("page saved successfully");
                return 1;
            }
            if (isNewContent)//旧URL，且与所有文件内容均不同，下载文件，拉响警报！
            {
                downloadHtml(url, html);
                if (gAlarm != null)
                {
                    //gAlarm.Abort();                   
                }
                else
                {
                    Thread t = new Thread(alarm);
                    t.Start();
                    gAlarm = t;
                }
                setLogtRed("Attention! Page modified on " + url);
                return 2;
            }
            else
            {
                setLogT(url + " is unchanged");
                return 3;
            }
        }
        */
        public void setRequest(HttpWebRequest req)
        {
            req.AllowAutoRedirect = false;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //req.Accept = "*/*";
            //req.Connection = "keep-alive";
            req.KeepAlive = true;
            req.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.10; rv:40.0) Gecko/20100101 Firefox/40.0";
            //req.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:37.0) Gecko/20100101 Firefox/37.0";
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E";
            req.Headers["Accept-Encoding"] = "gzip, deflate";
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            req.Host = "www.visaservices.org.in";

            req.CookieContainer = new CookieContainer();
            req.CookieContainer.PerDomainCapacity = 40;
            if (gCookieContainer != null)
            {
                req.CookieContainer.Add(gCookieContainer);
            }
            req.ContentType = "application/x-www-form-urlencoded";
        }

        public int writePostData(HttpWebRequest req, string data)
        {
            byte[] postBytes = Encoding.UTF8.GetBytes(data);
            req.ContentLength = postBytes.Length;
            Stream postDataStream = null;
            try
            {
                postDataStream = req.GetRequestStream();
                postDataStream.Write(postBytes, 0, postBytes.Length);
                
            }
            catch (WebException webEx)
            {
                setLogT("GetRequestStream," + webEx.Status.ToString());
                return -1;
            }
            
            postDataStream.Close();
            return 1;
        }

        public string resp2html(HttpWebResponse resp)
        {
            string respHtml = "";
            char[] cbuffer = new char[256];
            Stream respStream = resp.GetResponseStream();
            StreamReader respStreamReader = new StreamReader(respStream);//respStream,Encoding.UTF8
            int byteRead = 0;
            try
            {
                byteRead = respStreamReader.Read(cbuffer, 0, 256);

            }
            catch (WebException webEx)
            {
                setLogT("respStreamReader, " + webEx.Status.ToString());
                return "";
            }
            while (byteRead != 0)
            {
                string strResp = new string(cbuffer, 0, byteRead);
                respHtml = respHtml + strResp;
                try
                {
                    byteRead = respStreamReader.Read(cbuffer, 0, 256);
                }
                catch (WebException webEx)
                {
                    setLogT("respStreamReader, " + webEx.Status.ToString());
                    return "";
                }
                
            }
            respStreamReader.Close();
            respStream.Close();
            return respHtml;
        }


        /* 
         * return success or not
         */
        public int weLoveMuYue(string url, string method, string referer, bool allowAutoRedirect, string postData)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
            
                if (method.Equals("POST"))
                {
                    if (writePostData(req, postData) < 0)
                    {
                        continue;
                    }
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    setLogT("respStreamReader, " + webEx.Status.ToString());
                    continue;
                }
                if (resp != null)
                {
                    respHtml = resp2html(resp);
                    if (respHtml.Equals(""))
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
                setTestLog(req, respHtml);
                gCookieContainer = req.CookieContainer.GetCookies(req.RequestUri);
                resp.Close();
                break;
            }
            return 1;
        }

        /* 
         * return response HTML
         */
        public string weLoveYue(string url, string method, string referer, bool allowAutoRedirect, string postData)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = null;
                setRequest(req);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
            
                if (method.Equals("POST"))
                {
                    if (writePostData(req, postData) < 0)
                    {
                        continue;
                    } 
                }
                string respHtml = "";
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    setLogT("respStreamReader, " + webEx.Status.ToString());
                    continue;
                }
                if (resp != null)
                {
                    respHtml = resp2html(resp);
                    if (respHtml.Equals(""))
                    {
                        continue;
                    }
                    gCookieContainer = req.CookieContainer.GetCookies(req.RequestUri);
                    if (debug)
                    {
                        setTestLog(req, respHtml);
                    }
                    resp.Close();
                    return respHtml;
                }
                else
                {
                    continue;
                }
            }
        }

        /*
         * do not handle the response
         */
        public HttpWebResponse weLoveYueer(HttpWebResponse resp, string url, string method, string referer, bool allowAutoRedirect, string postData)
        {
            while (true)
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                setRequest(req);
                req.Method = method;
                req.Referer = referer;
                if (allowAutoRedirect)
                {
                    req.AllowAutoRedirect = true;
                }
            
                if (method.Equals("POST"))
                {
                    if (writePostData(req, postData) < 0)
                    {
                        continue;
                    } 
                }
                try
                {
                    resp = (HttpWebResponse)req.GetResponse();
                }
                catch (WebException webEx)
                {
                    setLogT("respStreamReader, " + webEx.Status.ToString());
                    continue;
                }
                if (resp != null)
                {
                    gCookieContainer = req.CookieContainer.GetCookies(req.RequestUri);
                    return resp;
                }
                else
                {
                    continue;
                }
            }
        }



        public int create() 
        {
            string respHtml;

            
            setLogT("get first page..");

            respHtml = weLoveYue(
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                "GET",
                "",
                true,
                "");

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation = myMatch.Groups[0].Value;
            }
            gEventvalidation = ToUrlEncode(gEventvalidation);

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate = myMatch.Groups[0].Value;

            }
            gViewstate = ToUrlEncode(gViewstate);
            


            setLogT("make a pointment..");
            respHtml = weLoveYue(
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                "POST",
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                false,
                "__EVENTTARGET=ctl00%24plhMain%24lnkSchApp&__EVENTARGUMENT=&__VIEWSTATE="+ gViewstate
                +"&____Ticket="+gTicket.ToString()
                +"&__EVENTVALIDATION="+gEventvalidation
                );
            gTicket++;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation = myMatch.Groups[0].Value;
            }
            gEventvalidation = ToUrlEncode(gEventvalidation);

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate = myMatch.Groups[0].Value;

            }
            gViewstate = ToUrlEncode(gViewstate);

            return 1;
        }


        public int selectLocation()
        {
            setLogT("selecting Location: " + gVACity_raw);

            string respHtml = weLoveYue(
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppScheduling.aspx",
                "POST",
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppWelcome.aspx?p=Gta39GFZnstZVCxNVy83zTlkvzrXE95fkjmft28XjNg%3d",
                false,
                "__VIEWSTATE=U8AJVYNixXRfz4bH8v8%2F0vyB2azTOxRhlu62TVP4Amy7PraT6FvK3uGzIJqpRnwHPLQBDjit0Tjqobj9c3TrNCXUsyOncX0WxstNd60kTj8"
                + "%2Bd2aNdNAHhWwFQbihaPgQt5lqYnaTge7vlpLbWpGs1joqc1zDofYD9mVpEFI%2FO2z%2Bek3MI8aSix%2FDSg5erl%2B8uRJ1JwBoHBwR2so02sjNNZGjkrCqF8m6WqbVdzjMAnEEhrSuy7sSn"
                + "%2Fpfy54zWWFpQpBwD1OXAtltLg1C%2FT5KV5tpWKQHxmuq4JXjIQ4EPdT%2BSZFl9taV2DiZDT3X0kkl%2FyxDYRbAo0OU88hhWUeJf"
                + "%2BMdRSC7C6y3pzBtisn2c10P9Dk6t%2FZxewskMJAIsnN5a7cAQr3%2BVEWgDVMdZBow0Ylr7q6CFokZaUVebCMLBTFOnvnI9Zjbxg"
                + "%3D%3D&ctl00%24plhMain%24cbo"
                + "VAC=" + gVACity
                + "&ctl00%24plhMain%24btnSubmit=%E6%8F%90%E4%BA%A4&ctl00%24plhMain%24hdnValidation1"
                + "=%E8%AF%B7%E9%80%89%E6%8B%A9%EF%BC%9A&ctl00%24plhMain%24hdnValidation2=%E7%AD%BE%E8%AF%81%E7%94%B3%E8"
                + "%AF%B7%E4%B8%AD%E5%BF%83&ctl00%24plhMain%24hdnValidation3=%E5%B1%85%E4%BD%8F%E5%9B%BD&____Ticket="+gTicket.ToString()
                +"&__EVENTVALIDATION"
                + "=Vl39F6qwZOICJpTa9PcH0gV%2FyeRGOfg5uQqROs1LRKtDZsxgCLg9LzkK%2F0bKajvKLYu88iUHiiiQQjV%2FynffMgplscVinn0GMf5vgACt66c"
                + "%3D"
                );
            gTicket++;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation = myMatch.Groups[0].Value;
            }
            gEventvalidation = ToUrlEncode(gEventvalidation);

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate = myMatch.Groups[0].Value;

            }
            gViewstate = ToUrlEncode(gViewstate);

            return 1;
        }

        public int selectVisaType()
        {
            string respHtml;
            /*
            
            setLogT("select the visa type..");
            //neccessary?
            respHtml = weLoveYue(
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingGetInfo.aspx",
                "POST",
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingGetInfo.aspx",
                false,
                "__EVENTTARGET=ctl00%24plhMain%24cboVisaCategory&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE=" + gViewstate
                + "&ctl00%24plhMain%24tbxNumOfApplicants=1&ctl00%24plhMain%24cboVisaCategory=" + gCategory //13 for general, 17 for working and holiday
                + "&ctl00%24plhMain%24hdnValidation1=%E8%AF%B7%E8%BE%93%E5%85%A5%EF%BC%9A&ctl00%24plhMain%24hdnValidation2=%E6%9C%89%E6%95%88%E4%BA%BA%E6"
                +"%95%B0%E3%80%82&ctl00%24plhMain%24hdnValidation3=%E6%8A%A5%E5%90%8D%E4%BA%BA%E6%95%B0%E5%BF%85%E9%A1"
                +"%BB%E4%BB%8B%E4%BA%8E1%E5%92%8C++&ctl00%24plhMain%24hdnValidation4=%E7%AD%BE%E8%AF%81%E7%B1%BB%E5%88%AB"
                + "&____Ticket="+gTicket.ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation
                );
            gTicket++;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation = myMatch.Groups[0].Value;

            }
            gEventvalidation = ToUrlEncode(gEventvalidation);

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate = myMatch.Groups[0].Value;

            }
            gViewstate = ToUrlEncode(gViewstate);

            */

            delegate2 s0 = new delegate2(delegate()
            {
                setLogT("submitting the visa type: " + comboBox1.SelectedItem.ToString());
            });
            textBox1.Invoke(s0);
            respHtml = weLoveYue(
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingGetInfo.aspx",
                "POST",
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingGetInfo.aspx",
                false,
                "__EVENTTARGET=&__EVENTARGUMENT=&__LASTFOCUS=&__VIEWSTATE="+gViewstate
                +"&ctl00%24plhMain%24tbxNumOfApplicants"
                +"=1&ctl00%24plhMain%24cboVisaCategory=" + gCategory //13 for general, 17 for working and holiday
                +"&ctl00%24plhMain%24btnSubmit=%E6%8F%90%E4%BA%A4&ctl00%24plhMain"
                +"%24hdnValidation1=%E8%AF%B7%E8%BE%93%E5%85%A5%EF%BC%9A&ctl00%24plhMain%24hdnValidation2=%E6%9C%89%E6"
                +"%95%88%E4%BA%BA%E6%95%B0%E3%80%82&ctl00%24plhMain%24hdnValidation3=%E6%8A%A5%E5%90%8D%E4%BA%BA%E6%95"
                +"%B0%E5%BF%85%E9%A1%BB%E4%BB%8B%E4%BA%8E1%E5%92%8C++&ctl00%24plhMain%24hdnValidation4=%E7%AD%BE%E8%AF"
                +"%81%E7%B1%BB%E5%88%AB&____Ticket="+gTicket.ToString()
                +"&__EVENTVALIDATION=" + gEventvalidation
                );
            gTicket++;

            if (respHtml.Contains("No date(s) available for appointment.") || respHtml.Contains("无日期（S）委任。"))
            {
                setLogT("该类别名额已满，不要灰心，备战下一次");
                return -9;
            }

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation = myMatch.Groups[0].Value;
                
            }
            gEventvalidation = ToUrlEncode(gEventvalidation);

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate = myMatch.Groups[0].Value;
                
            }
            gViewstate = ToUrlEncode(gViewstate);

            string cCodeGuid = "";
            reg = @"(?<=MyCaptchaImage.aspx\?guid=).*?(?="" border=)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                cCodeGuid = myMatch.Groups[0].Value;
            }

            if (textBox1.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    pictureBox1.ImageLocation = @"https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/MyCaptchaImage.aspx?guid=" + cCodeGuid;
                    textBox1.Text = "";
                    textBox1.ReadOnly = false;
                    textBox1.Focus();
                    label6.Visible = true;
                });
                textBox1.Invoke(sl);
            }
            else
            {
                pictureBox1.ImageLocation = @"https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/MyCaptchaImage.aspx?guid=" + cCodeGuid;
                textBox1.Text = "";
                textBox1.ReadOnly = false;
                textBox1.Focus();
                label6.Visible = true;
            }

            return 1;
        }


        public int fillInDetails()
        {
            setLogT("filling in details..");

            string respHtml = weLoveYue(
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingVisaCategory.aspx",
                "POST",
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingGetInfo.aspx",
                false,
                "__VIEWSTATE=" + gViewstate
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxPassportNo="
                + gPassport
                + "&ctl00%24plhMain%24repAppVisaDetails"
                + "%24ctl01%24cboTitle="
                + gTitle
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxFName="
                + gFirstName
                + "&ctl00%24plhMain"
                + "%24repAppVisaDetails%24ctl01%24tbxLName="
                + gLastName
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxSTDCode="
                + gSTDCode
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxContactNumber="
                + gContactNumber
                + "&ctl00%24plhMain%24repAppVisaDetails"
                + "%24ctl01%24tbxMobileNumber="
                + gMobile
                + "&ctl00%24plhMain%24repAppVisaDetails%24ctl01%24tbxEmailAddress="
                + gEmail
                + "&ctl00%24plhMain%24btnSubmit=%E6%8F%90%E4%BA%A4&ctl00%24plhMain%24hdnValidation1=%E8%AF%B7%E8"
                + "%BE%93%E5%85%A5%E7%94%B3%E8%AF%B7%E4%BA%BA%E7%9A%84%E6%8A%A4%E7%85%A7%E5%8F%B7%E7%A0%81%E3%80%82&ctl00"
                + "%24plhMain%24hdnValidation2=%E8%AF%B7%E7%94%B3%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E9%80%89%E6%8B%A9"
                + "%E6%A0%87%E9%A2%98%E3%80%82&ctl00%24plhMain%24hdnValidation3=%E8%AF%B7%E8%BE%93%E5%85%A5%E7%BB%99%E5"
                + "%AE%9A%E5%90%8D%E7%A7%B0%E7%9A%84%E7%94%B3%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E3%80%82&ctl00%24plhMain"
                + "%24hdnValidation4=%E8%AF%B7%E8%BE%93%E5%85%A5%E5%A7%93%E7%94%B3%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E3"
                + "%80%82&ctl00%24plhMain%24hdnValidation5=%E8%AF%B7%E8%BE%93%E5%85%A5%E6%89%8B%E6%9C%BA%E5%8F%B7%E7%A0"
                + "%81%E3%80%82%E7%94%B3%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E3%80%82&ctl00%24plhMain%24hdnValidation6="
                + "%E8%AF%B7%E8%BE%93%E5%85%A5%E6%9C%89%E6%95%88%E7%9A%84%E7%9A%84STD%E4%BB%A3%E7%A0%81%E4%B8%BA%E7%94%B3"
                + "%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E3%80%82&ctl00%24plhMain%24hdnValidation7=%E8%AF%B7%E8%BE%93%E5"
                + "%85%A5%E6%9C%89%E6%95%88%E7%9A%84%E7%94%B5%E5%AD%90%E9%82%AE%E4%BB%B6%E5%9C%B0%E5%9D%80%EF%BC%8C%E7%94"
                + "%B3%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E3%80%82&ctl00%24plhMain%24hdnValidation8=%E8%AF%B7%E5%AF%B9"
                + "%E7%94%B3%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E8%BE%93%E5%85%A5%E6%9C%89%E6%95%88%E7%9A%84GWFNo%E7%9A"
                + "%84%E3%80%82&ctl00%24plhMain%24hdnValidation9=%E8%AF%B7%E9%80%89%E6%8B%A9%E7%AD%BE%E8%AF%81%E7%B1%BB"
                + "%E5%88%AB%E7%9A%84%E7%94%B3%E8%AF%B7%E4%BA%BA%E6%B2%A1%E6%9C%89%E3%80%82&____Ticket="+gTicket.ToString()
                + "&__EVENTVALIDATION=" + gEventvalidation
                + "&ctl00%24plhMain%24mycaptchacontrol1=" + gVerificationCode
                );
            gTicket++;

            reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gEventvalidation = myMatch.Groups[0].Value;
            }
            gEventvalidation = ToUrlEncode(gEventvalidation);

            reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                gViewstate = myMatch.Groups[0].Value;

            }
            gViewstate = ToUrlEncode(gViewstate);

            if (respHtml.Contains("correct CAPTCHA alphabets"))
            {
                setLogT("验证码错误！请重新输入");
                string cCodeGuid = "";
                reg = @"(?<=MyCaptchaImage.aspx\?guid=).*?(?="" border=)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    cCodeGuid = myMatch.Groups[0].Value;
                }
                pictureBox1.ImageLocation = @"https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/MyCaptchaImage.aspx?guid=" + cCodeGuid;
            
                return -2;
            }

            if (respHtml.Contains("申请人已预约")||respHtml.Contains("Marked applicants already have an appointment.") )
            {
                setLogT("申请人已预约: " + gLastName +" "+ gFirstName +", 护照: " + gPassport);
                return -3;
            }

            if (respHtml.Contains("Applicant Details") && respHtml.Contains("Please enter"))
            {
                setLogT("incompleted personal details");
                return -1;
            }

            reg = @"(?<=Appointment',')\d+?(?='\)"" style=""color:Black"" title="")";
            myMatch = (new Regex(reg)).Match(respHtml);
            while (myMatch.Success)
            {
                gDays.Add(myMatch.Groups[0].Value);
                myMatch = myMatch.NextMatch();
            }
            if (gDays.Count==0)
            {
                setLogT(gVACity_raw + ", " + (gCategory.Equals("17") ? "working and holiday, " : "general") + ", 名额已满, 请尝试其它预约地点");
                return -1;
            }



            //we do not need to calculate the TimeSpan, it's on the page
            /*
            string dateDay="";
            string dateMonth="";
            int dateYear = 2015;
            
            //find the month
            reg = @"(?<=style=""color:Black"" title="").*?(?= \d+"")";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                dateMonth = myMatch.Groups[0].Value;
            }
            else
            {
                setLogT("Error.. there is no available month");
                return -1;
            }

            //find a latest day
            for (int i = 31; i > 0; i--)
            {
                reg = i.ToString()+@""">"+i.ToString()+@"</a></td><td align";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    dateDay = i.ToString();
                    break;
                }
            }
            if (dateDay.Equals(""))
            {
                setLogT(gVACity_raw + ", " + (gCategory.Equals("17")?"working and holiday, ":"general") + ", 名额已满!");
                return -1;
            }

            //find the year
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyyMMMMd";
            while (true)
            {
                DateTime dt = Convert.ToDateTime(dateYear.ToString() + dateMonth + dateDay, dtFormat);
                if ((dt - DateTime.Now).Days > 0)
                {
                    gDays[1] = (dt - Convert.ToDateTime("2000-01-01 00:00:00")).Days.ToString();
                    break;
                }
                dateYear++;
            }
             */
 
            return 1;
        }

        public int pickDate()
        {
            while(true)
            {
                setLogT("pickDate..");

                if (gDays.Count == 0)
                {
                    setLogT(gVACity_raw + ", " + (gCategory.Equals("17") ? "working and holiday, " : "general") + ", 名额已满, 请尝试其它预约地点");
                    return -1;
                }

                string respHtml = weLoveYue(
                    "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingInterviewDate.aspx",
                    "POST",
                    "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingVisaCategory.aspx",
                    false,
                    "__EVENTTARGET=ctl00%24plhMain%24cldAppointment&__EVENTARGUMENT="
                    + gDays.Last()
                    + "&__VIEWSTATE=" + gViewstate
                    + "&____Ticket=" + gTicket.ToString()
                    + "&__EVENTVALIDATION=" + gEventvalidation
                    );
                gTicket++;

                reg = @"style=""color:Black"" title="".*?"">\d+</a></td><td align=""center""";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    setLogT("所选日期异常，选择前一个可用天");
                    gDays.RemoveAt(gDays.Count - 1);
                    continue;
                }


                reg = @"(?<=name=""__EVENTVALIDATION"" id=""__EVENTVALIDATION"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gEventvalidation = myMatch.Groups[0].Value;

                }
                gEventvalidation = ToUrlEncode(gEventvalidation);

                reg = @"(?<=id=""__VIEWSTATE"" value="").*?(?="" />)";
                myMatch = (new Regex(reg)).Match(respHtml);
                if (myMatch.Success)
                {
                    gViewstate = myMatch.Groups[0].Value;

                }
                gViewstate = ToUrlEncode(gViewstate);

                return 1;
            }
        }

        public int pickTime()
        {
            setLogT("pickTime..");

            string respHtml = weLoveYue(
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingInterviewDate.aspx",
                "POST",
                "https://www.visaservices.org.in/DIAC-China-Appointment/AppScheduling/AppSchedulingInterviewDate.aspx",
                false,
                "__EVENTTARGET=ctl00%24plhMain%24gvSlot%24ctl" + gTime
                +"%24lnkTimeSlot&__EVENTARGUMENT=&__VIEWSTATE=" + gViewstate
            +"&____Ticket="+gTicket.ToString()
            +"&__EVENTVALIDATION="+ gEventvalidation
            );
            gTicket++;

            string lblReference = "";
            reg = @"(?<=<span id=""lblReference"">).*?(?=</span>)";
            myMatch = (new Regex(reg)).Match(respHtml);
            if (myMatch.Success)
            {
                lblReference = myMatch.Groups[0].Value;
                setLogT("预约成功！预约号: "+lblReference+"\n确认信已下载到软件所在位置，请截图或拍照并请牢记预约号。");
            }
            else
            {
                setLogT("所选时间异常, 重新提交前一个可用日期");
                gDays.RemoveAt(gDays.Count - 1);
                pickDate();
                pickTime();
            }

            string result = respHtml
                .Replace("<head id=\"Head1\"><title>", "<head id=\"Head1\"><meta charset=\"UTF-8\"><title>")
                .Replace("../", "https://www.visaservices.org.in/DIAC-China-Appointment/")
                .Replace("Print Appointment Letter", "打印预约信")
                .Replace("AppWelcome.aspx\">Home</a>", "AppWelcome.aspx\">主页</a>")
                .Replace("Australian Visa Application Centre", "澳大利亚签证申请中心")
                .Replace("Guangzhou", "广州")
                .Replace("Shanghai", "上海")
                .Replace("Beijing", "北京")
                .Replace("Confirmation of Appointment", "预约确认")
                .Replace("Please arrive at the Visa Application Centre not earlier than 10 minutes before the given time.<br>", "请在预约时间前10分钟抵达签证申请中心。")//英文网页多一个br
                .Replace("Please remember to carry the following with you :", "请携带：")
                .Replace("1. Your passport", "1. 护照")
                .Replace("2. A completed and signed visa form", "2. 填写并签署的申请表")
                .Replace("3. Printout of the checklist for the visa type being applied for", "3. 打印所申请签证类别适用的审核清单")
                .Replace("4. Supporting documentation listed in the checklist", "4. 审核清单中所列的支持性文件")
                .Replace("5. The fee amount – please check our website for the visa fee details and mode of payment.", "5. 费用-请在我们的网站上查询签证费详情和付款方式。")
                .Replace("Entry into the Visa Application Centre is regulated by a security check. Please carry a print out of this appointment confirmation letter so that the same can be verified from our Daily Appointment List at the information counter.",
                "进入签证申请中心需接受安检。请携带此预约信以便认证您的预约信息。")
                .Replace("Reference Number", "确认号码")
                .Replace("Visa Category", "签证类别")
                .Replace("Appointment Date", "预约日期")
                .Replace("Appointment Time", "预约时间")
                .Replace("Sr. No.", "序列号码")
                .Replace("Full Name", "姓名")
                .Replace("Passport Number", "护照号码")
                .Replace("Address of VAC", "签证申请中心地址")
                .Replace("Unit 02, 29/F, HM Tower, No. 3, Jinsui Road,", "广州市天河区珠江新城金穗路3号")
                .Replace("Zhujiang New Town, Tianhe District, 广州  ", "汇美大厦29楼02单元 邮编：")
                .Replace("Enquiry Number", "问询号码")
                .Replace("2nd Floor, Jiushi Commercial Building,", "中国上海市黄浦区四川中路213号久事商务大厦2层,")
                .Replace("No. 213 Middle Sichuan Rd., Huangpu District, 上海, 200002, China ", "邮编200002. ")
                .Replace("Room C,D,E,F,G,H,I, 21st Floor", "中国北京东城区东直门外大街")
                .Replace("Oriental Kenzo Plaza, No.48 Dongzhimenwai Street Dongcheng District, 北京 , P.R. China 100027", "48号东方银座写字楼21层D-I室 邮编：100027 ");
            
            downloadHtml(result);

            return 1;
        }
        /*
 public void loginT()
 {
     while (true)
     {
         if (rate.Text.Equals(""))
         {
             Thread.Sleep(500);
         }
         else if (Convert.ToInt32(rate.Text) > 0)
         {
             Thread.Sleep(Convert.ToInt32(rate.Text));
         }
         else
         {
             Thread.Sleep(500);
         }

         int r = loginF();
         if (r == -3)
         {
             continue;
         }
         if (r != -2)
         {
             break;
         }
                
     }
 }
 
 public void autoT0()
 {            
     loginT();
     while (true)
     {
         for (int i = 0; i < urlList.Items.Count; i++)
         {
             if (probe(urlList.GetItemText(urlList.Items[i])) == -1)
             {
                 loginT();
             }
             if (rate.Text.Equals(""))
             {
                 Thread.Sleep(500);
             }
             else if (Convert.ToInt32(rate.Text) > 0)
             {
                 Thread.Sleep(Convert.ToInt32(rate.Text));
             }
             else
             {
                 Thread.Sleep(500);
             }                    
         }
     }
 }
 */
        public void autoT() {
            if (textBox1.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    textBox1.ReadOnly = true;
                    label6.Visible = false;
                });
                textBox1.Invoke(sl);
            }
            else
            {
                textBox1.ReadOnly = true;
                label6.Visible = false;
            }

            //create();
            //not neccessary; enable ->Chinese letter; disable -> English letter; daiyyr


            selectLocation();
            if (selectVisaType() == -9)
            {
                return;
            }
            
            if (textBox1.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    label6.Visible = true;
                });
                textBox1.Invoke(sl);
            }
            else
            {
                label6.Visible = true;
            }
        }

        public void autoT2() {
            int fillInResult = fillInDetails();
            if (fillInResult == -2)//incorrect vervification code
            {
                if (textBox1.InvokeRequired)
                {
                    delegate2 sl = new delegate2(delegate()
                    {
                        textBox1.Text = "";
                        textBox1.ReadOnly = false;
                        textBox1.Focus();
                        label6.Visible = true;
                    });
                    textBox1.Invoke(sl);
                }
                else
                {
                    textBox1.Text = "";
                    textBox1.ReadOnly = false;
                    textBox1.Focus();
                    label6.Visible = true;
                }
                return;
            }
            if (fillInResult == -1 || fillInResult == -3)
            {
                return;
            }

            if (pickDate() == -1)
            {
                return;
            }

            pickTime();
            
        }
            

        private void loginB_Click(object sender, EventArgs e)
        {
//            Thread t = new Thread(loginT);
//            t.Start();
        }

        private void autoB_Click(object sender, EventArgs e)
        {
          //  comboBox1.Enabled = false;
          //  rate.Enabled = false;
            Thread t = new Thread(autoT);
            t.Start();
        }

        private void rate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void logT_TextChanged(object sender, EventArgs e)
        {
            logT.SelectionStart = logT.Text.Length;
            logT.ScrollToCaret();
        }

        public delegate void delegate2();


        /*
        public void addURL()
        {
            //string P = @"^http(s)?:\/\/([\w-]+\.)+[\w-]+$";//无法匹配下级页面
            string P = @"^(https?|ftp|file)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]";
            Match M = (new Regex(P)).Match(inputT.Text);
            if (M.Success)
            {
            }else{
                MessageBox.Show("invalid url!");
                return;
            }            
            if (urlList.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    urlList.Items.Add(inputT.Text);
                    inputT.Text = "";
                });
                urlList.Invoke(sl);
            }
            else
            {
                urlList.Items.Add(inputT.Text);
                inputT.Text = "";
            }
            string strCollected = string.Empty;
            for (int i = 0; i < urlList.Items.Count; i++)
            {
                if (strCollected == string.Empty)
                {
                    strCollected = urlList.GetItemText(urlList.Items[i]);
                }
                else
                {
                    strCollected += "\n" + urlList.GetItemText(urlList.Items[i]) ;
                }
            }
            writeFile(System.Environment.CurrentDirectory + "\\" + "urlList", strCollected);
        }

       
        public void deleteURL()
        {
            if (urlList.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    for (int i = urlList.CheckedItems.Count - 1; i >= 0; i--)
                    {
                        urlList.Items.Remove(urlList.CheckedItems[i]);
                    }
                });
                urlList.Invoke(sl);
            }
            else
            {
                for (int i = urlList.CheckedItems.Count - 1; i >= 0; i--)
                {
                    urlList.Items.Remove(urlList.CheckedItems[i]);
                }
            }
            string strCollected = string.Empty;
            for (int i = 0; i < urlList.Items.Count; i++)
            {
                if (strCollected == string.Empty)
                {
                    strCollected = urlList.GetItemText(urlList.Items[i]);
                }
                else
                {
                    strCollected += "\n" + urlList.GetItemText(urlList.Items[i]);
                }
            }
            writeFile(System.Environment.CurrentDirectory + "\\" + "urlList", strCollected);
        }
        */
        private void addB_Click(object sender, EventArgs e)
        {
       //     Thread t = new Thread(addURL);
       //     t.Start();
        }

        private void deleteB_Click(object sender, EventArgs e)
        {
       //     Thread t = new Thread(deleteURL);
       //     t.Start();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 5)
            {
                gVerificationCode = textBox1.Text.Substring(0, 5);
                if (textBox1.InvokeRequired)
                {
                    delegate2 sl = new delegate2(delegate()
                    {
                        textBox1.ReadOnly = true;
                        label6.Visible = false;
                    });
                    textBox1.Invoke(sl);
                }
                else
                {
                    textBox1.ReadOnly = true;
                    label6.Visible = false;
                }
                Thread t = new Thread(autoT2);
                t.Start();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.InvokeRequired)
            {
                delegate2 sl = new delegate2(delegate()
                {
                    textBox1.Text = "";
                    textBox1.ReadOnly = false;
                    textBox1.Focus();
                    label6.Visible = true;
                });
                textBox1.Invoke(sl);
            }
            else
            {
                textBox1.Text = "";
                textBox1.ReadOnly = false;
                textBox1.Focus();
                label6.Visible = true;
            }
        }
        
        //for test
        private void button2_Click(object sender, EventArgs e)
        {
            //test TimeSpan
            DateTime dt1 = Convert.ToDateTime("1996-01-01 00:00:00");
            DateTime dt20000101 = Convert.ToDateTime("1997-01-01 00:00:00");
            setLogT((dt1-dt20000101).Days.ToString());
            int dd = (dt1 - dt20000101).Days;

            //正则表达式有另一条规则，比懒惰／贪婪规则的优先级更高：最先开始的匹配拥有最高的优先权
            reg = @"(?<=a).*?(?=b)";
            myMatch = (new Regex(reg)).Match("aaaaaaaaaaa333b");
            if (myMatch.Success)
            {
                setLogT( myMatch.Groups[0].Value);
            }

            //test string 2 DateTime
            int year = 2015;
            string strDt = year.ToString() + "April25";
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyyMMMMd";
            DateTime dt = Convert.ToDateTime(strDt, dtFormat);
            setLogT(dt.ToString());

            reg = @"(?<=style=""color:Black"" title="").*?(?= \d+"")";
            myMatch = (new Regex(reg)).Match("ent','5750')\" style=\"color:Black\" title=\"September 29\">29</a></td><td align=\"center\" ");
            if (myMatch.Success)
            {
                setLogT( myMatch.Groups[0].Value);
            }

            setLogT(comboBox1.SelectedItem.ToString());

            gDays.Add("1 data");
            gDays.Add("2 data");
            gDays.Add("3 data");
            setLogT(gDays.Last());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gCategory = comboBox1.SelectedIndex == 0 ? "17" : "13";    //13 for general, 17 for work and holiday
        }
    }
}

//选择类别时，是否不需要提交两次: 是，不需要.
//直接post a inavailable date, could succeed?  不可以
//不需要访问第一页？  如果不get首页，最终将返回英文预约页，且英文预约者在中文页重新申请，仍显示名额满；所以可跳过首页的GET和POST , 替换预约页

//清理冗余代码、禁用测试单元
//如果出现两个月的日期，能不翻页直接提交第二个月的最晚时间？
