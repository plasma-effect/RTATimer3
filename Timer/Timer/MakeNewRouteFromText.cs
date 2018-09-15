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
    public partial class MakeNewRouteFromText : Form
    {
        public MakeNewRouteFromText()
        {
            InitializeComponent();
        }

        private void OpenClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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
