using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day24
{
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public List<Node> Nodes { get; }

        public Node(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
    }
}
