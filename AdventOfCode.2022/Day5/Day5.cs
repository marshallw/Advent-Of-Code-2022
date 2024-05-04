using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day5
{
    public class Day5
    {
        private List<string> _input { get; }
        public Day5()
        {
            _input = File.ReadAllLines("Day5/input.txt").ToList();
        }

        public string Part1()
        {
            var stacks = SupplyStacksFactory.Create(_input.ToArray());
            _input.Reverse();
            var stack = new Stack<string>(_input);

            while (stack.Peek().Length == 0 || stack.Peek()[0] != 'm')
            {
                stack.Pop();
            }

            while(stack.Any())
            {
                stacks.ProcessInstruction(Instruction.Create(stack.Pop()));
            }
            return stacks.GetTop();
        }

        public string Part2()
        {
            var stacks = SupplyStacksFactory.Create(_input.ToArray());
            _input.Reverse();
            var stack = new Stack<string>(_input);

            while (stack.Peek().Length == 0 || stack.Peek()[0] != 'm')
            {
                stack.Pop();
            }

            while (stack.Any())
            {
                stacks.ProcessInstructionPart2(Instruction.Create(stack.Pop()));
            }
            return stacks.GetTop();
        }
    }
}
