namespace Timer
{
    partial class TargetCheck
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SegmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PersonalBest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SegmentBest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SumOfSegmentBest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalancedPB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Goal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SegmentName,
            this.PersonalBest,
            this.SegmentBest,
            this.SumOfSegmentBest,
            this.BalancedPB,
            this.Goal});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(950, 505);
            this.dataGridView1.TabIndex = 0;
            // 
            // SegmentName
            // 
            this.SegmentName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SegmentName.HeaderText = "Name";
            this.SegmentName.Name = "SegmentName";
            this.SegmentName.ReadOnly = true;
            this.SegmentName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SegmentName.Width = 72;
            // 
            // PersonalBest
            // 
            this.PersonalBest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PersonalBest.HeaderText = "PB";
            this.PersonalBest.Name = "PersonalBest";
            this.PersonalBest.ReadOnly = true;
            this.PersonalBest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PersonalBest.Width = 46;
            // 
            // SegmentBest
            // 
            this.SegmentBest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SegmentBest.HeaderText = "Seg Best";
            this.SegmentBest.Name = "SegmentBest";
            this.SegmentBest.ReadOnly = true;
            this.SegmentBest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SegmentBest.Width = 106;
            // 
            // SumOfSegmentBest
            // 
            this.SumOfSegmentBest.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SumOfSegmentBest.HeaderText = "SSB";
            this.SumOfSegmentBest.Name = "SumOfSegmentBest";
            this.SumOfSegmentBest.ReadOnly = true;
            this.SumOfSegmentBest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SumOfSegmentBest.Width = 59;
            // 
            // BalancedPB
            // 
            this.BalancedPB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.BalancedPB.HeaderText = "Balanced";
            this.BalancedPB.Name = "BalancedPB";
            this.BalancedPB.ReadOnly = true;
            this.BalancedPB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BalancedPB.Width = 106;
            // 
            // Goal
            // 
            this.Goal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Goal.HeaderText = "Goal";
            this.Goal.Name = "Goal";
            this.Goal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Goal.Width = 60;
            // 
            // TargetCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 529);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TargetCheck";
            this.Text = "TargetCheck";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SegmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PersonalBest;
        private System.Windows.Forms.DataGridViewTextBoxColumn SegmentBest;
        private System.Windows.Forms.DataGridViewTextBoxColumn SumOfSegmentBest;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalancedPB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Goal;
    }
}