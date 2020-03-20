using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using HtmlAgilityPack;

namespace GetWebImg
{
    public partial class Form1 : Form
    {

        private string defSavaPath = Path.Combine(Application.StartupPath, "ItemsSave");

        public Form1()
        {
            InitializeComponent();
        }

        public void LogOutput()
        {
            this.TxtLogOutput("========欢迎使用========");
        }


        /// <summary>
        ///  //这个就是我们的函数，我们把要对控件进行的操作放在这里
        ///  日志
        /// </summary>
        private void TxtLogOutput(string logText)
        {
            //判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成 if(!InvokeRequired)
            //if (!this.txt_Log.InvokeRequired)
            //{
            //    this.txt_Log.AppendText($"{t}" + Environment.NewLine);
            //}
            //else
            //{
            //    Invoke(a1, new object[] { t });//执行唤醒操作
            //}

            if (this.txt_Log.InvokeRequired)//不同线程访问了
                SetTxtInvoke(logText);
               // this.txt_Log.Invoke(new Action<TextBox, string>(SetTxtValue), this.txt_Log, logText.ToString());//跨线程了

            else//同线程直接赋值
               // this.txt_Log.Text = logText.ToString();
              SetTxtValue(this.txt_Log, logText.ToString()+ "======同线程直接赋值");


        }


        private void SetTxtInvoke(string logText)
        {
            Thread.Sleep(1000);
            this.txt_Log.Invoke(new Action<TextBox, string>(SetTxtValue), this.txt_Log, logText.ToString()+ "---------------跨线程了");//跨线程了
        }

        private void SetTxtValue(TextBox txt, string value)
        {
            txt.AppendText($"{value}" + Environment.NewLine);
        }

        #region 事件

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Start_Click(object sender, EventArgs e)
        {
            //判断用是否有效
            if (this.Validation())
            {
                //this.TxtLogOutput("开始=====>");
                //this.TxtLogOutput($"数据保存至：{ defSavaPath}");
                this.SetTxtInvoke("开始=====>");
                this.SetTxtInvoke($"数据保存至：{ defSavaPath}");
              
                // this.txt_Log.AppendText($"开始=====>" + Environment.NewLine);
                // this.txt_Log.AppendText($"数据保存至：{defSavaPath}" + Environment.NewLine);


              


                //判断是否启用
                if (this.cBox_Enable.Checked)
                {
                    MessageBox.Show("是否启用二级获取二级链接数据  实现中……");
                }
                else
                {
                    ThreadStart thStart = new ThreadStart(this.LogOutput);//threadStart委托 
                    Thread thread = new Thread(thStart);
                    thread.Priority = ThreadPriority.Highest;
                    thread.IsBackground = true; //关闭窗体继续执行
                    thread.Start();

                    this.GetHtmlImgData(this.txt_Url.Text);
                }
            }
        }

