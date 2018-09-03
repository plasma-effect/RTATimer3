using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Timer
{
    [Serializable]class RouteRecord
    {
        public string RouteName { get; }
        public string[] SegmentName { get; }
        public List<TimeSpan?[]> Records { get; }

        public RouteRecord(string routename, string[] segname)
        {
            this.RouteName = routename;
            this.SegmentName = segname;
            this.Records = new List<TimeSpan?[]>();
        }

        public void AddRecord(TimeSpan?[] record)
        {
            var rec = record;
            if (record.Length < this.SegmentName.Length)
            {
                rec = new TimeSpan?[this.SegmentName.Length];
                foreach(var i in Utility.Range(0, record.Length))
                {
                    rec[i] = record[i];
                }
            }
            else if (record.Length > this.SegmentName.Length)
            {
                rec = record.Take(this.SegmentName.Length).ToArray();
            }
            this.Records.Add(rec);
        }

        public TimeSpan?[] RouteBest
        {
            get
            {
                var ret = new TimeSpan?[this.SegmentName.Length];
                foreach(var rec in this.Records)
                {
                    if (ret.Last() is TimeSpan sp)
                    {
                        if (sp > rec.Last())
                        {
                            ret = rec;
                        }
                    }
                    else if (rec.Last() is TimeSpan)
                    {
                        ret = rec;
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

    [Serializable]class CategoryRecord
    {
        public string CategoryName { get; }
        public List<RouteRecord> MyRecords { get; }
        public TimeSpan? Goal { set; get; }

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
                return this.MyRecords.Select(r => r.RouteBest.Last()).Min();
            }
        }

        public RouteRecord this[int index]
        {
            get
            {
                return this.MyRecords[index];
            }
        }
    }

    [Serializable]class GameRecord
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

        public CategoryRecord this[int index]
        {
            get
            {
                return this.CategoryRecords[index];
            }
        }

        public static GameRecord MakeTestData()
        {
            var data = new GameRecord("test");
            data[0].MyRecords.Add(new RouteRecord("test route", new string[] { "First", "Second", "Last" }));
            data[0][1].AddRecord(new TimeSpan?[]
            {
                new TimeSpan(0,0,0,1,0),
                new TimeSpan(0,0,0,2,500),
                new TimeSpan(0,0,0,5,000)
            });
            data[0][1].AddRecord(new TimeSpan?[]
            {
                new TimeSpan(0,0,0,2,000),
                new TimeSpan(0,0,0,3,500),
                new TimeSpan(0,0,0,4,500)
            });
            data[0].Goal = new TimeSpan(0, 0, 0, 4, 0);
            return data;
        }
    }
}
