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
    public partial class MakeNewRoute : Form
    {
        public MakeNewRoute()
        {
            InitializeComponent();
        }
        
        private void CompleteClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        
        public string[] Route
        {
            get
            {
                var len = this.dataGridView1.RowCount - 1;
                if (len == 0)
                {
                    return new string[1] { "Complete" };
                }
                var ret = new string[len];
                foreach(var i in Range(0, len))
                {
                    ret[i] = this.dataGridView1[0, i].Value as string;
                }
                return ret;
            }
        }

        public string RouteName
        {
            get
            {
                return this.textBox1.Text;
            }
        }
    }
}
