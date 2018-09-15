namespace Timer
{
    partial class CategoryRouteChange
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.categoryPrev = new System.Windows.Forms.Button();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.routeLabel = new System.Windows.Forms.Label();
            this.categoryNext = new System.Windows.Forms.Button();
            this.routePrev = new System.Windows.Forms.Button();
            this.routeNext = new System.Windows.Forms.Button();
            this.Complete = new System.Windows.Forms.Button();
            this.categoryIndexLabel = new System.Windows.Forms.Label();
            this.routeIndexLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // categoryPrev
            // 
            this.categoryPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryPrev.Location = new System.Drawing.Point(335, 60);
            this.categoryPrev.Name = "categoryPrev";
            this.categoryPrev.Size = new System.Drawing.Size(60, 60);
            this.categoryPrev.TabIndex = 1;
            this.categoryPrev.Text = "前";
            this.categoryPrev.UseVisualStyleBackColor = true;
            this.categoryPrev.Click += new System.EventHandler(this.CategoryPrevClick);
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.categoryLabel.Location = new System.Drawing.Point(12, 9);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(135, 48);
            this.categoryLabel.TabIndex = 4;
            this.categoryLabel.Text = "label1";
            // 
            // routeLabel
            // 
            this.routeLabel.AutoSize = true;
            this.routeLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.routeLabel.Location = new System.Drawing.Point(12, 123);
            this.routeLabel.Name = "routeLabel";
            this.routeLabel.Size = new System.Drawing.Size(135, 48);
            this.routeLabel.TabIndex = 5;
            this.routeLabel.Text = "label2";
            // 
            // categoryNext
            // 
            this.categoryNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryNext.Location = new System.Drawing.Point(402, 60);
            this.categoryNext.Name = "categoryNext";
            this.categoryNext.Size = new System.Drawing.Size(60, 60);
            this.categoryNext.TabIndex = 6;
            this.categoryNext.Text = "次";
            this.categoryNext.UseVisualStyleBackColor = true;
            this.categoryNext.Click += new System.EventHandler(this.CategoryNextClick);
            // 
            // routePrev
            // 
            this.routePrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.routePrev.Location = new System.Drawing.Point(335, 174);
            this.routePrev.Name = "routePrev";
            this.routePrev.Size = new System.Drawing.Size(60, 60);
            this.routePrev.TabIndex = 7;
            this.routePrev.Text = "前";
            this.routePrev.UseVisualStyleBackColor = true;
            this.routePrev.Click += new System.EventHandler(this.RoutePrevClick);
            // 
            // routeNext
            // 
            this.routeNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.routeNext.Location = new System.Drawing.Point(402, 174);
            this.routeNext.Name = "routeNext";
            this.routeNext.Size = new System.Drawing.Size(60, 60);
            this.routeNext.TabIndex = 8;
            this.routeNext.Text = "次";
            this.routeNext.UseVisualStyleBackColor = true;
            this.routeNext.Click += new System.EventHandler(this.RouteNextClick);
            // 
            // Complete
            // 
            this.Complete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Complete.Location = new System.Drawing.Point(262, 240);
            this.Complete.Name = "Complete";
            this.Complete.Size = new System.Drawing.Size(200, 60);
            this.Complete.TabIndex = 9;
            this.Complete.Text = "完了";
            this.Complete.UseVisualStyleBackColor = true;
            this.Complete.Click += new System.EventHandler(this.CompleteClick);
            // 
            // categoryIndexLabel
            // 
            this.categoryIndexLabel.AutoSize = true;
            this.categoryIndexLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.categoryIndexLabel.Location = new System.Drawing.Point(14, 60);
            this.categoryIndexLabel.Name = "categoryIndexLabel";
            this.categoryIndexLabel.Size = new System.Drawing.Size(92, 33);
            this.categoryIndexLabel.TabIndex = 10;
            this.categoryIndexLabel.Text = "label1";
            // 
            // routeIndexLabel
            // 
            this.routeIndexLabel.AutoSize = true;
            this.routeIndexLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.routeIndexLabel.Location = new System.Drawing.Point(12, 174);
            this.routeIndexLabel.Name = "routeIndexLabel";
            this.routeIndexLabel.Size = new System.Drawing.Size(92, 33);
            this.routeIndexLabel.TabIndex = 11;
            this.routeIndexLabel.Text = "label2";
            // 
            // CategoryRouteChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 312);
            this.Controls.Add(this.routeIndexLabel);
            this.Controls.Add(this.categoryIndexLabel);
            this.Controls.Add(this.Complete);
            this.Controls.Add(this.routeNext);
            this.Controls.Add(this.routePrev);
            this.Controls.Add(this.categoryNext);
            this.Controls.Add(this.routeLabel);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.categoryPrev);
            this.Name = "CategoryRouteChange";
            this.Text = "CategoryRouteChange";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button categoryPrev;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.Label routeLabel;
        private System.Windows.Forms.Button categoryNext;
        private System.Windows.Forms.Button routePrev;
        private System.Windows.Forms.Button routeNext;
        private System.Windows.Forms.Button Complete;
        private System.Windows.Forms.Label categoryIndexLabel;
        private System.Windows.Forms.Label routeIndexLabel;
    }
}