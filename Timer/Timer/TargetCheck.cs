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
    }
}
