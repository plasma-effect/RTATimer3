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
using System.Runtime.Serialization;

namespace Timer
{
    public partial class TimerForm : Form
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
            PossibleTimeSave,
            BalancedPB,
            BalancedGoal
        }

        private TargetLabels MakeTargetLabels(int index, string name, TimeSpan?[] rec, TargetLabels.TimeSpanFlag timeSpanFlag, TargetLabels.PrevColorFlag prevColorFlag)
        {
            this.targetNames[index].Text = name;
            return new TargetLabels(rec, this.targetCurrent[index], this.targetPrevious[index], timeSpanFlag, prevColorFlag);
        }

        public TimerForm()
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
            this.records = RecordTest.MakeTestData("test", 3, 500, 3000, "First", "Second", "Last");//LoadFile(Properties.Settings.Default.DefaultPath);
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
            this.balanced = new TimeSpan?[this.records[this.category][this.route].SegmentCount];
            if (this.mypb.Last() - this.myssb.Last() is TimeSpan bal)
            {
                AverageTargetSet(this.myssb, this.balanced, bal);
            }
            this.goal = new TimeSpan?[this.records[this.category][this.route].SegmentCount];
            if (this.goaltime - this.myssb.Last() is TimeSpan gl)
            {
                AverageTargetSet(this.myssb, this.goal, gl);
            }
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
                    case TargetType.PossibleTimeSave:
                        {
                            var ret = new TimeSpan?[this.mysb.Length];
                            ret[0] = this.mypb[0] - this.mysb[0];
                            foreach (var j in Utility.Range(1, this.mysb.Length)) 
                            {
                                ret[j] = (this.mypb[j] - this.mypb[j - 1]) - this.mysb[j];
                            }
                            this.targetLabels[i] = MakeTargetLabels(i, "Possible Time Save", ret,
                                TargetLabels.TimeSpanFlag.None,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BalancedPB:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Balanced", this.balanced,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BalancedGoal:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Goal", this.goal,
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
        TimeSpan?[] balanced;
        TimeSpan?[] goal;
        TimeSpan? goaltime;
        int nowsegment;
        DateTime start;

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
                    this.records[this.category][this.route].AddRecord(this.running, this.start);
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
                this.start = DateTime.Now;
                this.stopwatch.Restart();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (this.stopwatch.IsRunning && this.prev.Milliseconds / 100 != this.stopwatch.Elapsed.Milliseconds / 100)
            {
                this.prev = this.stopwatch.Elapsed;
                this.mainTimer.Text = Utility.SpanToString(this.prev);
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

        private void EditRecordToolStripMenuItemClick(object sender, EventArgs e)
        {
            var form = new RecordEditor
            {
                Record = this.records[this.category][this.route]
            };
            form.ShowDialog(this);
            this.records[this.category][this.route] = form.Record;
            TargetSet();
        }

        private void TargetCheckToolStripMenuItemClick(object sender, EventArgs e)
        {
            var form = new TargetCheck();
            form.SetName(this.records[this.category][this.route].SegmentName);
            form.SetTime(0, this.mypb);
            form.SetTime(1, this.mysb);
            form.SetTime(2, this.myssb);
            form.SetTime(3, this.balanced);
            form.SetTime(4, this.goal);
            form.ShowDialog(this);
        }
    }
}
