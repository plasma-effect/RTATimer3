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
            TargetSet();
        }

        private void TargetSet()
        {
            this.currentSegmentLabel.Text = this.records[this.category][this.route].SegmentName[0];
            this.previousSegmentLabel.Text = "";
            var rb = this.records[this.category][this.route].RouteBest;
            var sb = this.records[this.category][this.route].SegmentBests;
            var ssb = Utility.CumSum(sb);
            var pb = this.records[this.category].PersonalBest;
            this.running = new TimeSpan?[sb.Length];
            foreach (var i in Utility.Range(0, 4))
            {
                switch (this.targettypes[i])
                {
                    case TargetType.PersonalBest:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Personal Best", rb,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.Plus | TargetLabels.PrevColorFlag.Minus);
                        }
                        break;
                    case TargetType.SegmentBest:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Segment Best", sb,
                                TargetLabels.TimeSpanFlag.Segment,
                                TargetLabels.PrevColorFlag.Minus);
                        }
                        break;
                    case TargetType.SumOfBestSegments:
                        {
                            this.targetLabels[i] = MakeTargetLabels(i, "Best Segments", ssb,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BestPossibleTime:
                        {
                            var ret = new TimeSpan?[sb.Length];
                            ret[0] = rb[0] - sb[0];
                            foreach (var j in Utility.Range(1, sb.Length)) 
                            {
                                ret[j] = (rb[j] - rb[j - 1]) - sb[j];
                            }
                            this.targetLabels[i] = MakeTargetLabels(i, "Best Possible Time", ret,
                                TargetLabels.TimeSpanFlag.None,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BalancedPB:
                        {
                            var ret = new TimeSpan?[sb.Length];
                            if (pb - ssb.Last() is TimeSpan span)
                            {
                                AverageTargetSet(sb, ret, span);
                            }
                            this.targetLabels[i] = MakeTargetLabels(i, "Balanced", ret,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                    case TargetType.BalancedGoal:
                        {
                            var ret = new TimeSpan?[sb.Length];
                            if (this.records[this.category].Goal - ssb.Last() is TimeSpan span)
                            {
                                AverageTargetSet(sb, ret, span);
                            }
                            this.targetLabels[i] = MakeTargetLabels(i, "Goal", ret,
                                TargetLabels.TimeSpanFlag.Total,
                                TargetLabels.PrevColorFlag.None);
                        }
                        break;
                }
                this.targetLabels[i].TimeSet(0, this.running);
            }
        }

        private static void AverageTargetSet(TimeSpan?[] sb, TimeSpan?[] ret, TimeSpan span)
        {
            ret[0] = sb[0];
            foreach (var j in Utility.Range(1, sb.Length))
            {
                ret[j] = sb[j] + ret[j - 1];
            }
            foreach (var j in Utility.Range(0, sb.Length))
            {
                ret[j] = ret[j] + new TimeSpan(0, 0, 0, 0, ((int)span.TotalMilliseconds) * (j + 1) / sb.Length);
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
