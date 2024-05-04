using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day9
{
    public class Instruction
    {
        private Direction _direction { get; }
        private int _distance { get; }

        public Instruction (Direction direction, int distance)
        {
            _direction = direction;
            _distance = distance;
        }

        public int Distance => _distance;
        public Direction Dir => _direction;

        public static Instruction Parse(char direction, int distance)
        {
            switch(direction)
            {
                case 'U': return new Instruction(Direction.Up(), distance);
                case 'D': return new Instruction(Direction.Down(), distance);
                case 'L': return new Instruction(Direction.Left(), distance);
                case 'R': return new Instruction(Direction.Right(), distance);
            }
            return new Instruction(Direction.Up(), distance);
        }
    }
}
