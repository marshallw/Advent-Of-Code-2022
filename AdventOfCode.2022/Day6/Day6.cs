using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day6
{
    public class Day6
    {
        private string _input { get; }

        public Day6()
        {
            _input = File.ReadAllLines("Day6/input.txt")[0];
        }

        public int Part1()
        {
            for (int start = 0; start < _input.Length; start++)
            {
                if (NextXAreUnique(_input, start, 4))
                    return start + 4;
            }
            return 0;
        }

        public int Part2()
        {
            for (int start = 0; start < _input.Length; start++)
            {
                if (NextXAreUnique(_input, start, 14))
                    return start + 14;
            }
            return 0;
        }



        private bool NextXAreUnique(string input, int start, int numUnique)
        {
            var toCompare = input.Substring(start, numUnique);
            return toCompare.Distinct().Count() == numUnique;
        }
    }
}