        /// <summary>
        /// 获取HTML页面图片数据
        /// </summary>
        /// <param name="url">页面地址</param>
        private void GetHtmlImgData(string url)
        {
            //获取页面文档
            string htmlDoc = this.GetHtmlDocument(url);
            if (!string.IsNullOrEmpty(htmlDoc))
            {
                //获取图片数据
                string[] imgData = this.AnalysisImgDataByHtmlDoc(htmlDoc);
                if (imgData.Length > 0)
                {
                    //图片保存路径判断
                    string savaImgPath = Path.Combine(defSavaPath, this.GetHtmlTitle(htmlDoc));

                    //判断文件夹是否存在
                    if (!Directory.Exists(savaImgPath))
                        Directory.CreateDirectory(savaImgPath);

                    //保存网页纯文本
                    if (this.cBox_IsSavaText.Checked)
                    {
                        this.SetTxtInvoke("启用保存网页纯文本内容中…");
                        //this.TxtLogOutput("启用保存网页纯文本内容中…");
                        //this.txt_Log.AppendText($"启用保存网页纯文本内容中…" + Environment.NewLine);
                        this.SavaHtmlText(htmlDoc, savaImgPath + @"\网页内容.txt");
                    }

                    if (imgData.Length > 0)
                    {
                        for (int i = 0; i < imgData.Length; i++)
                        {
                            this.SetTxtInvoke($"共计【{imgData.Length}】张图片，第【{i + 1}】张图片，保存==>{ savaImgPath + string.Format($@"\{i + 1}.jpg")}");
                            //this.TxtLogOutput($"共计【{imgData.Length}】张图片，第【{i + 1}】张图片，保存==>{ savaImgPath + string.Format($@"\{i + 1}.jpg")}");
                            // this.txt_Log.AppendText($"共计【{imgData.Length}】张图片，第【{i + 1}】张图片，保存==>{ savaImgPath + string.Format($@"\{i + 1}.jpg")}" + Environment.NewLine);
                            this.DownloadData(imgData[i], savaImgPath + string.Format($@"\{i}.jpg"));
                        }
                    }

                    this.SetTxtInvoke($"程序运行完毕！！！！！！！！！！！");
                    //this.TxtLogOutput($"程序运行完毕！！！！！！！！！！！");
                    //this.txt_Log.AppendText($"程序运行完毕！！！！！！！！！！！" + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearLog_Click(object sender, EventArgs e)
        {
            this.txt_Log.Text = string.Empty;
        }

        /// <summary>
        /// 鼠标离开网址输入框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Url_LostFocus(object sender, EventArgs e)
        {
            //判断用是否有效
            this.Validation();
        }

        /// <summary>
        /// 设置图片保存文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PathSava_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // string foldPath = dialog.SelectedPath;
                defSavaPath = dialog.SelectedPath;

                this.lbl_SavaPath.Text = defSavaPath;

                this.SetTxtInvoke($"文件将会保存至：{defSavaPath}");
                //this.TxtLogOutput($"文件将会保存至：{defSavaPath}");
                //this.txt_Log.AppendText($"文件将会保存至：{defSavaPath}" + Environment.NewLine);
            }
        }

        /// <summary>
        /// 单项选中——其他
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb_Other_CheckedChanged(object sender, EventArgs e)
        {
            //this.cBox_Enable=true;
            if (this.rdb_Other.Checked)
            {
                this.cBox_Enable.Visible = false;
                this.cBox_Enable.Checked = false;
                this.txt_RegularText.Text = string.Empty;

                this.SetTxtInvoke($"切换至{rdb_Other.Text}");
                //this.TxtLogOutput($"切换至{rdb_Other.Text}");
                //this.txt_Log.AppendText($"切换至{rdb_Other.Text}" + Environment.NewLine);
                //this.txt_Log.AppendText($"切换至{this.cBox_Enable.Checked}" + Environment.NewLine);
            }
        }
        /// <summary>
        /// 单项选中——微信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb_Wx_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdb_Wx.Checked)
            {
                this.cBox_Enable.Visible = true;
                this.cBox_Enable.Checked = false;
                this.txt_RegularText.Text = @"<img\b[^<>]*?\bdata-src[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";

                //this.txt_Log.AppendText($"切换至{this.cBox_Enable.Checked}" + Environment.NewLine);
                //this.txt_Log.AppendText($"切换至{rdb_Wx.Text}" + Environment.NewLine);
                //this.TxtLogOutput($"切换至{rdb_Wx.Text}");
                this.SetTxtInvoke($"切换至{rdb_Wx.Text}");
            }
        }

        #endregion

