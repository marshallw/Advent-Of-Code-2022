using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day4
{
    public class Day4
    {
        private List<string> _input { get; }
        public Day4()
        {
            _input = File.ReadAllLines("Day4/input.txt").ToList();
        }

        public void Part1()
        {
            Console.WriteLine(
                _input.Select(x => 
                    x.Split(',')
                    .Select(CleaningDuties.Parse)
                    .ToArray())
                .Select(x => x[0].Contains(x[1]) || x[1].Contains(x[0]))
                .Count(x => x == true));
        }

        public void Part2()
        {
            Console.WriteLine(
                _input.Select(x =>
                    x.Split(',')
                    .Select(CleaningDuties.Parse)
                    .ToArray())
                .Select(x => x[0].OverlapsAtAll(x[1]) || x[1].OverlapsAtAll(x[0]))
                .Count(x => x == true));
        }
    }
}
