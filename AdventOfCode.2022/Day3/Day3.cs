using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day3
{
    public class Day3 : IDay<int, int>
    {
        private List<string> _input;
        public Day3()
        {
            _input = File.ReadAllLines("Day3/input.txt").ToList();
        }

        public int Part1() => 
            _input.Select(x => new string[] {x.Substring(0,x.Length/2), x.Substring(x.Length/2, x.Length/2)})
                .Select(x => GetDuplicate2(x[0], x[1]))
                .Sum(x => ToScore(x));

        public int Part2() => 
            _input.Select((x, index) => (index / 3, x))
                .GroupBy(x => x.Item1)
                .Select(x => x.Select(x => x.x))
                .Select(x => GetDuplicate2(x.First(), x.Skip(1).First(), x.Skip(2).First()))//GetDuplicate(GetDuplicates(x.First(), x.Skip(1).First()), x.Skip(2).First()))
                .Sum(ToScore);

        private string GetDuplicate(string comp1, string comp2)
        {
            string duplicates = "";
            var queue1 = new Queue<char>(comp1);
            while (queue1.TryDequeue(out char letter))
            {
                if (comp2.Contains(letter))
                    return letter.ToString();
            }
            return duplicates;
        }

        // these two functions created on second try to attempt to make the code as unreadable as possible
        private char[] GetDuplicate2(string comp1, string comp2) => 
            comp1.Join(comp2, str1 => str1, str2 => str2, (str1, str2) => str1)
                .Distinct()
                .ToArray();

        private char[] GetDuplicate2(string comp1, string comp2, string comp3) =>
            comp1.Join(comp2, str1 => str1, str2 => str2, (str1, str2) => str1)
                .Join(comp3, str1 => str1, str2 => str2, (str1, str2) => str1)
                .Distinct()
                .ToArray();

        private string GetDuplicates(string comp1, string comp2)
        {
            string duplicates = "";
            var queue1 = new Queue<char>(comp1);

            while (queue1.Any())
            {
                char letter = queue1.Dequeue();
                if (comp2.Contains(letter))
                    duplicates += letter;
            }
            return duplicates;
        }

        private int ToScore(char[] duplicates)
        {
            var result = 0;
            foreach (var letter in duplicates)
            {
                if (char.IsLower(letter))
                    result += letter - 96;
                else
                    result += letter - 38;
            }
            return result;
        }
    }
}
