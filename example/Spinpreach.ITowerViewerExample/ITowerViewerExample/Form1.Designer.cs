namespace Spinpreach.ITowerViewerExample
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.ToggleMuteButton = new System.Windows.Forms.Button();
            this.ScreenShotButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.ITowerBrowser = new Spinpreach.ITowerViewer.ITowerBrowser();
            this.SuspendLayout();
            // 
            // ToggleMuteButton
            // 
            this.ToggleMuteButton.Location = new System.Drawing.Point(12, 110);
            this.ToggleMuteButton.Name = "ToggleMuteButton";
            this.ToggleMuteButton.Size = new System.Drawing.Size(101, 43);
            this.ToggleMuteButton.TabIndex = 6;
            this.ToggleMuteButton.Text = "ToggleMute";
            this.ToggleMuteButton.UseVisualStyleBackColor = true;
            this.ToggleMuteButton.Click += new System.EventHandler(this.ToggleMuteButton_Click);
            // 
            // ScreenShotButton
            // 
            this.ScreenShotButton.Location = new System.Drawing.Point(12, 61);
            this.ScreenShotButton.Name = "ScreenShotButton";
            this.ScreenShotButton.Size = new System.Drawing.Size(101, 43);
            this.ScreenShotButton.TabIndex = 5;
            this.ScreenShotButton.Text = "ScreenShot";
            this.ScreenShotButton.UseVisualStyleBackColor = true;
            this.ScreenShotButton.Click += new System.EventHandler(this.ScreenShotButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(101, 43);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ITowerBrowser
            // 
            this.ITowerBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ITowerBrowser.Location = new System.Drawing.Point(0, 0);
            this.ITowerBrowser.login = null;
            this.ITowerBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ITowerBrowser.Name = "ITowerBrowser";
            this.ITowerBrowser.Size = new System.Drawing.Size(507, 338);
            this.ITowerBrowser.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 338);
            this.Controls.Add(this.ToggleMuteButton);
            this.Controls.Add(this.ScreenShotButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ITowerBrowser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private ITowerViewer.ITowerBrowser ITowerBrowser;
        private System.Windows.Forms.Button ToggleMuteButton;
        private System.Windows.Forms.Button ScreenShotButton;
        private System.Windows.Forms.Button StartButton;
    }
}

