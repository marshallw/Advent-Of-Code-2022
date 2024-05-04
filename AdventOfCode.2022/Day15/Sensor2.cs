using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day15
{
    internal class Sensor2
    {
        public int X { get; }
        public int Y { get; }
        public int Range { get; }

        public Sensor2(int x, int y, int range)
        {
            X = x;
            Y = y;
            Range = range;
        }

        public bool IsWithinRange(int x, int y)
        {
            return Math.Abs(x - X) + Math.Abs(y - Y) <= Range;
        }
    }
}
