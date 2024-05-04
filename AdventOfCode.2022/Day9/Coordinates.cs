using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day9
{
    public class Coordinates : IEquatable<Coordinates>, IEqualityComparer<Coordinates>
    {
        public int X { get; }
        public int Y { get; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinates? other)
        {
            return other?.X == X && other?.Y == Y;
        }

        public override bool Equals(object? obj)
        {
            var coord = (Coordinates)obj;
            return X == coord.X && Y == coord.Y;
        }

        public int CompareTo(Coordinates? other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Coordinates? x, Coordinates? y)
        {
            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode([DisallowNull] Coordinates obj)
        {
            return obj.X.GetHashCode() ^ obj.Y.GetHashCode();
        }
    }
}
