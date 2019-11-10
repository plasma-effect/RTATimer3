using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Timer
{
    /// <summary>
    /// 個別の記録のクラス
    /// </summary>
    [Serializable]public class RecordElement
    {
        public TimeSpan?[] Records { get; set; }
        public DateTime PlayDateTime { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="records">各区間の通過タイム</param>
        /// <param name="playDateTime">開始時間</param>
        public RecordElement(TimeSpan?[] records, DateTime playDateTime)
        {
            this.Records = records;
            this.PlayDateTime = playDateTime;
        }
        
        /// <summary>
        /// 区間の通過タイムを取る
        /// </summary>
        /// <param name="index">取りたい区間の番号</param>
        /// <returns>タイム(nullならスキップ)</returns>
        public TimeSpan? this[int index]
        {
            get
            {
                return this.Records[index];
            }
            set
            {
                this.Records[index] = value;
            }
        }

        public void RestoreTimes(TimeSpan?[] records)
        {
            this.Records = records;
        }
    }

    /// <summary>
    /// 一つのルートの各区間の名前とそのルートの記録のクラス
    /// </summary>
    [Serializable]public class RouteRecord
    {
        public string RouteName { get; }
        public string[] SegmentName { get; }
        public List<RecordElement> Records { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="routename">ルートの名前</param>
        /// <param name="segname">各区間の名前</param>
        public RouteRecord(string routename, string[] segname)
        {
            this.RouteName = routename;
            this.SegmentName = segname;
            this.Records = new List<RecordElement>();
        }
        
        /// <summary>
        /// 記録を追加する
        /// </summary>
        /// <param name="record">各区間の通過タイム</param>
        /// <param name="datetime">開始時間</param>
        /// <returns>追加した記録のindex</returns>
        public int AddRecord(TimeSpan?[] record, DateTime datetime)
        {
            var rec = record;
            if (record.Length != this.SegmentName.Length)
            {
                rec = new TimeSpan?[this.SegmentName.Length];
                foreach(var i in Utility.Range(0, Math.Min(record.Length,this.SegmentName.Length)))
                {
                    rec[i] = record[i];
                }
            }
            this.Records.Add(new RecordElement(record, datetime));
            return this.Records.Count - 1;
        }
        
        public TimeSpan?[] RouteBest
        {
            get
            {
                var len = this.SegmentName.Length;
                var maxlen = 0;
                var ret = new TimeSpan?[len];
                foreach (var rec in this.Records)
                {
                    if (ret[len - 1] is TimeSpan sp)
                    {
                        if (rec[len - 1] < ret[len - 1])
                        {
                            ret = rec.Records;
                        }
                    }
                    else
                    {
                        var size = 0;
                        foreach(var i in Enumerable.Range(0, len))
                        {
                            if(rec[i] is TimeSpan)
                            {
                                size = i + 1;
                            }
                        }
                        if (maxlen < size)
                        {
                            ret = rec.Records;
                            maxlen = size;
                        }
                        else if (maxlen == size && rec[size - 1] < ret[size - 1])
                        {
                            ret = rec.Records;
                        }
                    }
                }
                return ret;
            }
        }

        public TimeSpan?[] SegmentBests
        {
            get
            {
                var ret = new TimeSpan?[this.SegmentName.Length];
                foreach(var rec in this.Records)
                {
                    ret[0] = Utility.Min(ret[0], rec[0]);
                    foreach(var i in Utility.Range(1, this.SegmentName.Length))
                    {
                        ret[i] = Utility.Min(ret[i], rec[i] - rec[i - 1]);
                    }
                }
                return ret;
            }
        }

        public TimeSpan?[] SumOfBestSegments
        {
            get
            {
                var ret = new TimeSpan?[this.SegmentName.Length];
                foreach(var i in Utility.Range(0, this.SegmentName.Length))
                {
                    foreach(var rec in this.Records)
                    {
                        if (!rec[i].HasValue)
                        {
                            continue;
                        }
                        var prev = 1;
                        for (var j = i - 1; j >= 0; --j, ++prev)
                        {
                            if (rec[j].HasValue)
                            {
                                break;
                            }
                        }
                        if (prev > i)
                        {
                            ret[i] = Utility.Min(ret[i], rec[i]);
                        }
                        else
                        {
                            ret[i] = Utility.Min(ret[i], rec[i] - rec[i - prev] + ret[i - prev]);
                        }
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// 記録の数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return this.Records.Count;
            }
        }

        /// <summary>
        /// 区間の数
        /// </summary>
        public int SegmentCount
        {
            get
            {
                return this.SegmentName.Length;
            }
        }

        /// <summary>
        /// 個別の記録
        /// </summary>
        /// <param name="index">番号</param>
        /// <returns>index番目の記録を返す</returns>
        public RecordElement this[int index]
        {
            get
            {
                return this.Records[index];
            }
            set
            {
                this.Records[index] = value;
            }
        }
    }

    /// <summary>
    /// カテゴリの名前とそのカテゴリの記録のクラス
    /// </summary>
    [Serializable]public class CategoryRecord
    {
        public string CategoryName { get; }
        public List<RouteRecord> MyRecords { get; }
        public TimeSpan? Goal { set; get; }

        /// <summary>
        /// カテゴリ名
        /// </summary>
        /// <param name="name"></param>
        public CategoryRecord(string name)
        {
            this.CategoryName = name;
            this.MyRecords = new List<RouteRecord>();
            this.Goal = null;
        }

        public TimeSpan? PersonalBest
        {
            get
            {
                return this.MyRecords.Select(r => r.RouteBest?.Last()).Min();
            }
        }

        /// <summary>
        /// index番目のRouteRecordを返す
        /// </summary>
        /// <param name="index">番号</param>
        /// <returns>index番目のRoute</returns>
        public RouteRecord this[int index]
        {
            get
            {
                return this.MyRecords[index];
            }
            set
            {
                this.MyRecords[index] = value;
            }
        }

        /// <summary>
        /// ルート追加
        /// </summary>
        /// <param name="route">追加するルート</param>
        /// <returns>追加したルートのindex</returns>
        public int AddRoute(RouteRecord route)
        {
            this.MyRecords.Add(route);
            return this.MyRecords.Count - 1;
        }

        public int RouteCount
        {
            get
            {
                return this.MyRecords.Count;
            }
        }

    }

    /// <summary>
    /// ゲーム名とそのゲームに関する記録のクラス
    /// </summary>
    [Serializable]public class GameRecord
    {
        public string GameName { get; }
        public List<CategoryRecord> CategoryRecords { get; }

        public GameRecord(string gamename)
        {
            this.GameName = gamename;
            this.CategoryRecords = new List<CategoryRecord>
            {
                new CategoryRecord("Any%")
            };
            this.CategoryRecords.Last().MyRecords.Add(new RouteRecord("default", new string[] { "Clear" }));
        }

        /// <summary>
        /// index番目のカテゴリを返す
        /// </summary>
        /// <param name="index">番号</param>
        /// <returns>カテゴリ</returns>
        public CategoryRecord this[int index]
        {
            get
            {
                return this.CategoryRecords[index];
            }
        }

        public int AddCategory(CategoryRecord category)
        {
            this.CategoryRecords.Add(category);
            return this.CategoryRecords.Count - 1;
        }

        public int CategoryCount
        {
            get
            {
                return this.CategoryRecords.Count;
            }
        }
    }
    
    public class RecordTest
    {   
        public static GameRecord MakeTestData(string gamename, int size, int minspan,int maxspan, params string[] segnames)
        {
            var random = new Random();
            var ret = new GameRecord(gamename);
            ret[0].AddRoute(new RouteRecord("test", segnames));
            foreach(var i in Utility.Range(0, size))
            {
                var rec = new TimeSpan?[segnames.Length];
                var prev = 0;
                foreach(var k in Utility.Range(0, segnames.Length))
                {
                    prev += random.Next(1, maxspan / minspan + 1) * minspan;
                    rec[k] = new TimeSpan(0, 0, 0, 0, prev);
                }
                ret[0][1].AddRecord(rec, DateTime.Now);
            }
            return ret;
        }
    }
}
