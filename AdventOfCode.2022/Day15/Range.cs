using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day15
{
    internal class Range
    {
        public (int X, int X2) XRange { get; }
        public int Y { get; set; }

        public Range((int X, int X2) start, int y)
        {
            XRange = start;
            Y = y;
        }

        public List<(int x, int y)> GetCoordinatesOutsideBoundary(int start, int end)
        {
            List<(int x, int y)> result = new List<(int x, int y)>();

            if (start < XRange.X)
                for(int x = start; x <= XRange.X; x++)
                {
                    result.Add((x, Y));
                }
            if (end > XRange.X2)
                for (int x = XRange.X2; x <= end; x++)
                    result.Add((x, Y));
            return result;
        }

        public Range Combine(Range range)
        {
            if (range.Y != Y)
                throw new Exception("i'm not that smart.");

            return new Range((Math.Min(range.XRange.X, XRange.X), Math.Max(range.XRange.X2, XRange.X2)), Y);
        }

        public bool Overlap(Range range)
        {
            return range.XRange.X < XRange.X && range.XRange.X2 > XRange.X ||
                range.XRange.X2 > XRange.X2 && range.XRange.X < XRange.X2;
        }
    }
}
