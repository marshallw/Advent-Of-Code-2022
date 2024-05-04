using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day12
{
    public class Day12 : IDay<int, int>
    {
        string[] _input { get; }

        public Day12()
        {
            _input = File.ReadAllLines("Day12/input.txt");
        }
        public int Part1()
        {
            Map map = new Map(_input);
            return map.Djikstra();
        }


        public int Part2()
        {
            Map map = new Map(_input);
            return map.FindLowestSteps();
        }
    }
}
