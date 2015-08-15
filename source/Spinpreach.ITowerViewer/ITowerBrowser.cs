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
            this.Resize += new EventHandler(this.ITowerBrowser_Resize);
        }

        #region Event

        private void ITowerBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {

                System.Diagnostics.Debug.WriteLine(e.Url.ToString());
                var Browser = (WebBrowser)sender;

                if (e.Url.PathAndQuery.StartsWith("/my/-/login/=/path="))
                {
                    HtmlElementCollection inputs = Browser.Document.GetElementsByTagName("input");
                    HtmlElement LoginId = inputs.Cast<HtmlElement>().Single(el => el.Id == "login_id");
                    HtmlElement Password = inputs.Cast<HtmlElement>().Single(el => el.Id == "password");
                    HtmlElement Submit = inputs.Cast<HtmlElement>().Single(x => x.OuterHtml.Contains("submit"));

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
            await Task.Run(() => { System.Threading.Thread.Sleep(500); }); // ← 要調整（画面読み込み待機）
            this.documentChanger();
            this.frameReSize(this.Width, this.Height);
            this.audio = new Audio();
            this.audio.MuteChangedEvent += (isMute) => { this.Invoke(new Action<bool>(this.Audio_MuteChanged), isMute); };
            this.LoginCompletedEvent?.Invoke(); // ← 読み込み完了通知は最後！！
        }

        private void Audio_MuteChanged(bool isMute)
        {
            if (this.IsMute() == null) return;
            this.MuteChangedEvent?.Invoke(isMute);
        }

        private void ITowerBrowser_Resize(object sender, EventArgs e)
        {
            this.frameReSize(this.Width, this.Height);
        }

        #endregion

        #region method

        #region getElementById

        private T getElementById<T>(string id)
        {
            try
            {
                var guidService = typeof(SHDocVw.IWebBrowserApp).GUID;
                var riid = typeof(SHDocVw.IWebBrowser2).GUID;
                var document = (mshtml.HTMLDocument)this.Document.DomDocument;
                if (document == null) { return default(T); }
                for (int i = 0; i < document.frames.length; i++)
                {
                    var provider = document.frames.item(i) as IServiceProvider; if (provider == null) continue;
                    object ppvObject;
                    provider.QueryService(guidService, riid, out ppvObject);
                    var webBrowser = ppvObject as SHDocVw.IWebBrowser2; if (webBrowser == null) continue;
                    var iframeDocument = webBrowser.Document as mshtml.HTMLDocument; if (iframeDocument == null) continue;
                    var element = iframeDocument.getElementById(id); if (element == null) continue;
                    return (T)element;
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        #endregion

        #region StyleSheet

        private void documentChanger()
        {
            try
            {
                var document = (mshtml.HTMLDocument)this.Document.DomDocument;
                if (document == null) { return; }

                //******************************************************************
                var div1 = document.getElementById("dmm-ntgnavi-renew"); if (div1 == null) { return; }
                var div2 = document.getElementById("twitter-widget-0"); if (div2 == null) { return; }
                var div3 = document.getElementById("foot"); if (div3 == null) { return; }
                var div4 = document.getElementById("ntg-recommend"); if (div4 == null) { return; }
                var div5 = document.getElementById("game_frame"); if (div5 == null) { return; }

                //インラインフレーム内のDIV
                var div6 = this.getElementById<mshtml.HTMLDivElement>("infoWrap"); if (div6 == null) return;
                var div7 = this.getElementById<mshtml.HTMLDivElement>("tabWrap"); if (div7 == null) return;
                //******************************************************************

                div1.style.visibility = "hidden";
                div2.style.visibility = "hidden";
                div3.style.visibility = "hidden";
                div4.style.visibility = "hidden";
                div6.style.visibility = "hidden";
                div7.style.visibility = "hidden";

                StringBuilder css = new StringBuilder();
                //*******************************************************************
                css.Append(".naviapp");
                css.Append("{");
                css.Append("    visibility: hidden;");
                css.Append("}");
                //*******************************************************************
                css.Append("body");
                css.Append("{");
                css.Append("    overflow: hidden;");
                css.Append("}");
                ////*******************************************************************
                css.Append("iframe");
                css.Append("{");
                css.Append("    position: fixed;");
                css.Append("    top: 0px;");
                css.Append("    z-index: 1;");
                css.Append("}");
                //*******************************************************************
                var target = (mshtml.HTMLDocument)div5.document;
                target.createStyleSheet().cssText = css.ToString();
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region frameReSize

        private void frameReSize(int width, int height)
        {
            try
            {
                var document = (mshtml.HTMLDocument)this.Document.DomDocument;
                if (document == null) { return; }

                //**************************************************************
                // game_frame
                //**************************************************************
                var div1 = document.getElementById("game_frame"); if (div1 == null) { return; }
                if (width > 800)
                {
                    int x = (width - 800);
                    div1.style.left = string.Format("-{0}px", x);
                    div1.style.width = string.Format("{0}px", width + x);
                    div1.style.height = string.Format("{0}px", height);
                }
                else
                {
                    div1.style.left = string.Format("{0}px", 0);
                    div1.style.width = string.Format("{0}px", width);
                    div1.style.height = string.Format("{0}px", height);
                }

                //**************************************************************
                // flash_object
                //**************************************************************
                var div2 = this.getElementById<mshtml.HTMLEmbed>("externalswf"); if (div2 == null) return;
                div2.style.width = string.Format("{0}px", width);
                div2.style.height = string.Format("{0}px", height);

            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Start

        public void Start()
        {
            this.login = LoginInfo.Load();
            if (this.login.IsExists())
            {
                this.audio = null;
                this.Navigate(GAME_URL);
            }
        }

        #endregion

        #region ScreenShot

        public void ScreenShot(string Path)
        {

            try
            {

                var swf = this.getElementById<mshtml.HTMLEmbed>("externalswf"); if (swf == null) return;

                int width = int.Parse(swf.width);
                int height = int.Parse(swf.height);

                var image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                var rect = new RECT { left = 0, top = 0, width = width, height = height, };
                var tdevice = new DVTARGETDEVICE { tdSize = 0, };

                using (var graphics = Graphics.FromImage(image))
                {
                    var hdc = graphics.GetHdc();
                    IViewObject viewObject = swf as IViewObject;
                    viewObject.Draw(1, 0, IntPtr.Zero, tdevice, IntPtr.Zero, hdc, rect, null, IntPtr.Zero, IntPtr.Zero);
                    graphics.ReleaseHdc(hdc);
                }

                image.Save(Path, ImageFormat.Png);

            }
            catch (Exception)
            {
            }

        }

        #endregion

        #region IsMute

        public bool? IsMute()
        {
            if (this.audio == null) { return null; }
            return this.audio.IsMute();
        }

        #endregion

        #region ToggleMute

        public void ToggleMute()
        {
            if (this.audio == null) { return; }
            this.audio.ToggleMute();
        }

        #endregion

        #endregion

    }
}
