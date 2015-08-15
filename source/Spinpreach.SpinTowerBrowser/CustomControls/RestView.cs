using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spinpreach.SpinTowerBrowser.CustomControls
{
    public class RestView : BaseView
    {

        public RestView() : base() { }

        public void Style()
        {

            var middleCenter = new DataGridViewCellStyle(); 
            var middleRight = new DataGridViewCellStyle(); 
            var middleLeft = new DataGridViewCellStyle(); 

            middleCenter.Alignment = DataGridViewContentAlignment.MiddleCenter;
            middleRight.Alignment = DataGridViewContentAlignment.MiddleRight;
            middleLeft.Alignment = DataGridViewContentAlignment.MiddleLeft;

            var column1 = new DataGridViewTextBoxColumn()
            {
                Frozen = true,
                HeaderText = "No",
                Name = "no",
                DataPropertyName = "no",
                ReadOnly = true,
                Width = 35,
                DefaultCellStyle = middleCenter,
            };
            var column2 = new DataGridViewTextBoxColumn()
            {
                Frozen = true,
                HeaderText = "艦娘",
                Name = "name",
                DataPropertyName = "name",
                ReadOnly = true,
                Width = 130,
                DefaultCellStyle = middleLeft,
            };
            var column3 = new DataGridViewTextBoxColumn()
            {
                Frozen = true,
                HeaderText = "Lv",
                Name = "lv",
                DataPropertyName = "lv",
                ReadOnly = true,
                Width = 35,
                DefaultCellStyle = middleRight,
            };
            var column4 = new DataGridViewTextBoxColumn()
            {
                Frozen = true,
                HeaderText = "入渠時間",
                Name = "time",
                DataPropertyName = "time",
                ReadOnly = true,
                Width = 70,
                DefaultCellStyle = middleRight,
            };

            DataGridViewColumn[] columns = {
                column1,
                column2,
                column3,
                column4,
            };

            this.Columns.AddRange(columns);

            this.Width = this.Columns.Cast<DataGridViewTextBoxColumn>().Sum(x => x.Width) + 1;
            this.Height = 180;

            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

        }

    }
}