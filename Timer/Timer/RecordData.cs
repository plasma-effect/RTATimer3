using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                foreach(var i in Enumerable.Range(0, record.Length))
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

        public TimeSpan? RouteBest
        {
            get
            {
                return this.Records.Select(r => r.Last()).Min();
            }
        }
    }

    [Serializable]class CategoryRecord
    {
        public string CategoryName { get; }
        public List<RouteRecord> MyRecords { get; }

        public CategoryRecord(string name)
        {
            this.CategoryName = name;
            this.MyRecords = new List<RouteRecord>();
        }

        public TimeSpan? PersonalBest
        {
            get
            {
                return this.MyRecords.Select(r => r.RouteBest).Min();
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
    }
}
