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
    public partial class GoalChange : Form
    {
        public GoalChange()
        {
            InitializeComponent();
        }

        private void CompleteClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public TimeSpan? NewGoal
        {
            get
            {
                if(TimeSpan.TryParse(this.textBox1.Text,out var t))
                {
                    return t;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
