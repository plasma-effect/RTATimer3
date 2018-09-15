using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Timer.Utility;

namespace Timer
{
    public partial class TargetCheck : Form
    {
        public class InsideData
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }

        public TargetCheck()
        {
            InitializeComponent();
        }
        
        public void SetTime(int column, TimeSpan?[] value)
        {
            this.dataGridView1.RowCount = Math.Max(this.dataGridView1.RowCount, value.Length);
            foreach (var (v, index) in value.Indexed())
            {
                this.dataGridView1[column + 1, index].Value = StrictSpanToString(v);
            }
        }

        public void SetName(string[] names)
        {
            this.dataGridView1.RowCount = Math.Max(this.dataGridView1.RowCount, names.Length);
            foreach (var (v, index) in names.Indexed()) 
            {
                this.dataGridView1[0, index].Value = v;
            }
        }

        private void CompleteClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public int Target0
        {
            get
            {
                return this.comboBox1.SelectedIndex;
            }
            set
            {
                this.comboBox1.SelectedIndex = value;
            }
        }
        public int Target1
        {
            get
            {
                return this.comboBox2.SelectedIndex;
            }
            set
            {
                this.comboBox2.SelectedIndex = value;
            }
        }
        public int Target2
        {
            get
            {
                return this.comboBox3.SelectedIndex;
            }
            set
            {
                this.comboBox3.SelectedIndex = value;
            }
        }
        public int Target3
        {
            get
            {
                return this.comboBox4.SelectedIndex;
            }
            set
            {
                this.comboBox4.SelectedIndex = value;
            }
        }
    }
}
