using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace Timer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.stopwatch = new Stopwatch();
            this.prev = new TimeSpan();
            this.timer1.Start();
        }
        Stopwatch stopwatch;
        TimeSpan prev;

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    KeyEnterDown();
                    break;
            }
        }

        private void KeyEnterDown()
        {
            if (this.stopwatch.IsRunning)
            {
                this.stopwatch.Stop();
            }
            else
            {
                this.stopwatch.Restart();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (this.prev.Milliseconds / 100 != this.stopwatch.Elapsed.Milliseconds / 100) 
            {
                this.prev = this.stopwatch.Elapsed;
                this.mainTimer.Text = this.prev.ToString(@"h\:mm\:ss\.f");
            }
        }
    }
}
