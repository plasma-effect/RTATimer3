using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timer
{
    public partial class RecordEditor : Form
    {
        public RecordEditor()
        {
            InitializeComponent();
            this.edited = false;
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            SaveFile();
        }

        public class RowData
        {
            public string SegmentName { get; set; }
            public string Time { get; set; }

            public RowData(string segmentName, string time)
            {
                this.SegmentName = segmentName;
                this.Time = time;
            }
        }

        public RouteRecord Record
        {
            set
            {
                this.record = value;
                this.index = this.record.RecordCount - 1;
                this.dataGridView1.RowCount = this.record.SegmentCount;
                DataSet();
            }
            get
            {
                return this.record;
            }
        }

        public void DataSet()
        {
            foreach (var i in Utility.Range(0, this.record.SegmentCount))
            {
                this.dataGridView1[0, i].Value = this.record.SegmentName[i];
                this.dataGridView1[1, i].Value = Utility.StrictSpanToString(this.record[this.index][i]);
            }
            this.RecordDateTimeLabel.Text = this.record[this.index].PlayDateTime.ToString();
            this.edited = false;
            this.IndexLabel.Text = $"{this.index + 1} / {this.record.RecordCount}";
        }

        RouteRecord record;
        int index;
        bool edited;

        private void DataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.edited = true;
        }

        private void ClickPrevious(object sender, EventArgs e)
        {
            ChangeData(-1);
        }

        private void ClickNext(object sender, EventArgs e)
        {
            ChangeData(1);
        }

        private void ChangeData(int slide)
        {
            var next = this.index + slide;
            if (next >= 0 && next < this.record.RecordCount) 
            {
                if (this.edited)
                {
                    switch (MessageBox.Show("変更されています、保存しますか？", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Cancel:
                            return;
                        case DialogResult.Yes:
                            if (!SaveFile())
                            {
                                return;
                            }
                            break;
                    }
                }
                this.index = next;
                DataSet();
            }
        }

        private bool SaveFile()
        {
            var rec = new TimeSpan?[this.record.SegmentCount];
            var flag = false;
            foreach(var i in Utility.Range(0, this.record.SegmentCount))
            {
                var str = this.dataGridView1[1, i].Value as string;
                if(TimeSpan.TryParse(str,out var res))
                {
                    rec[i] = res;
                }
                else if (!Utility.IsAnyOfParams(str, "-", "--", "---", "--:--", "-:--:--", "--:--.---", "-:--:--.---"))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                if (MessageBox.Show("不明な形式の文字列が含まれています。nullとして扱いますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                {
                    return false;
                }
            }
            this.record[this.index].RestoreTimes(rec);
            this.edited = false;
            return true;
        }

        private void EditCancelButtonClick(object sender, EventArgs e)
        {
            if (this.edited)
            {
                if (MessageBox.Show("保存されていない変更があります。このまま記録編集を終了しますか？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                {
                    return;
                }
            }
            Close();
        }

        private void CompleteButtonClick(object sender, EventArgs e)
        {
            if (!SaveFile())
            {
                return;
            }
            Close();
        }
    }
}
