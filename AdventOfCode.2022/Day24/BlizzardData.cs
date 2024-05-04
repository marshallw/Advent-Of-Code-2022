using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day24
{
    public class BlizzardData
    {
        public List<(int x, int y, char direction)> CurrentState { get; set; }
        public int X { get; }
        public int Y { get; }
        public int Frame { get; private set; }
        public int CurrentSize { get; private set; }
        public List<Node> AllNodes { get; private set; }
        public Node Start { get; private set; }
        public (int x, int y) End { get; set; }

        public BlizzardData(int x, int y, List<(int x, int y, char direction)> blizzard)
        {
            CurrentState = blizzard;
            X = x;
            Y = y;
            Frame = 0;
            AllNodes = new List<Node>();
            Start = new Node(0, -1, 0);
            AllNodes.Add(Start);
            End = (x-1, y-1);
        }

        public BlizzardData(int x, int y, List<(int x, int y, char direction)> blizzard, Node start) : this(x, y, blizzard)
        {
            AllNodes = new List<Node>();
            AllNodes.Add(start);
            Start = start;
        }

        private BlizzardData(int x, int y, List<(int x, int y, char direction)> blizzard, int frame) : this(x, y, blizzard)
        {
            Frame = frame;

        }

        public bool WillIntersectWithBlizzard((int x, int y) coord)
        {
            return CurrentState.Any(blizzard => blizzard.x == coord.x && blizzard.y == coord.y);
        }

        private List<Node> CreateNextSetOfNodes(List<Node> lastSetOfNodes)
        { 
            CurrentSize++;
            List<Node> nodes = new List<Node>();

            for (int y = 0; y < Y; y++)
                for (int x = 0; x < X; x++)
                {
                    if (!CurrentState.Any(blizzard => blizzard.x == x && blizzard.y == y))
                    {
                        var newNode = new Node(x, y, CurrentSize);

                        var adjacentNodes = lastSetOfNodes.Where(node => node.Z == CurrentSize - 1 && 
                            (((Math.Abs(Math.Abs(node.X) - x) <= 1 && y - node.Y == 0) || ((y - node.Y >= -1 && y - node.Y <= 1) && Math.Abs(Math.Abs(node.X) - x) == 0)))).ToList();
                        adjacentNodes.ForEach(node => node.AddNode(newNode));
                        if (adjacentNodes.Count > 0)
                        {
                            AllNodes.Add(newNode);
                            nodes.Add(newNode);
                        }
                    }
                }

                var n = new Node(Start.X, Start.Y, CurrentSize);
                AllNodes.Where(x => x.X == Start.X && x.Y == Start.Y && x.Z == CurrentSize - 1).Single().AddNode(n);
                AllNodes.Add(n);
                nodes.Add(n);

            return nodes;
        }

        public void CreateToExit()
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(Start);
            do
            {
                MoveBlizzards();
                nodes = CreateNextSetOfNodes(nodes);
            } while (!AllNodes.Any(node => node.X == End.x && node.Y == (End.y)));
        }

        public void MoveBlizzards() => CurrentState = CurrentState.Select(coord => GetNextCoord(coord.x, coord.y, coord.direction)).ToList();

        public int GetShortestPath()
        {
            Dictionary<Node, int> distance = new Dictionary<Node, int>();
            Dictionary<Node, bool> visited = new Dictionary<Node, bool>();
            AllNodes.ForEach(node => distance.Add(node, 100000));
            AllNodes.ForEach(node => visited.Add(node, false));
            distance[Start] = 0;
            Node destination = AllNodes.Where(node => node.X == End.x && node.Y == End.y).First();

            do
            {
                var current = distance.Where(x => visited.Any(y => x.Key == y.Key && y.Value == false)).OrderBy(x => x.Value).FirstOrDefault();

                foreach(var node in current.Key.Nodes)
                {
                    if (current.Value + 1 < distance[node])
                        distance[node] = current.Value + 1;
                }
                visited[current.Key] = true;

            } while (distance[destination] == 100000);
            return distance[destination];
        }

        private (int x, int y, char direction) GetNextCoord(int x, int y, char direction)
        {
            switch (direction)
            {
                case '^':
                    y--;
                    break;
                case '>':
                    x++;
                    break;
                case 'v':
                    y++;
                    break;
                case '<':
                    x--;
                    break;
            }

            if (x < 0)
                x = X - 1;
            if (x >= X)
                x = 0;
            if (y < 0)
                y = Y - 1;
            if (y >= Y)
                y = 0;

            return (x, y, direction);
        }
    }
}
