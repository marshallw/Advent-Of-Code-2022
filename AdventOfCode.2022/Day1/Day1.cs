using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day1
{
    public class Day1 : IDay<int, int>
    {
        List<string> _input { get; }
        public Day1()
        {
            _input = File.ReadAllLines("Day1/input.txt").Select(line => line).ToList();
        }

        // this and Part2 admittedly written later as a second attempt.  GetMostCalories was my first, written 
        public int Part1() =>
            _input.Select((calorie, index) => (index, calorie))
                .Where(calories => calories.calorie != "")
                .GroupBy(calorie => _input.GetRange(0, calorie.index).Count(x => x == ""))
                .Select(calories => calories.Sum(calories => Int32.Parse(calories.calorie)))
                .OrderByDescending(calories => calories)
                .First();

        public int Part2() => 
            _input.Select((calorie, index) => (index, calorie))
                .Where(calories => calories.calorie != "")
                .GroupBy(calorie => _input.GetRange(0, calorie.index).Count(x => x == ""))
                .Select(calories => calories.Sum(calories => Int32.Parse(calories.calorie)))
                .OrderByDescending(calories => calories)
                .Take(3)
                .Sum(x => x);

        public static void GetMostCalories()
        {
            List<string> input = File.ReadAllLines("Day1/input.txt").ToList();

            int total = 0;
            int current = 0;
            List<int> calories = new List<int>();
            foreach (string line in input)
            {
                if (Int32.TryParse(line, out current))
                {
                    total += current;
                }
                else
                {
                    calories.Add(total);
                    total = 0;
                    current = 0;
                }
            }

            var ordered = calories.OrderByDescending(x => x);

            Console.WriteLine(ordered.First() + ordered.Skip(1).First() + ordered.Skip(2).First());

            
        }
    }
}
