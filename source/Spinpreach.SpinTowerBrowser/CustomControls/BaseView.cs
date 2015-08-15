using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Spinpreach.SpinTowerBrowser.CustomControls
{
    public class BaseView : DataGridView
    {
        public BaseView() : base()
        {

            // ColumnHeadersDefaultCellStyle
            var cellStyle1 = new DataGridViewCellStyle()
            {
                Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128))),
                BackColor = Color.FromArgb(255, 196, 37),
                ForeColor = Color.Black,
                SelectionBackColor = Color.White,
                SelectionForeColor = Color.Black,
            };

            // DefaultCellStyle
            var cellStyle2 = new DataGridViewCellStyle()
            {
                Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(128))),
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.White,
                SelectionForeColor = Color.Black,
            };

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // this
            // 
            this.EnableHeadersVisualStyles = false;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;
            this.BackgroundColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BorderStyle = BorderStyle.None;
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ColumnHeadersDefaultCellStyle = cellStyle1;
            this.ColumnHeadersHeight = 30;
            this.DefaultCellStyle = cellStyle2;
            this.MultiSelect = false;
            this.ReadOnly = true;
            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.RowHeadersVisible = false;
            this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.RowTemplate.Height = 25;
            this.ScrollBars = ScrollBars.None;
            this.ShowCellErrors = false;
            this.ShowCellToolTips = false;
            this.ShowEditingIcon = false;
            this.ShowRowErrors = false;
            this.TabStop = false;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}