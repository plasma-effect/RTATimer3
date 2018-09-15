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
    public partial class CategoryRouteChange : Form
    {
        public CategoryRouteChange()
        {
            InitializeComponent();
        }
        public List<string> CategoryList { get; set; }
        public List<List<string>> RouteList { get; set; }
        int category;
        int route;
        public int Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
                this.categoryLabel.Text = this.CategoryList[value];
                this.categoryIndexLabel.Text = $"{this.category + 1} / {this.CategoryList.Count}";
            }
        }
        public int Route
        {
            get
            {
                return this.route;
            }
            set
            {
                this.route = value;
                this.routeLabel.Text = this.RouteList[this.category][value];
                this.routeIndexLabel.Text = $"{this.route + 1} / {this.RouteList[this.category].Count}";
            }
        }

        private void CompleteClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void CategoryNextClick(object sender, EventArgs e)
        {
            if (this.category != this.CategoryList.Count - 1)
            {
                this.Category = this.category + 1;
                this.Route = 0;
            }
        }

        private void CategoryPrevClick(object sender, EventArgs e)
        {
            if (this.category != 0)
            {
                this.Category = this.category - 1;
                this.Route = 0;
            }
        }

        private void RouteNextClick(object sender, EventArgs e)
        {
            if (this.route != this.RouteList[this.category].Count - 1) 
            {
                this.Route = this.route + 1;
            }
        }

        private void RoutePrevClick(object sender, EventArgs e)
        {
            if (this.route != 0)
            {
                this.Route = 0;
            }
        }
    }
}
