using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day16
{
    public class Traveler : IEquatable<Traveler?>
    {
        public Node2 Current { get; set; }
        public Node2 Destination { get; set; }
        public int distance { get; set; }
        public bool Opened { get; set; }

        public Traveler(Node2 current, Node2 destination, int distance, bool opened)
        {
            Current = current;
            Destination = destination;
            this.distance = distance;
            Opened = opened;
        }

        public Traveler SetNewDestination(Node2 newDestination, int distance) => new Traveler(Destination, newDestination, distance, false);

        public Traveler Clone() => new Traveler(Current, Destination, distance, Opened);

        public override bool Equals(object? obj)
        {
            return Equals(obj as Traveler);
        }

        public bool Equals(Traveler? other)
        {
            return other is not null &&
                   EqualityComparer<Node2>.Default.Equals(Current, other.Current) &&
                   EqualityComparer<Node2>.Default.Equals(Destination, other.Destination) &&
                   distance == other.distance &&
                   Opened == other.Opened;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Current, Destination, distance, Opened);
        }
    }
}
