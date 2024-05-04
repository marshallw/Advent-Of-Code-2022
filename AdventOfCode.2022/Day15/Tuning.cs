using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day15
{
    public class Tuning
    {
        int X { get; }
        int Y { get; }
        int Frequency => X * 4000000 + Y;

        public Tuning(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
