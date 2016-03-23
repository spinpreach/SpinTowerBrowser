﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace Spinpreach.ITowerViewer
{
    public class ITowerBrowser : WebBrowser
    {

        const string GAME_URL = "http://www.dmm.com/netgame/social/-/gadgets/=/app_id=137465/";

        public Action LoginCompletedEvent;
        public Action<Exception> LoginErrorEvent;
        public Action<bool> MuteChangedEvent;

        public LoginInfo login { get; set; }
        private Audio audio;

        public ITowerBrowser()
        {
            ViewerRegistry.SetRenderingMode(ViewerRegistry.ENUM_RENDERING_MODE.IE9);
            this.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.ITowerBrowser_DocumentCompleted);
        }

        private void ITowerBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                var Browser = (WebBrowser)sender;
                if (e.Url.PathAndQuery.StartsWith("/my/-/login/=/path="))
                {
                    var inputs = Browser.Document.GetElementsByTagName("input");
                    var LoginId = inputs.Cast<HtmlElement>().Single(el => el.Id == "login_id");
                    var Password = inputs.Cast<HtmlElement>().Single(el => el.Id == "password");
                    var Submit = inputs.Cast<HtmlElement>().Single(x => x.OuterHtml.Contains("submit"));

                    LoginId.SetAttribute("value", this.login.UserID);
                    Password.SetAttribute("value", this.login.PassWord);
                    Submit.InvokeMember("click");
                }
                else if (e.Url.ToString() == GAME_URL)
                {
                    // 少しタイミングを遅らせる。
                    this.CompletedAsync();
                }
            }
            catch (Exception ex)
            {
                this.LoginErrorEvent?.Invoke(ex);
            }
        }

        private async void CompletedAsync()
        {
            await Task.Run(() => { System.Threading.Thread.Sleep(1500); }); // ← 要調整（画面読み込み待機）
            this.documentChanger();
            this.frameReSize();
            this.Resize += (sender, e) => this.frameReSize();
            this.audio = new Audio();
            this.audio.MuteChangedEvent += (mute) => this.Invoke(new Action<bool>(this.Audio_MuteChanged), mute);
            this.LoginCompletedEvent?.Invoke(); // ← 読み込み完了通知は最後！！
        }

        private void Audio_MuteChanged(bool mute)
        {
            if (this.IsMute() == null) return;
            this.MuteChangedEvent?.Invoke(mute);
        }

        #region method

        private void SetDefaultElementStyle(mshtml.HTMLParaElement element)
        {
            element.style.margin = "0px";
            element.style.padding = "0px";
            element.style.backgroundColor = "black";
        }

        private void documentChanger()
        {
            try
            {
                var document1 = this.Document.DomDocument as mshtml.HTMLDocument;
                if (document1 == null) return;

                var body1 = document1.getElementsByTagName("body").item(0) as mshtml.HTMLBody;
                body1.style.margin = "0px";
                body1.style.padding = "0px";
                body1.style.overflow = "hidden";
                body1.style.backgroundColor = "black";

                var div1 = document1.getElementsByTagName("div").Cast<mshtml.HTMLParaElement>();
                foreach (mshtml.HTMLParaElement item in div1)
                {
                    this.SetDefaultElementStyle(item);
                    if (item.id == "w") continue;
                    if (item.id == "main-ntg") continue;
                    if (item.id == "page") continue;
                    if (item.id == "area-game") continue;
                    item.style.display = "none";
                }
                var p1 = document1.getElementsByTagName("p").Cast<mshtml.HTMLParaElement>();
                foreach (mshtml.HTMLParaElement item in p1)
                {
                    item.style.display = "none";
                }

                var frame = document1.getElementById("game_frame") as mshtml.HTMLFrameElement;
                if (frame == null) return;

                var document2 = this.ConvertToDocument(frame);
                if (document2 == null) return;

                var body2 = document2.getElementsByTagName("body").item(0) as mshtml.HTMLBody;
                body2.style.backgroundColor = "black";

                var div2 = document2.getElementsByTagName("div").Cast<mshtml.HTMLParaElement>();
                foreach (mshtml.HTMLParaElement item in div2)
                {
                    this.SetDefaultElementStyle(item);
                    if (item.id == "flashWrap") continue;
                    item.style.display = "none";
                }
                var p2 = document2.getElementsByTagName("p").Cast<mshtml.HTMLParaElement>();
                foreach (mshtml.HTMLParaElement item in p2)
                {
                    item.style.display = "none";
                }

                var main_ntg = div1.SingleOrDefault(x => x.id == "main-ntg");
                if (main_ntg != null)
                {
                    main_ntg.style.textAlign = "left";
                }

                var flashWrap = div2.SingleOrDefault(x => x.id == "flashWrap");
                if (flashWrap != null)
                {
                    flashWrap.style.textAlign = "left";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}.{1} >> {2}", ex.TargetSite.ReflectedType.FullName, ex.TargetSite.Name, ex.Message));
            }
        }

        private mshtml.HTMLDocument ConvertToDocument(mshtml.HTMLFrameElement frame)
        {
            try
            {
                if (frame == null) return null;

                var window = frame.contentWindow as mshtml.HTMLWindow2;
                if (window == null) return null;

                var provider = window as IServiceProvider;
                if (provider == null) return null;

                var guidService = typeof(SHDocVw.IWebBrowserApp).GUID;
                var riid = typeof(SHDocVw.IWebBrowser2).GUID;
                object ppvObject;
                provider.QueryService(guidService, riid, out ppvObject);

                var webBrowser = ppvObject as SHDocVw.IWebBrowser2;
                if (webBrowser == null) return null;

                return webBrowser.Document as mshtml.HTMLDocument;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void frameReSize()
        {
            try
            {
                if (this.Document == null) return;

                var document1 = this.Document.DomDocument as mshtml.HTMLDocument;
                if (document1 == null) return;

                var frame = document1.getElementById("game_frame") as mshtml.HTMLFrameElement;
                if (frame == null) return;

                var document2 = this.ConvertToDocument(frame);
                if (document2 == null) return;

                var embed = document2.getElementsByTagName("EMBED").item(0) as mshtml.HTMLEmbed;
                if (embed == null) return;

                var resize = true; if (resize)
                {
                    frame.style.height = string.Format("{0}px", this.Height);
                    frame.style.width = string.Format("{0}px", this.Width);
                    embed.style.height = string.Format("{0}px", this.Height);
                    embed.style.width = string.Format("{0}px", this.Width);
                }
                else
                {
                    frame.style.height = string.Format("{0}px", 480);
                    frame.style.width = string.Format("{0}px", 800);
                    embed.style.height = string.Format("{0}px", 480);
                    embed.style.width = string.Format("{0}px", 800);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}.{1} >> {2}", ex.TargetSite.ReflectedType.FullName, ex.TargetSite.Name, ex.Message));
            }
        }

        public void Start()
        {
            this.login = LoginInfo.Load();
            if (this.login.IsExists())
            {
                this.audio = null;
                this.Navigate(GAME_URL);
            }
        }

        public void ScreenShot(string Path)
        {
            try
            {
                var document1 = this.Document.DomDocument as mshtml.HTMLDocument;
                if (document1 == null) return;

                var frame = document1.getElementById("game_frame") as mshtml.HTMLFrameElement;
                if (frame == null) return;

                var document2 = this.ConvertToDocument(frame);
                if (document2 == null) return;

                var embed = document2.getElementsByTagName("EMBED").item(0) as mshtml.HTMLEmbed;
                if (embed == null) return;

                var width = int.Parse(embed.width);
                var height = int.Parse(embed.height);
                var image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                var rect = new RECT { left = 0, top = 0, width = width, height = height, };
                var tdevice = new DVTARGETDEVICE { tdSize = 0, };
                using (var graphics = Graphics.FromImage(image))
                {
                    var hdc = graphics.GetHdc();
                    IViewObject viewObject = embed as IViewObject;
                    viewObject.Draw(1, 0, IntPtr.Zero, tdevice, IntPtr.Zero, hdc, rect, null, IntPtr.Zero, IntPtr.Zero);
                    graphics.ReleaseHdc(hdc);
                }
                image.Save(Path, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception : {0}.{1} >> {2}", ex.TargetSite.ReflectedType.FullName, ex.TargetSite.Name, ex.Message));
            }
        }

        public bool? IsMute()
        {
            if (this.audio == null) { return null; }
            return this.audio.IsMute();
        }

        public void ToggleMute()
        {
            if (this.audio == null) { return; }
            this.audio.ToggleMute();
        }

        #endregion

    }
}
