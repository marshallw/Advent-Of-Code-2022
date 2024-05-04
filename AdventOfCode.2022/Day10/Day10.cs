using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day10
{
    public class Day10
    {
        public string[] _input { get; }

        public Day10()
        {
            _input = File.ReadAllLines("Day10/input.txt");
        }

        public void Part1()
        {
            var instructions = new Stack<string[]>(_input.Select(line => line.Split(' ')).Reverse());
            var machine = new Machine();
            int result = 0;
            int round = 0;
            int[] CycleCheck = new int[] { 20, 60, 100, 140, 180, 220 };

            while(instructions.Any())
            {
                round++;
                if (machine.ReadyForInstruction())
                {
                    var instruction = instructions.Pop();
                    if (instruction.Count() > 1)
                        machine.EnterInstruction(instruction[0], Int32.Parse(instruction[1]));
                    else
                        machine.EnterInstruction(instruction[0]);
                }

                Console.WriteLine($"Round: {round} Cycle: {machine.Cycle} X: {machine.X}");

                if (CycleCheck.Contains(machine.Cycle))
                {
                    result += machine.Cycle * machine.X;
                    Console.WriteLine($"Cycle: {machine.Cycle} X: {machine.X} Multiplied: {machine.Cycle * machine.X}");
                }
                machine.Tick();
            }
            Console.WriteLine(result);
        }

        public void Part2()
        {
            var instructions = new Stack<string[]>(_input.Select(line => line.Split(' ')).Reverse());
            var machine = new Machine(40);

            while (instructions.Any())
            {
                if (machine.ReadyForInstruction())
                {
                    var instruction = instructions.Pop();
                    if (instruction.Count() > 1)
                        machine.EnterInstruction(instruction[0], Int32.Parse(instruction[1]));
                    else
                        machine.EnterInstruction(instruction[0]);
                }
               
                machine.Tick();
            }
        }
    }
}
