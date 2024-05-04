using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day5
{
    public class SupplyStacks
    {
        public Stack<char>[] _stacks { get; }
        public SupplyStacks(int x)
        {
            _stacks = new Stack<char>[x];

            for (int i = 0; i < _stacks.Length; i++)
            {
                _stacks[i] = new Stack<char>();
            }
        }

        public void Add(int x, char toAdd) => _stacks[x].Push(toAdd);

        public char Remove(int x) => _stacks[x].Pop();

        public void ProcessInstruction(Instruction instruction)
        {
            AddFromQueue(RemoveAmount(instruction.Amount, instruction.From), instruction.To);
        }

        public void ProcessInstructionPart2(Instruction instruction)
        {
            AddFromStack(RemoveAmountToStack(instruction.Amount, instruction.From), instruction.To);
        }

        public string GetTop()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < _stacks.Length; i++)
            {
                if (_stacks[i].Any())
                    result.Append(_stacks[i].Pop());
            }

            return result.ToString();
        }

        private Queue<char> RemoveAmount(int amount, int x)
        {
            var queue = new Queue<char>();

            for (int i = 0; i < amount; i++)
                queue.Enqueue(_stacks[x].Pop());
            return queue;
        }

        private Stack<char> RemoveAmountToStack(int amount, int x)
        {
            var stack = new Stack<char>();

            for (int i = 0; i < amount; i++)
                stack.Push(_stacks[x].Pop());
            return stack;
        }

        private void AddFromQueue(Queue<char> queue, int x)
        {
            while(queue.Any())
            {
                _stacks[x].Push(queue.Dequeue());
            }
        }

        private void AddFromStack(Stack<char> stack, int x)
        {
            while(stack.Any())
            {
                _stacks[x].Push(stack.Pop());
            }
        }
    }
}
