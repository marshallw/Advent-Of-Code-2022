using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day12
{
    public class Node
    {
        public int value { get; set; }
        public bool visited { get; set; }
        public int x { get; }
        public int y { get; }

        public Node(int _value, bool _visited, int x, int y)
        {
            value = _value;
            visited = _visited;
            this.x = x;
            this.y = y;
        }
    }
}