        #region 其他方法

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="imgSrc">网络图片路径 如：https://images.weserv.nl/?url=https://mmbiz.qpic.cn/mmbiz_png/kq3mQ6o8akgSVXXVvEDnzgKZvkXxjDaLpRCLvcxyZn5Pgwt8dpjDkN1M3aufwc7e9ibGLWf2HQeiazIibLE5smlYA/640?wx_fmt=png</param>
        /// <param name="imgSavaPath">图片的保存路径 如：D:\img\1.jpg</param>
        /// <param name="isNext"></param>
        /// <returns></returns>
        private bool DownloadData(string imgSrc, string imgSavaPath)
        {
            Thread.Sleep(2500);
            WebRequest request = WebRequest.Create(imgSrc);
            //this.TxtLogOutput($"创建请求报文");
            this.SetTxtInvoke($"创建请求报文");
            //this.txt_Log.AppendText($"创建请求报文" + Environment.NewLine);
            try
            {
                using (WebResponse response = (WebResponse)request.GetResponse())
                {
                    //this.TxtLogOutput($"图片文件响应回来");
                    this.SetTxtInvoke($"图片文件响应回来");
                    //this.txt_Log.AppendText($"图片文件响应回来" + Environment.NewLine);
                    using (Stream stream = response.GetResponseStream())//原始
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                          //  this.TxtLogOutput($"保存下载图片");
                            this.SetTxtInvoke($"保存下载图片");
                            //this.txt_Log.AppendText($"保存下载图片" + Environment.NewLine);
                            #region 保存下载图片
                            Byte[] buffer = new Byte[response.ContentLength];
                            int offset = 0, actuallyRead = 0;
                            do
                            {
                                actuallyRead = stream.Read(buffer, offset, buffer.Length - offset);
                                offset += actuallyRead;
                            }
                            while (actuallyRead > 0);
                            using (MemoryStream ms = new MemoryStream(buffer))
                            {
                                byte[] buffurPic = ms.ToArray();
                                System.IO.File.WriteAllBytes(imgSavaPath, buffurPic);
                            }
                            this.SetTxtInvoke($"保存下载图片完成=》路径：{imgSavaPath}");
                            //this.TxtLogOutput($"保存下载图片完成=》路径：{imgSavaPath}");
                            //this.txt_Log.AppendText($"保存下载图片完成=》路径：{imgSavaPath}" + Environment.NewLine);
                            #endregion
                        }
                    }

                }
                Thread.Sleep(2500);
            }
            catch (Exception ex)
            {
                //this.SetTxtInvoke($"出现错误：{ex}");
                this.TxtLogOutput($"出现错误：{ex}");
                //this.txt_Log.AppendText($"出现错误：{ex}" + Environment.NewLine);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获取网址的Html文档
        /// </summary>
        /// <param name="targetUrl">目标网址</param>
        /// <returns></returns>
        private string GetHtmlDocument(string targetUrl)
        {
            string retHtmlDoc = string.Empty;

            if (string.IsNullOrEmpty(targetUrl))
            {
                return retHtmlDoc;
            }
            //this.txt_Log.AppendText($"开始获取==>【{targetUrl}】网页文档的数据" + Environment.NewLine);
            this.TxtLogOutput($"开始获取==>【{targetUrl}】网页文档的数据");
            this.SetTxtInvoke($"开始获取==>【{targetUrl}】网页文档的数据");
            try
            {
                //创建代理
                // WebProxy proxy = new WebProxy("113.121.245.231", 6675);    // "107.150.96.188", 8080
                //创建一个请求
                HttpWebRequest httprequst = (HttpWebRequest)WebRequest.Create(targetUrl);
                //代理ip
                // httprequst.Proxy = proxy;
                //不建立持久性链接
                httprequst.KeepAlive = true;
                //设置请求的方法
                httprequst.Method = "GET";
                //设置标头值
                httprequst.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
                httprequst.Accept = "*/*";
                httprequst.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                httprequst.ServicePoint.Expect100Continue = false;
                httprequst.Timeout = 5000;
                httprequst.AllowAutoRedirect = true;//是否允许302
                ServicePointManager.DefaultConnectionLimit = 30;
                //获取响应
                HttpWebResponse webRes = (HttpWebResponse)httprequst.GetResponse();
                //获取响应的文本流

                using (System.IO.Stream stream = webRes.GetResponseStream())
                {
                    using (System.IO.StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8")))
                    {
                        retHtmlDoc = reader.ReadToEnd();
                    }
                }
                //取消请求
                httprequst.Abort();
                //返回数据内容
                return retHtmlDoc;
            }
            catch (Exception ex)
            {
                this.TxtLogOutput($"获取网页文档时出错：{ex}");
                //this.txt_Log.AppendText($"获取网页文档时出错：{ex}" + Environment.NewLine);
                return retHtmlDoc;
            }

        }

        /// <summary>
        ///  分析返回图片数据
        ///  正则出所有图片
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private string[] AnalysisImgDataByHtmlDoc(string htmlDoc)
        {
            string[] retSrcArray;
            try
            {
                if (htmlDoc == null && string.IsNullOrEmpty(htmlDoc))
                {
                    return retSrcArray = new string[0];
                }
                string regularText = this.txt_RegularText.Text;
                if (regularText.Length == 0 && string.IsNullOrEmpty(regularText))
                {
                    this.TxtLogOutput($"正则表达式的值不能为空");
                    //this.txt_Log.AppendText($"正则表达式的值不能为空" + Environment.NewLine);
                    return retSrcArray = new string[0];
                }

                // 定义正则表达式用来匹配 img 标签 
                Regex regImg = new Regex(regularText, RegexOptions.IgnoreCase);

                // 搜索匹配的字符串 
                MatchCollection matches = regImg.Matches(htmlDoc);
                int i = 0;
                retSrcArray = new string[matches.Count];

                // 取得匹配项列表 
                foreach (Match match in matches)
                    retSrcArray[i++] = match.Groups["imgUrl"].Value;

                this.TxtLogOutput($"总共统计出【{retSrcArray.Length}】张图片");
                //this.txt_Log.AppendText($"总共统计出【{retSrcArray.Length}】张图片" + Environment.NewLine);
                return retSrcArray;

            }
            catch (Exception ex)
            {
                this.TxtLogOutput($"正则所有图片时出错：{ex}");
                //this.txt_Log.AppendText($"正则所有图片时出错：{ex}" + Environment.NewLine);
                return retSrcArray = new string[0]; ;
            }

        }


        /// <summary>
        /// 获取网页标题
        /// </summary>
        /// <param name="htmlDoc"></param>
        /// <returns></returns>
        private string GetHtmlTitle(string htmlDoc)
        {
            string retStr = System.DateTime.Now.ToFileTimeUtc().ToString();
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlDoc);

                HtmlNode titleObj = doc.DocumentNode.SelectSingleNode("//title");
                //
                HtmlNode node = doc.DocumentNode.SelectSingleNode(".//*[@id=\"activity-name\"]");
                // "rich_media_title"
                retStr = node.InnerHtml.Trim();

                if (titleObj != null && !string.IsNullOrEmpty(titleObj.InnerText.Trim('\n')))
                {
                    retStr = titleObj.InnerText.Trim('\n');
                }
                else
                {
                    retStr = node == null ? retStr : string.IsNullOrEmpty(node.InnerHtml.Trim()) ? retStr : node.InnerHtml.Trim();
                }


                this.TxtLogOutput($"网页标题{retStr}");
                //this.txt_Log.AppendText($"网页标题{retStr}" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                this.TxtLogOutput($"获取网页标题出错：{ex}");
                //this.txt_Log.AppendText($"获取网页标题出错：{ex}" + Environment.NewLine);
            }

            string reg = @"\:" + @"|\;" + @"|\/" + @"|\\" + @"|\|" + @"|\," + @"|\*" + @"|\?" + @"|\""" + @"|\<" + @"|\>";//特殊字符
            Regex r = new Regex(reg);

            return r.Replace(retStr, "").Replace(".", "");//将特殊字符替换为"";
        }
        /// <summary>
        /// 保存网页上的纯文本文字
        /// </summary>
        /// <param name="htmlDoc">html文档内容</param>
        /// <param name="textSavaPath">文字保存路径 如：E:text\1.txt</param>
        private void SavaHtmlText(string htmlDoc,string textSavaPath)
        {
            try
            {
                string htmlText = string.Empty;
                if (!string.IsNullOrEmpty(textSavaPath))
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(htmlDoc);

                    htmlText = doc.DocumentNode.InnerText;
                    //htmlText = System.Text.RegularExpressions.Regex.Replace(htmlDoc, @"<\/*[^<>]*>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    //htmlText = htmlDoc.Replace("\r\n", "").Replace("\r", "").Replace("&nbsp;", "").Replace(" ", "");

                    if (!System.IO.File.Exists(textSavaPath))
                    {
                        using (System.IO.StreamWriter sw = System.IO.File.CreateText(textSavaPath))
                        {
                            sw.WriteLine(htmlText.Trim());
                            sw.WriteLine();
                            sw.Close();
                        }

                        this.TxtLogOutput($"网页纯文本保存成功");
                        //this.txt_Log.AppendText($"网页纯文本保存成功" + Environment.NewLine);
                    }
                }
                else
                {
                    this.TxtLogOutput($"没有设置网页纯文本保存地址！！");
                    //this.txt_Log.AppendText($"没有设置网页纯文本保存地址！！" + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                this.TxtLogOutput($"没有设置网页纯文本保存地址出错：{ex}！！！！");
                //this.txt_Log.AppendText($"没有设置网页纯文本保存地址出错：{ex}！！" + Environment.NewLine);
            }
        }


