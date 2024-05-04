using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day5
{
    public class Instruction
    {
        public int From { get; }
        public int To { get; }
        public int Amount { get; }

        public Instruction(int from, int to, int amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }

        public static Instruction Create(string line)
        {
            var array = line.Split(' ');
            return new Instruction(Int32.Parse(array[3]) - 1, Int32.Parse(array[5]) - 1, Int32.Parse(array[1]));
        }
    }
}
