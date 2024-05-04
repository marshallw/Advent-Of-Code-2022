using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day9
{
    public class Direction : IEqualityComparer<Direction>, IEquatable<Direction>
    {
        private string _direction;
        public Direction(string direction)
        {
            _direction = direction;
        }
        public string Dir => _direction;

        public static Direction Up() => new Direction("Up");
        public static Direction Down() => new Direction("Down");
        public static Direction Left() => new Direction("Left");
        public static Direction Right() => new Direction("Right");

        public bool Equals(Direction? x, Direction? y)
        {
            return x?.Dir == y?.Dir;
        }

        public int GetHashCode([DisallowNull] Direction obj)
        {
            return obj.Dir.GetHashCode();
        }

        public bool Equals(Direction? other)
        {
            return Dir == other?.Dir;
        }

        public override bool Equals(object? obj)
        {
            return Equals((Direction)obj);
        }
    }
}