        #endregion

        #region 验证输入的完整性
        /// <summary>
        /// 判断网络地址是否有效
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool UrlIsExist(String url)
        {
            bool isExist = false;
            try
            {
                System.Uri u = null;
                try
                {
                    u = new Uri(url);
                }
                catch { return false; }
             
                System.Net.HttpWebRequest r = System.Net.HttpWebRequest.Create(u)
                                        as System.Net.HttpWebRequest;
                r.Method = "HEAD";
                try
                {
                    System.Net.HttpWebResponse s = r.GetResponse() as System.Net.HttpWebResponse;
                    if (s.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        isExist = true;
                    }
                }
                catch (System.Net.WebException x)
                {
                    try
                    {
                        isExist = ((x.Response as System.Net.HttpWebResponse).StatusCode !=
                                     System.Net.HttpStatusCode.NotFound);
                    }
                    catch { isExist = (x.Status == System.Net.WebExceptionStatus.Success); }
                }
            }
            catch (Exception ex)
            {
                this.TxtLogOutput($"判断网络地址是否有效出错：{ex}！！！！");  
            }
            return isExist;

        }

        /// <summary>
        /// 验证是否有效
        /// </summary>
        private bool Validation()
        {
            bool isOk = true;
            try
            {
                //判断用是否输入链接
                if (this.txt_Url.TextLength == 0 && string.IsNullOrEmpty(this.txt_Url.Text))
                {
                    this.TxtLogOutput($"请先输入有效的网址");
                    //this.txt_Log.AppendText($"请先输入有效的网址" + Environment.NewLine);
                    MessageBox.Show("网址不能为空！");
                    return isOk = false;
                }
                //判断链接的有效性
                if (!this.UrlIsExist(this.txt_Url.Text))
                {
                    this.TxtLogOutput($"你输入的网址无法正常访问");
                    //this.txt_Log.AppendText($"你输入的网址无法正常访问" + Environment.NewLine);
                    MessageBox.Show("请先输入有效的网址！");
                    return isOk = false;
                }
                if (string.IsNullOrEmpty(this.txt_RegularText.Text))
                {
                    this.TxtLogOutput($"请输入有效的正则表达式");
                    //this.txt_Log.AppendText($"请输入有效的正则表达式" + Environment.NewLine);
                    MessageBox.Show("请输入有效的正则表达式！");
                    return isOk = false;
                }
            }
            catch (Exception ex)
            {
                this.TxtLogOutput($"验证时出错！{ex}");
                //this.txt_Log.AppendText($"验证时出错！{ex}" + Environment.NewLine);
                return isOk = false;
            }

            return isOk;
        }




        #endregion

    }
}
