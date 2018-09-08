using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Timer
{
    public static class Utility
    {
        public static void LabelColorSet(Label label, Color forecolor, Color backcolor)
        {
            label.ForeColor = forecolor;
            label.BackColor = backcolor;
        }

        public static void SetNormalColorLabel(Label label)
        {
            LabelColorSet(label, Color.Black, SystemColors.Control);
        }

        public static void SetRedColorLabel(Label label)
        {
            LabelColorSet(label, Color.White, Color.Red);
        }

        public static void SetGreenColorLabel(Label label)
        {
            LabelColorSet(label, Color.White, Color.Green);
        }

        public static TimeSpan? Min(TimeSpan? a,TimeSpan? b)
        {
            if(a is TimeSpan sa)
            {
                if(b is TimeSpan sb)
                {
                    return sa < sb ? sa : sb;
                }
                else
                {
                    return a;
                }
            }
            else
            {
                return b;
            }
        }

        public static IEnumerable<int> Range(int start, int end)
        {
            foreach(var i in Enumerable.Range(start, end - start))
            {
                yield return i;
            }
        }

        const string ft = @"h\:mm\:ss";
        const string fmt = @"h\:mm\:ss\.f";
        const string sfmt = @"h\:mm\:ss\.fff";
        public static string SpanToString(TimeSpan? span)
        {
            return span?.ToString(fmt) ?? "-:--:--.-";
        }

        public static string StrictSpanToString(TimeSpan? span)
        {
            return span?.ToString(sfmt) ?? "-:--:--.---";
        }

        public static string ShortSpanToString(TimeSpan? span)
        {
            return span?.ToString(ft) ?? "-:--:--";
        }

        public static TimeSpan?[] CumSum(TimeSpan?[] ar)
        {
            var ret = new TimeSpan?[ar.Length];
            ret[0] = ar[0];
            foreach(var i in Range(1, ar.Length))
            {
                ret[i] = ret[i - 1] + ar[i];
            }
            return ret;
        }

        public static bool IsAnyOfParams<T>(T val, params T[] ts)
        {
            foreach(var t in ts)
            {
                if (val.Equals(t))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<(T value, int index)> Indexed<T>(this IEnumerable<T> rng)
        {
            var index = -1;
            foreach(var v in rng)
            {
                yield return (v, ++index);
            }
        }

        public static T Clamp<T>(T val, T min, T max)
            where T : IComparable
        {
            if (val.CompareTo(min) < 0)
            {
                val = min;
            }
            if (max.CompareTo(val) < 0)
            {
                val = max;
            }
            return val;
        }
    }
}