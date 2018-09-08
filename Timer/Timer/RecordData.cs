﻿using System;
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
        public TimeSpan?[] Records { get; }
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
                var ret = new TimeSpan?[len];
                foreach(var rec in this.Records)
                {
                    if (ret.Last() is TimeSpan sp)
                    {
                        if (sp > rec[len - 1]) 
                        {
                            ret = rec.Records;
                        }
                    }
                    else if (rec[len] is TimeSpan)
                    {
                        ret = rec.Records;
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
        }

        public int AddRoute(RouteRecord route)
        {
            this.MyRecords.Add(route);
            return this.MyRecords.Count - 1;
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
                foreach(var k in Utility.Range(0, segnames.Length))
                {
                    rec[k] = new TimeSpan(0, 0, 0, 0, random.Next(1, maxspan / minspan + 1) * minspan);
                }
                ret[0][1].AddRecord(rec, DateTime.Now);
            }
            return ret;
        }
    }
}
