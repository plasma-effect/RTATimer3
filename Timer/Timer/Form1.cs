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
using static Timer.Utility;

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
                if (record is null || index != record.Length)
                {
                    this.current.Text = SpanToString(this.target[index]);
                    SetNormalColorLabel(this.current);
                }
                else
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
        int segmentCount;
        DateTime start;
        string[] segmentNames;
        TimeSpan segprev;
        string path;

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
            this.path = Properties.Settings.Default.DefaultPath;
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
            
            this.targetLabels = new TargetLabels[4];
            this.records = RecordTest.MakeTestData("test", 3, 500, 3000, "First", "Second", "Last");//LoadFile(this.path);
            this.category = Clamp(Properties.Settings.Default.DefaultCategory, 0, this.records.CategoryCount - 1);
            this.route = Clamp(Properties.Settings.Default.DefaultRoute, 0, this.records[this.category].RouteCount - 1);
            ThreeUpdates();
        }

        /// <summary>
        /// 各種記録にアクセスする準備をする
        /// </summary>
        private void ReadyRecord()
        {
            this.Text = $"{this.records.GameName} - {this.records[this.category].CategoryName} [{this.records[this.category][this.route].RouteName}]";
            this.segmentCount = this.records[this.category][this.route].SegmentCount;
            this.segmentNames = this.records[this.category][this.route].SegmentName;
            this.goaltime = this.records[this.category].Goal;
            this.nowsegment = this.segmentCount;
            this.balanced = new TimeSpan?[this.segmentCount];
            if (this.mypb.Last() - this.myssb.Last() is TimeSpan bal)
            {
                AverageTargetSet(this.myssb, this.balanced, bal);
            }
            this.goal = new TimeSpan?[this.segmentCount];
            if (this.goaltime - this.myssb.Last() is TimeSpan gl)
            {
                AverageTargetSet(this.myssb, this.goal, gl);
            }
        }

        /// <summary>
        /// 下三行を更新する
        /// </summary>
        private void UpdatePB()
        {
            this.mysb = this.records[this.category][this.route].SegmentBests;
            this.myssb = CumSum(this.mysb);
            this.mypb = this.records[this.category][this.route].RouteBest;
            this.personalBest.Text = StrictSpanToString(this.records[this.category].PersonalBest);
            this.sumOfBestSegments.Text = StrictSpanToString(this.myssb.Last());
            this.bestPossibleTime.Text = StrictSpanToString(this.myssb.Last());
        }

        /// <summary>
        /// ターゲットの4行を更新する
        /// </summary>
        private void TargetSet()
        {
            this.currentSegmentLabel.Text = this.segmentNames[0];
            this.previousSegmentLabel.Text = "";
            this.previousTotal.Text = "-:--:--.-";
            this.previousSegment.Text = "-:--:--";
            this.targettypes = new TargetType[]
            {
                (TargetType)Properties.Settings.Default.Target0,
                (TargetType)Properties.Settings.Default.Target1,
                (TargetType)Properties.Settings.Default.Target2,
                (TargetType)Properties.Settings.Default.Target3
            };
            foreach (var i in Range(0, 4))
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
        }

        private static void AverageTargetSet(TimeSpan?[] ssb, TimeSpan?[] ret, TimeSpan span)
        {
            foreach (var j in Range(0, ssb.Length))
            {
                ret[j] = ssb[j] + new TimeSpan(0, 0, 0, 0, ((int)span.TotalMilliseconds) * (j + 1) / ssb.Length);
            }
        }

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
                MessageBox.Show("対応していないファイル形式です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(System.IO.FileNotFoundException exp)
            {
                MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return new GameRecord("default");
        }

        private bool SaveFile(string path)
        {
            try
            {
                using(var stream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    formatter.Serialize(stream, this.records);
                    return true;
                }
            }
            catch(UnauthorizedAccessException)
            {
                MessageBox.Show("ファイルの書き込みを拒否されました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    KeyEnterDown();
                    break;
                case Keys.Space:
                    KeySpaceDown();
                    break;
                case Keys.Back:
                    KeyBackDown();
                    break;
            }
        }

        private void KeyBackDown()
        {
            if (this.nowsegment != 0)
            {
                --this.nowsegment;
                UpdateTimeLabels(this.running[this.nowsegment]);
                this.running[this.nowsegment] = null;
            }
        }

        private void KeySpaceDown()
        {
            if (this.nowsegment != this.segmentCount)
            {
                Split(null);
            }
        }

        /// <summary>
        /// スプリット処理
        /// </summary>
        /// <param name="time">通過タイム(nullならスキップ)</param>
        private void Split(TimeSpan? time)
        {
            this.running[this.nowsegment] = time;
            ++this.nowsegment;
            UpdateTimeLabels(time);
        }

        private void UpdateTimeLabels(TimeSpan? time)
        {
            var runprev = this.nowsegment == 0 ? TimeSpan.Zero : this.running[this.nowsegment - 1];
            var ssbprev = this.nowsegment == 0 ? TimeSpan.Zero : this.myssb[this.nowsegment - 1];
            this.previousSegmentLabel.Text = this.nowsegment > 0 ? this.segmentNames[this.nowsegment - 1] : "";
            this.previousTotal.Text = this.nowsegment > 0 ? SpanToString(this.running[this.nowsegment - 1]) : "-:--:--.-";
            if (this.nowsegment >= 2)
            {
                this.previousSegment.Text = SpanToString(runprev - this.running[this.nowsegment - 2]);
            }
            else if (this.nowsegment == 1) 
            {
                this.previousSegment.Text = SpanToString(runprev);
            }
            else
            {
                this.previousSegment.Text = "-:--:--";
            }
            if (this.nowsegment == this.segmentCount)
            {
                this.bestPossibleTime.Text = "-:--:--.---";
            }
            else
            {
                this.currentSegmentLabel.Text = this.segmentNames[this.nowsegment];
                this.segprev = TimeSpan.Zero;
                this.segmentTimer.Text = time is null ? "-:--:--" : ShortSpanToString(time - runprev);
                this.bestPossibleTime.Text = StrictSpanToString(runprev - ssbprev + this.myssb.Last());
            }
            foreach (var t in this.targetLabels)
            {
                t.TimeSet(this.nowsegment, this.running);
            }
        }

        /// <summary>
        /// タイマースタート、スプリット、タイマーストップを行う
        /// </summary>
        private void KeyEnterDown()
        {
            if (this.nowsegment != this.segmentCount)
            {
                Split(this.stopwatch.Elapsed);
            }
            else
            {
                this.stopwatch.Restart();
                this.start = DateTime.Now;
                if (this.running is TimeSpan?[] run && !run.All(s => s is null)) 
                {
                    this.records[this.category][this.route].AddRecord(run, this.start);
                }
                this.running = new TimeSpan?[this.segmentCount];
                this.nowsegment = 0;
                UpdatePB();
                TargetSet();
            }
        }

        /// <summary>
        /// 20ms秒ごとに行われる更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            if (this.nowsegment != this.segmentCount)
            {
                var el = this.stopwatch.Elapsed;
                if (this.prev.Milliseconds / 100 != el.Milliseconds / 100)
                {
                    this.prev = el;
                    this.mainTimer.Text = SpanToString(this.prev);
                }
                if (this.nowsegment == 0)
                {
                    this.segmentTimer.Text = ShortSpanToString(this.prev);
                }
                else if(this.running[this.nowsegment-1] is TimeSpan pv)
                {
                    if (this.segprev.Seconds != (el - pv).Seconds) 
                    {
                        this.segprev = el - pv;
                        this.segmentTimer.Text = ShortSpanToString(this.segprev);
                    }
                }
                else
                {
                    this.segmentTimer.Text = "-:--:--";
                }
            }
        }

        /// <summary>
        /// 自己記録編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditRecordToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.stopwatch.Stop();
            if (this.running is TimeSpan?[] run && !run.All(s => s is null))
            {
                this.records[this.category][this.route].AddRecord(run, this.start);
                this.running = null;
            }
            var form = new RecordEditor
            {
                Record = this.records[this.category][this.route]
            };
            form.ShowDialog(this);
            this.records[this.category][this.route] = form.Record;
            ThreeUpdates();
        }

        /// <summary>
        /// ターゲット確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TargetCheckToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.stopwatch.Stop();
            if (this.running is TimeSpan?[] run && !run.All(s => s is null))
            {
                this.records[this.category][this.route].AddRecord(run, this.start);
                this.running = null;
            }
            var form = new TargetCheck();
            form.SetName(this.records[this.category][this.route].SegmentName);
            form.SetTime(0, this.mypb);
            form.SetTime(1, this.mysb);
            form.SetTime(2, this.myssb);
            form.SetTime(3, this.balanced);
            form.SetTime(4, this.goal);
            form.Target0 = Properties.Settings.Default.Target0;
            form.Target1 = Properties.Settings.Default.Target1;
            form.Target2 = Properties.Settings.Default.Target2;
            form.Target3 = Properties.Settings.Default.Target3;
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Properties.Settings.Default.Target0 = form.Target0;
                Properties.Settings.Default.Target1 = form.Target1;
                Properties.Settings.Default.Target2 = form.Target2;
                Properties.Settings.Default.Target3 = form.Target3;
                ThreeUpdates();
            }
        }

        /// <summary>
        /// 終了時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.running is TimeSpan?[] run && !run.All(s => s is null))
            {
                this.records[this.category][this.route].AddRecord(run, this.start);
            }
            SaveFile(this.path);
        }

        /// <summary>
        /// デフォルトに設定する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultSetToolStripMenuItemClick(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultPath = this.path;
            Properties.Settings.Default.DefaultRoute = this.route;
            Properties.Settings.Default.DefaultCategory = this.category;
        }

        /// <summary>
        /// 新ルート追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeNewRouteToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this.running is TimeSpan?[] run && !run.All(s => s is null))
            {
                this.records[this.category][this.route].AddRecord(run, this.start);
                this.running = null;
            }
            var form = new MakeNewRoute();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.route = this.records[this.category].AddRoute(new RouteRecord(form.RouteName, form.Route));
                ThreeUpdates();
            }
        }

        /// <summary>
        /// 重要な3つのアップデート処理
        /// </summary>
        private void ThreeUpdates()
        {
            UpdatePB();
            ReadyRecord();
            TargetSet();
        }

        /// <summary>
        /// カテゴリー追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeNewCategoryToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (this.running is TimeSpan?[] run && !run.All(s => s is null))
            {
                this.records[this.category][this.route].AddRecord(run, this.start);
                this.running = null;
            }
            var form = new MakeNewCategory();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.category = this.records.AddCategory(new CategoryRecord(form.CategoryName)
                {
                    Goal = form.Goal
                });
                this.records[this.category].AddRoute(new RouteRecord(form.RouteName, form.Route));
                this.route = 0;
            }
            ThreeUpdates();
        }

        private void NewRouteFromTextClick(object sender, EventArgs e)
        {
            var form = new MakeNewRouteFromText();
            if (form.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            var ofd = new OpenFileDialog
            {
                Filter = "txt file(*.txt)|*.txt|all file(*.*)|*.*"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using(var stream = new System.IO.StreamReader(ofd.FileName))
                {
                    var seg = stream.ReadLine().Split(',');
                    this.route = this.records[this.category].AddRoute(new RouteRecord(form.RouteName, seg));
                    while (stream.Peek() >= 0)
                    {
                        var rec = stream.ReadLine().Split(',').Select<string, TimeSpan?>(str =>
                         {
                             if (TimeSpan.TryParse(str, out var t))
                             {
                                 return t;
                             }
                             else
                             {
                                 return null;
                             }
                         }).ToArray();
                        if(!rec.All(s=>s is null))
                        {
                            this.records[this.category][this.route].AddRecord(rec, DateTime.Now);
                        }
                    }
                }
                ThreeUpdates();
            }
        }

        private void ChangeGoalTimeClick(object sender, EventArgs e)
        {
            var form = new GoalChange();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.records[this.category].Goal = form.NewGoal;
            }
            ThreeUpdates();
        }
    }
}
