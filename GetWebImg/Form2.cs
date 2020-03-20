using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetWebImg
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string defSavaPath = Path.Combine(Application.StartupPath, "ItemsSave");

        private delegate void DelegateWriteMessage(string msgInfo);



        private void btn_Start_Click(object sender, EventArgs e)
        {

            //判断用是否有效
            if (this.Validation())
            {
                this.WriteMessage($"开始=====>{System.DateTime.Now}");
                this.WriteMessage($"数据保存至：{defSavaPath}");


                //WebBrowser webBrowser = new WebBrowser();
                //webBrowser.Navigate(txt_Url.Text.Trim());

                //var retHtmlDoc = webBrowser.Document.Body.InnerHtml; 


                Task task = Task.Run(() =>
                {
                    this.GetHtmlImgData();
                });


                btn_Start.Enabled = task.IsCompleted;
            }




        }


        private void GetHtmlImgData()
        {
            //获取页面文档
            string htmlDoc = this.GetHtmlDocument(this.txt_Url.Text.Trim());
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

                    if (imgData.Length > 0)
                    {
                        for (int i = 0; i < imgData.Length; i++)
                        {
                            this.WriteMessage($"共计【{imgData.Length}】张图片，第【{i + 1}】张图片，保存==>{ savaImgPath + string.Format($@"\{i + 1}.jpg")}");
                            //this.TxtLogOutput($"共计【{imgData.Length}】张图片，第【{i + 1}】张图片，保存==>{ savaImgPath + string.Format($@"\{i + 1}.jpg")}");
                            // this.txt_Log.AppendText($"共计【{imgData.Length}】张图片，第【{i + 1}】张图片，保存==>{ savaImgPath + string.Format($@"\{i + 1}.jpg")}"  );
                            this.DownloadData(imgData[i].Trim(), savaImgPath + string.Format($@"\{i}.jpg"));
                        }
                    }

                    this.WriteMessage($"程序运行完毕！！！！！！！！！！！");
                }
            }
            else
            {
                this.WriteMessage($"网络获取失败，请重启软件，换个网址再试");
            }


            //for (int i = 0; i < 100; i++)
            //{
            //    System.Threading.Thread.Sleep(2000);

            //    WriteMessage($"aaaaa:{i}");
            //}

        }

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

            this.WriteMessage($"创建请求报文");
            try
            {
                using (WebResponse response = (WebResponse)request.GetResponse())
                {
                    this.WriteMessage($"图片文件响应回来");

                    using (Stream stream = response.GetResponseStream())//原始
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            this.WriteMessage($"保存下载图片");

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
                            this.WriteMessage($"保存下载图片完成=》路径：{imgSavaPath}");
                            #endregion
                        }
                    }

                }
                Thread.Sleep(2500);
            }
            catch (Exception ex)
            {
                this.WriteMessage($"出现错误：再次尝试…………");
                try
                {
                    WebRequest request1 = WebRequest.Create("https://images.weserv.nl/?url=" + imgSrc);

                    this.WriteMessage($"再次创建请求报文");

                    using (WebResponse response = (WebResponse)request1.GetResponse())
                    {
                        this.WriteMessage($"图片文件响应回来");

                        using (Stream stream = response.GetResponseStream())//原始
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                this.WriteMessage($"保存下载图片");

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
                                this.WriteMessage($"保存下载图片完成=》路径：{imgSavaPath}");
                                #endregion
                            }
                        }

                    }
                    Thread.Sleep(2500);
                   // return true;
                }
                catch (Exception e)
                {
                    this.WriteMessage($"出现错误：再次尝试【失败】…………跳过当前");
                    return false;
                }
                 
            }
            return true;
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


                this.WriteMessage($"网页标题{retStr}"  );
            }
            catch (Exception ex)
            {
                this.WriteMessage($"获取网页标题出错：{ex}"  );
            }

            string reg = @"\:"+ @"|\;"+ @"|\/"+ @"|\\"+ @"|\|"+ @"|\,"+ @"|\*"+ @"|\?"+ @"|\"""+ @"|\<"+ @"|\>";//特殊字符
            Regex r = new Regex(reg);

            return r.Replace(retStr, "").Replace(".", "");//将特殊字符替换为"";
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
                    this.WriteMessage($"正则表达式的值不能为空"  );
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

                this.WriteMessage($"总共统计出【{retSrcArray.Length}】张图片");

                return retSrcArray;

            }
            catch (Exception ex)
            {
                this.WriteMessage($"正则所有图片时出错：{ex}"  );
                return retSrcArray = new string[0]; 
            }

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
            this.WriteMessage($"开始获取==>【{targetUrl}】网页文档的数据"  );
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

                this.WriteMessage($"获取网页文档时出错,还在尝试……");

                try
                {
                    PhantomJSDriverService services = PhantomJSDriverService.CreateDefaultService();
                    services.HideCommandPromptWindow = true;//隐藏控制台窗口
                  
                    IWebDriver driver = new PhantomJSDriver(services);
                    driver.Navigate().GoToUrl(targetUrl);
                    retHtmlDoc = driver.PageSource;
                    //Console.WriteLine(driver.PageSource);
                    //Console.Read();
                    driver.Quit();
                    driver.Dispose();
                }
                catch (Exception e)
                {
                    this.WriteMessage($"最终尝试失败!!!!");
                }

                return retHtmlDoc;
            }

        }


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
                this.WriteMessage($"判断网络地址是否有效出错：{ex}！！！！");
            }
            return isExist;

        }




        public void WriteMessage(string msgInfo)
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateWriteMessage(TxtLogOutput), new object[] { msgInfo });
            }
            else
            {
                TxtLogOutput(msgInfo);
            }

        }


        private void TxtLogOutput(string logText)
        {

            this.txt_Log.AppendText($"{logText}" + Environment.NewLine);

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
                    this.WriteMessage($"请先输入有效的网址");
                    //this.txt_Log.AppendText($"请先输入有效的网址"  );
                    MessageBox.Show("网址不能为空！");
                    return isOk = false;
                }
                //判断链接的有效性
                if (!this.UrlIsExist(this.txt_Url.Text))
                {
                    this.WriteMessage($"你输入的网址无法正常访问");
                    //this.txt_Log.AppendText($"你输入的网址无法正常访问"  );
                    MessageBox.Show("请先输入有效的网址！");
                    return isOk = false;
                }
                if (string.IsNullOrEmpty(this.txt_RegularText.Text))
                {
                    this.WriteMessage($"请输入有效的正则表达式");
                    //this.txt_Log.AppendText($"请输入有效的正则表达式"  );
                    MessageBox.Show("请输入有效的正则表达式！");
                    return isOk = false;
                }
            }
            catch (Exception ex)
            {
                this.WriteMessage($"验证时出错！{ex}");
                //this.txt_Log.AppendText($"验证时出错！{ex}"  );
                return isOk = false;
            }

            return isOk;
        }



        #endregion

        private void cBox_Enable_CheckedChanged(object sender, EventArgs e)
        {
            this.WriteMessage("是否启用二级获取二级链接数据  实现中……");
            MessageBox.Show("是否启用二级获取二级链接数据  实现中……");


            this.cBox_Enable.Checked = false;
            return;

        }


        /// <summary>
        /// 鼠标离开网址输入框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Url_LostFocus(object sender, EventArgs e)
        {
            //判断用是否有效
            if (!this.Validation())
            {
                this.txt_Url.Text = "https://mp.weixin.qq.com/s?__biz=MzAwNjY0ODUzMw==&mid=2650312221&idx=1&sn=a1b979cc8c32348567b475abf53b6c9d&chksm=830609a7b47180b15be6714965fe4824e6c08146fd8a2519a99dfeafea6384d2ff720e2e5f66&scene=21#wechat_redirect";
            }
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
                defSavaPath = dialog.SelectedPath;

                this.lbl_SavaPath.Text = defSavaPath;

                this.WriteMessage($"文件将会保存至：{defSavaPath}");
            }
        }

        private void cBox_IsSavaText_CheckedChanged(object sender, EventArgs e)
        {
            this.WriteMessage("待  实现中……");
            MessageBox.Show("待  实现中……");
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

        private void rdb_Wx_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdb_Wx.Checked)
            {
                this.txt_RegularText.Text = @"<img\b[^<>]*?\bdata-src[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
                this.txt_RegularText.ReadOnly = true;
                this.WriteMessage($"切换至{rdb_Wx.Text},【不可编辑】正则表达式");
            }
        }

        private void rdb_Other_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdb_Other.Checked)
            {
                this.txt_RegularText.Text = @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>";
                this.txt_RegularText.ReadOnly = false;

                this.WriteMessage($"切换至{rdb_Other.Text},【可自定义】正则表达式");
            }
        }
    }
}
