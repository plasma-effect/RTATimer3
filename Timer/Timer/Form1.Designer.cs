namespace Timer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainTimer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.currentSegmentLabel = new System.Windows.Forms.Label();
            this.currentTarget0 = new System.Windows.Forms.Label();
            this.currentTarget1 = new System.Windows.Forms.Label();
            this.currentTarget2 = new System.Windows.Forms.Label();
            this.currentTarget3 = new System.Windows.Forms.Label();
            this.segmentTimer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.personalBest = new System.Windows.Forms.Label();
            this.sobs = new System.Windows.Forms.Label();
            this.previousSegmentLabel = new System.Windows.Forms.Label();
            this.previousTotal = new System.Windows.Forms.Label();
            this.previousSegment = new System.Windows.Forms.Label();
            this.prevTarget0 = new System.Windows.Forms.Label();
            this.prevTarget1 = new System.Windows.Forms.Label();
            this.prevTarget2 = new System.Windows.Forms.Label();
            this.prevTarget3 = new System.Windows.Forms.Label();
            this.targetName0 = new System.Windows.Forms.Label();
            this.targetName1 = new System.Windows.Forms.Label();
            this.targetName2 = new System.Windows.Forms.Label();
            this.targetName3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainTimer
            // 
            this.mainTimer.AutoSize = true;
            this.mainTimer.Font = new System.Drawing.Font("MS UI Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainTimer.Location = new System.Drawing.Point(12, 9);
            this.mainTimer.Name = "mainTimer";
            this.mainTimer.Size = new System.Drawing.Size(390, 97);
            this.mainTimer.TabIndex = 0;
            this.mainTimer.Text = "0:00:00.0";
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.TimerTick);
            // 
            // currentSegmentLabel
            // 
            this.currentSegmentLabel.AutoSize = true;
            this.currentSegmentLabel.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentSegmentLabel.Location = new System.Drawing.Point(12, 134);
            this.currentSegmentLabel.Name = "currentSegmentLabel";
            this.currentSegmentLabel.Size = new System.Drawing.Size(331, 43);
            this.currentSegmentLabel.TabIndex = 1;
            this.currentSegmentLabel.Text = "Current Segment";
            // 
            // currentTarget0
            // 
            this.currentTarget0.AutoSize = true;
            this.currentTarget0.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentTarget0.Location = new System.Drawing.Point(255, 176);
            this.currentTarget0.Name = "currentTarget0";
            this.currentTarget0.Size = new System.Drawing.Size(132, 33);
            this.currentTarget0.TabIndex = 2;
            this.currentTarget0.Text = "0:00:00.0";
            // 
            // currentTarget1
            // 
            this.currentTarget1.AutoSize = true;
            this.currentTarget1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentTarget1.Location = new System.Drawing.Point(255, 209);
            this.currentTarget1.Name = "currentTarget1";
            this.currentTarget1.Size = new System.Drawing.Size(132, 33);
            this.currentTarget1.TabIndex = 3;
            this.currentTarget1.Text = "0:00:00.0";
            // 
            // currentTarget2
            // 
            this.currentTarget2.AutoSize = true;
            this.currentTarget2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentTarget2.Location = new System.Drawing.Point(255, 242);
            this.currentTarget2.Name = "currentTarget2";
            this.currentTarget2.Size = new System.Drawing.Size(132, 33);
            this.currentTarget2.TabIndex = 4;
            this.currentTarget2.Text = "0:00:00.0";
            // 
            // currentTarget3
            // 
            this.currentTarget3.AutoSize = true;
            this.currentTarget3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentTarget3.Location = new System.Drawing.Point(255, 275);
            this.currentTarget3.Name = "currentTarget3";
            this.currentTarget3.Size = new System.Drawing.Size(132, 33);
            this.currentTarget3.TabIndex = 5;
            this.currentTarget3.Text = "0:00:00.0";
            // 
            // segmentTimer
            // 
            this.segmentTimer.AutoSize = true;
            this.segmentTimer.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.segmentTimer.Location = new System.Drawing.Point(408, 58);
            this.segmentTimer.Name = "segmentTimer";
            this.segmentTimer.Size = new System.Drawing.Size(194, 48);
            this.segmentTimer.TabIndex = 6;
            this.segmentTimer.Text = "0:00:00.0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(12, 461);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(273, 43);
            this.label2.TabIndex = 7;
            this.label2.Text = "Personal Best";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(12, 547);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(436, 43);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sum of Best Segments";
            // 
            // personalBest
            // 
            this.personalBest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.personalBest.AutoSize = true;
            this.personalBest.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.personalBest.Location = new System.Drawing.Point(63, 504);
            this.personalBest.Name = "personalBest";
            this.personalBest.Size = new System.Drawing.Size(222, 43);
            this.personalBest.TabIndex = 9;
            this.personalBest.Text = "0:00:00.000";
            // 
            // sobs
            // 
            this.sobs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sobs.AutoSize = true;
            this.sobs.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sobs.Location = new System.Drawing.Point(63, 590);
            this.sobs.Name = "sobs";
            this.sobs.Size = new System.Drawing.Size(222, 43);
            this.sobs.TabIndex = 10;
            this.sobs.Text = "0:00:00.000";
            // 
            // previousSegmentLabel
            // 
            this.previousSegmentLabel.AutoSize = true;
            this.previousSegmentLabel.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.previousSegmentLabel.Location = new System.Drawing.Point(411, 106);
            this.previousSegmentLabel.Name = "previousSegmentLabel";
            this.previousSegmentLabel.Size = new System.Drawing.Size(218, 27);
            this.previousSegmentLabel.TabIndex = 11;
            this.previousSegmentLabel.Text = "Previous Segment";
            // 
            // previousTotal
            // 
            this.previousTotal.AutoSize = true;
            this.previousTotal.Font = new System.Drawing.Font("MS UI Gothic", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.previousTotal.Location = new System.Drawing.Point(451, 134);
            this.previousTotal.Name = "previousTotal";
            this.previousTotal.Size = new System.Drawing.Size(178, 43);
            this.previousTotal.TabIndex = 12;
            this.previousTotal.Text = "0:00:00.0";
            // 
            // previousSegment
            // 
            this.previousSegment.AutoSize = true;
            this.previousSegment.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.previousSegment.Location = new System.Drawing.Point(635, 144);
            this.previousSegment.Name = "previousSegment";
            this.previousSegment.Size = new System.Drawing.Size(132, 33);
            this.previousSegment.TabIndex = 13;
            this.previousSegment.Text = "0:00:00.0";
            // 
            // prevTarget0
            // 
            this.prevTarget0.AutoSize = true;
            this.prevTarget0.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.prevTarget0.Location = new System.Drawing.Point(497, 176);
            this.prevTarget0.Name = "prevTarget0";
            this.prevTarget0.Size = new System.Drawing.Size(132, 33);
            this.prevTarget0.TabIndex = 14;
            this.prevTarget0.Text = "0:00:00.0";
            // 
            // prevTarget1
            // 
            this.prevTarget1.AutoSize = true;
            this.prevTarget1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.prevTarget1.Location = new System.Drawing.Point(497, 209);
            this.prevTarget1.Name = "prevTarget1";
            this.prevTarget1.Size = new System.Drawing.Size(132, 33);
            this.prevTarget1.TabIndex = 15;
            this.prevTarget1.Text = "0:00:00.0";
            // 
            // prevTarget2
            // 
            this.prevTarget2.AutoSize = true;
            this.prevTarget2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.prevTarget2.Location = new System.Drawing.Point(497, 242);
            this.prevTarget2.Name = "prevTarget2";
            this.prevTarget2.Size = new System.Drawing.Size(132, 33);
            this.prevTarget2.TabIndex = 16;
            this.prevTarget2.Text = "0:00:00.0";
            // 
            // prevTarget3
            // 
            this.prevTarget3.AutoSize = true;
            this.prevTarget3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.prevTarget3.Location = new System.Drawing.Point(497, 275);
            this.prevTarget3.Name = "prevTarget3";
            this.prevTarget3.Size = new System.Drawing.Size(132, 33);
            this.prevTarget3.TabIndex = 17;
            this.prevTarget3.Text = "0:00:00.0";
            // 
            // targetName0
            // 
            this.targetName0.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.targetName0.Location = new System.Drawing.Point(15, 182);
            this.targetName0.Name = "targetName0";
            this.targetName0.Size = new System.Drawing.Size(234, 27);
            this.targetName0.TabIndex = 21;
            this.targetName0.Text = "Personal Best";
            this.targetName0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetName1
            // 
            this.targetName1.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.targetName1.Location = new System.Drawing.Point(15, 214);
            this.targetName1.Name = "targetName1";
            this.targetName1.Size = new System.Drawing.Size(234, 27);
            this.targetName1.TabIndex = 22;
            this.targetName1.Text = "Segment Best";
            this.targetName1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetName2
            // 
            this.targetName2.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.targetName2.Location = new System.Drawing.Point(15, 247);
            this.targetName2.Name = "targetName2";
            this.targetName2.Size = new System.Drawing.Size(234, 27);
            this.targetName2.TabIndex = 23;
            this.targetName2.Text = "Best Possible Time";
            this.targetName2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // targetName3
            // 
            this.targetName3.Font = new System.Drawing.Font("MS UI Gothic", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.targetName3.Location = new System.Drawing.Point(15, 280);
            this.targetName3.Name = "targetName3";
            this.targetName3.Size = new System.Drawing.Size(234, 27);
            this.targetName3.TabIndex = 24;
            this.targetName3.Text = "Balanced Time";
            this.targetName3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 642);
            this.Controls.Add(this.targetName3);
            this.Controls.Add(this.targetName2);
            this.Controls.Add(this.targetName1);
            this.Controls.Add(this.targetName0);
            this.Controls.Add(this.prevTarget3);
            this.Controls.Add(this.prevTarget2);
            this.Controls.Add(this.prevTarget1);
            this.Controls.Add(this.prevTarget0);
            this.Controls.Add(this.previousSegment);
            this.Controls.Add(this.previousTotal);
            this.Controls.Add(this.previousSegmentLabel);
            this.Controls.Add(this.sobs);
            this.Controls.Add(this.personalBest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.segmentTimer);
            this.Controls.Add(this.currentTarget3);
            this.Controls.Add(this.currentTarget2);
            this.Controls.Add(this.currentTarget1);
            this.Controls.Add(this.currentTarget0);
            this.Controls.Add(this.currentSegmentLabel);
            this.Controls.Add(this.mainTimer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label currentSegmentLabel;
        private System.Windows.Forms.Label currentTarget0;
        private System.Windows.Forms.Label currentTarget1;
        private System.Windows.Forms.Label currentTarget2;
        private System.Windows.Forms.Label currentTarget3;
        private System.Windows.Forms.Label segmentTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label personalBest;
        private System.Windows.Forms.Label sobs;
        private System.Windows.Forms.Label previousSegmentLabel;
        private System.Windows.Forms.Label previousTotal;
        private System.Windows.Forms.Label previousSegment;
        private System.Windows.Forms.Label prevTarget0;
        private System.Windows.Forms.Label prevTarget1;
        private System.Windows.Forms.Label prevTarget2;
        private System.Windows.Forms.Label prevTarget3;
        private System.Windows.Forms.Label targetName0;
        private System.Windows.Forms.Label targetName1;
        private System.Windows.Forms.Label targetName2;
        private System.Windows.Forms.Label targetName3;
    }
}

