﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace Timer
{
    public partial class Form1 : Form
    {
        class TargetLabels
        {
            public enum TimeSpanFlag
            {
                Total,
                Segment,
                None
            }
            [Flags]
            public enum PrevColorFlag
            {
                None = 0,
                Minus = 1,
                Plus = 2
            }
            TimeSpan?[] target;
            Label current;
            Label previous;
            TimeSpanFlag prevflag;
            PrevColorFlag prevcolorflag;

            public TargetLabels(TimeSpan?[] target, Label current, Label previous, TimeSpanFlag prevflag, PrevColorFlag prevcolorflag)
            {
                this.target = target;
                this.current = current;
                this.previous = previous;
                this.prevflag = prevflag;
                this.prevcolorflag = prevcolorflag;
                this.previous.Text = "";
            }

            private void TextSet(Label label, TimeSpan? diff)
            {
                var str = Utility.SpanToString(diff);
                if (diff < TimeSpan.Zero)
                {
                    str = "-" + str;
                    label.Text = str;
                    if ((this.prevcolorflag & PrevColorFlag.Minus) == PrevColorFlag.Minus)
                    {
                        Utility.SetGreenColorLabel(label);
                        return;
                    }
                }
                else if (diff > TimeSpan.Zero)
                {
                    str = "+" + str;
                    label.Text = str;
                    if ((this.prevcolorflag & PrevColorFlag.Plus) == PrevColorFlag.Plus)
                    {
                        Utility.SetRedColorLabel(label);
                        return;
                    }
                }
                else
                {
                    label.Text = "--:--:--.-";
                }
                Utility.SetNormalColorLabel(label);
            }

            public void TimeSet(int index, TimeSpan?[] record)
            {
                if (index == record.Length)
                {
                    if (this.prevflag == TimeSpanFlag.None)
                    {
                        this.current.Text = "";
                        return;
                    }
                    var diff = record[index - 1] - this.target[index - 1];
                    if (this.prevflag == TimeSpanFlag.Segment && index >= 2)
                    {
                        diff -= record[index - 2];
                    }
                    TextSet(this.current, diff);
                }
                else
                {
                    this.current.Text = Utility.SpanToString(this.target[index]);
                    Utility.SetNormalColorLabel(this.current);
                }
                if (this.prevflag != TimeSpanFlag.None)
                {
                    TimeSpan? diff = null;
                    if (this.prevflag == TimeSpanFlag.Segment && index >= 2)
                    {
                        diff = (record[index - 1] - record[index - 2]) - this.target[index - 1];
                    }
                    else if (index >= 1)
                    {
                        diff = record[index - 1] - this.target[index - 1];
                    }
                    TextSet(this.previous, diff);
                }
            }
        }

        enum TargetType
        {
            PersonalBest,
            SegmentBest,
            SumOfBestSegments,
            BestPossibleTime,
            BalancedPB,
            BalancedGoal
        }

        private TargetLabels MakeTargetLabels(int index, string name, TimeSpan?[] rec, TargetLabels.TimeSpanFlag timeSpanFlag, TargetLabels.PrevColorFlag prevColorFlag)
        {
            this.targetNames[index].Text = name;
            return new TargetLabels(rec, this.targetCurrent[index], this.targetPrevious[index], timeSpanFlag, prevColorFlag);
        }

        public Form1()
        {
            InitializeComponent();
            this.stopwatch = new Stopwatch();
            this.prev = new TimeSpan();
            this.timer1.Start();
            this.targetNames = new Label[]
            {
                this.targetName0,
                this.targetName1,
                this.targetName2,
                this.targetName3
            };
            this.targetCurrent = new Label[]
            {
                this.currentTarget0,
                this.currentTarget1,
                this.currentTarget2,
                this.currentTarget3
            };
            this.targetPrevious = new Label[]
            {
                this.prevTarget0,
                this.prevTarget1,
                this.prevTarget2,
                this.prevTarget3
            };
            this.targettypes = new TargetType[]
            {
                (TargetType)Properties.Settings.Default.Target0,
                (TargetType)Properties.Settings.Default.Target1,
                (TargetType)Properties.Settings.Default.Target2,
                (TargetType)Properties.Settings.Default.Target3
            };
            this.targetLabels = new TargetLabels[4];
            this.records = GameRecord.MakeTestData();//LoadFile(Properties.Settings.Default.DefaultPath);
            this.category = 0;//Math.Max(0, Math.Min(this.records.CategoryRecords.Count - 1, Properties.Settings.Default.DefaultCategory));
            this.route = 1;// Math.Max(0, Math.Min(this.records[this.category].MyRecords.Count - 1, Properties.Settings.Default.DefaultRoute));
            UpdatePB();
            TargetSet();
        }

        private void UpdatePB()
        {
            this.mypb = this.records[this.category][this.route].RouteBest;
            this.mysb = this.records[this.category][this.route].SegmentBests;
            this.myssb = Utility.CumSum(this.mysb);
            this.personalBest.Text = Utility.StrictSpanToString(this.mypb.Last());
            this.sumOfBestSegments.Text = Utility.StrictSpanToString(this.myssb.Last());
        }

        private void TargetSet()
        {
            this.currentSegmentLabel.Text = this.records[this.category][this.route].SegmentName[0];
            this.previousSegmentLabel.Text = "";
            var pb = this.records[this.category].PersonalBest;
            this.running = new TimeSpan?[this.mysb.Length];
            foreach (var i in Utility.Range(0, 4))
            {
                switch (this.targettypes[i])
                {
                    case TargetType.PersonalBest:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Personal Best", this.mypb,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.Plus | TargetLabels.PrevColorFlag.Minus);
                        }
                        break;
                    case TargetType.SegmentBest:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Segment Best", this.mysb,
                                TargetLabels.TimeSpanFlag.Segment,
                                TargetLabels.PrevColorFlag.Minus);
                        }
                        break;
                    case TargetType.SumOfBestSegments:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Best Segments", this.myssb,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BestPossibleTime:
                        {
                            var ret = new TimeSpan?[this.mysb.Length];
                            ret[0] = this.mypb[0] - this.mysb[0];
                            foreach (var j in Utility.Range(1, this.mysb.Length)) 
                            {
                                ret[j] = (this.mypb[j] - this.mypb[j - 1]) - this.mysb[j];
                            }
                            this.targetLabels[i] = MakeTargetLabels(i, "Best Possible Time", ret,
                                TargetLabels.TimeSpanFlag.None,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BalancedPB:
                        {
                            var ret = new TimeSpan?[this.mysb.Length];
                            if (pb - this.myssb.Last() is TimeSpan span)
                            {
                                AverageTargetSet(this.myssb, ret, span);
                            }
                            this.targetLabels[i] = MakeTargetLabels(i, "Balanced", ret,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BalancedGoal:
                        {
                            var ret = new TimeSpan?[this.myssb.Length];
                            if (this.records[this.category].Goal - this.myssb.Last() is TimeSpan span)
                            {
                                AverageTargetSet(this.myssb, ret, span);
                            }
                            this.targetLabels[i] = MakeTargetLabels(i, "Goal", ret,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                }
                this.targetLabels[i].TimeSet(0, this.running);
            }
            this.bestPossibleTime.Text = Utility.StrictSpanToString(this.myssb.Last());
        }

        private static void AverageTargetSet(TimeSpan?[] ssb, TimeSpan?[] ret, TimeSpan span)
        {
            foreach (var j in Utility.Range(0, ssb.Length))
            {
                ret[j] = ssb[j] + new TimeSpan(0, 0, 0, 0, ((int)span.TotalMilliseconds) * (j + 1) / ssb.Length);
            }
        }

        Stopwatch stopwatch;
        TimeSpan prev;
        Label[] targetNames;
        Label[] targetCurrent;
        Label[] targetPrevious;
        TargetLabels[] targetLabels;
        TargetType[] targettypes;
        GameRecord records;
        TimeSpan?[] running;
        int category;
        int route;
        TimeSpan?[] mypb;
        TimeSpan?[] mysb;
        TimeSpan?[] myssb;
        int nowsegment;

        private GameRecord LoadFile(string path)
        {
            try
            {
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)) 
                {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return (GameRecord)formatter.Deserialize(stream);
                }
            }
            catch(InvalidCastException)
            {
                MessageBox.Show("対応していないファイル形式です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(System.IO.FileNotFoundException exp)
            {
                MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new GameRecord("default");
        }

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
                this.running[this.nowsegment] = this.stopwatch.Elapsed;
                ++this.nowsegment;
                if (this.nowsegment == this.running.Length)
                {
                    this.stopwatch.Stop();
                    this.records[this.category][this.route].AddRecord(this.running);
                    UpdatePB();
                    this.bestPossibleTime.Text = "-:--:--.---";
                }
                else
                {
                    this.currentSegmentLabel.Text = this.records[this.category][this.route].SegmentName[this.nowsegment];
                    this.previousSegmentLabel.Text = this.records[this.category][this.route].SegmentName[this.nowsegment - 1];
                    this.bestPossibleTime.Text = Utility.StrictSpanToString(this.running[this.nowsegment - 1] - this.myssb[this.nowsegment - 1] + this.myssb.Last());
                    this.previousTotal.Text = Utility.SpanToString(this.running[this.nowsegment - 1]);
                    if (this.nowsegment >= 2)
                    {
                        this.previousSegment.Text = Utility.SpanToString(this.running[this.nowsegment - 1] - this.running[this.nowsegment - 2]);
                    }
                    else
                    {
                        this.previousSegment.Text = Utility.SpanToString(this.running[this.nowsegment - 1]);
                    }
                }
                foreach(var i in Utility.Range(0, 4))
                {
                    this.targetLabels[i].TimeSet(this.nowsegment, this.running);
                }
            }
            else
            {
                this.running = new TimeSpan?[this.running.Length];
                this.nowsegment = 0;
                TargetSet();
                this.stopwatch.Restart();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (this.stopwatch.IsRunning && this.prev.Milliseconds / 100 != this.stopwatch.Elapsed.Milliseconds / 100)
            {
                this.prev = this.stopwatch.Elapsed;
                this.mainTimer.Text = this.prev.ToString(@"h\:mm\:ss\.f");
                if (this.nowsegment == 0)
                {
                    this.segmentTimer.Text = this.mainTimer.Text;
                }
                else
                {
                    this.segmentTimer.Text = Utility.SpanToString(this.prev - this.running[this.nowsegment - 1]);
                }
            }
        }
    }
}
