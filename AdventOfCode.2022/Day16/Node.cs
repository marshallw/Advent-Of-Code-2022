using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day16
{
    public class Node
    {
        public List<Node> Nodes { get; set; }
        public int Flow { get; }
        public string Id { get; }
        public bool Visited { get; set; }
        public int Distance { get; set; }

        public Node(string id, int flow)
        {
            Nodes = new List<Node>();
            Flow = flow;
            Id = id;
            Visited = false;
            Distance = -10;
        }

        private Node(string id, int flow, List<Node> nodes)
        {
            Nodes = nodes;
            Flow = flow;
            Id = id;
            Visited = false;
            Distance = 100000;
        }

        public Node Clone()
        {
            return new Node(Id, Flow, Nodes);
        }
    }
}
