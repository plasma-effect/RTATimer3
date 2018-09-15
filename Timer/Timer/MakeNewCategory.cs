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
    public partial class MakeNewCategory : Form
    {
        public MakeNewCategory()
        {
            InitializeComponent();
        }

        private void ComplateClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public string CategoryName
        {
            get
            {
                return this.categoryBox.Text;
            }
        }

        public TimeSpan? Goal
        {
            get
            {
                if(TimeSpan.TryParse(this.goalBox.Text,out var ret))
                {
                    return ret;
                }
                else
                {
                    return null;
                }
            }
        }

        public string[] Route
        {
            get
            {
                var len = this.dataGridView1.RowCount - 1;
                if (len == 0)
                {
                    return new string[] { "Complete" };
                }
                var ret = new string[len];
                foreach(var i in Utility.Range(0, len))
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
                return this.routeBox.Text;
            }
        }
    }
}
