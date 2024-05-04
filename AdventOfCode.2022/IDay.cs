using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022
{
    public interface IDay<TPart1, TPart2>
    {
        TPart1 Part1();
        TPart2 Part2();
    }
}
