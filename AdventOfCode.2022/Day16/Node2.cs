using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day16
{
    public class Node2 : IEquatable<Node2?>
    {
        public List<(Node2 Node, int Weight)> Nodes { get; set; }
        public int Flow { get; }
        public string Id { get; }

        public Node2(string id, int flow)
        {
            Nodes = new List<(Node2, int)>();
            Flow = flow;
            Id = id;
        }

        private Node2(string id, int flow, List<(Node2, int)> nodes)
        {
            Nodes = nodes;
            Flow = flow;
            Id = id;
        }

        public Node2 Clone()
        {
            return new Node2(Id, Flow, Nodes);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Node2);
        }

        public bool Equals(Node2? other)
        {
            return other is not null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
