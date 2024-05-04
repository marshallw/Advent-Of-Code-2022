using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day8
{
    public class Day8
    {
        public string[] _input { get; set; }
        public Day8()
        {
            _input = File.ReadAllLines("Day8/input.txt");
        }

        public int Part1()
        {
            TreeMap treeMap = new TreeMap(_input);

            return treeMap.GetVisible();
        }

        public int Part2()
        {
            TreeMap treeMap = new TreeMap(_input);
            return treeMap.GetHighestScenicScore();
        }
    }
}
