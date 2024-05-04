using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day9
{

    public class Day9
    {
        public string[] _input { get; }
        public Day9()
        {
            _input = File.ReadAllLines("Day9/input.txt");
        }

        public int Part1()
        {
            Knot head = null;
            Knot tail = null;

            (head, tail) = KnotFactory.CreateRope(2);

            var instructions = _input.Select(line => line.Split(' ')).Select(instruction => Instruction.Parse(instruction[0].ToCharArray()[0], Int32.Parse(instruction[1])));

            foreach (var instruction in instructions)
            {
                head.FollowInstruction(instruction);
                tail.QueueChase();
            }
            return tail.GetNumTraveled();
        }

        public int Part2()
        {
            Knot[] knots = KnotFactory.CreateRopeArray(10);

            var instructions = _input.Select(line => line.Split(' ')).Select(instruction => Instruction.Parse(instruction[0].ToCharArray()[0], Int32.Parse(instruction[1])));

            foreach (var instruction in instructions)
            {
                for (int i = 0; i < instruction.Distance; i++)
                {
                    knots[0].FollowInstructionSlowly(instruction.Dir);
                    knots[9].QueueChase();
                }
            }
            return knots[9].GetNumTraveled();

        }
    }
}
