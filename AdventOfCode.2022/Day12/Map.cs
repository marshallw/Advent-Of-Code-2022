using System.Diagnostics;
using System.Text;

namespace AdventOfCode._2022.Day12
{
    public class Map
    {
        public int[,] _map { get; }
        public (int x, int y) start { get; set; }
        public (int x, int y) end {get; set;}
    
        public Map(string[] input)
        {
            (int x, int y) coordinates = GetCoordinates(input);
            _map = new int[coordinates.x, coordinates.y];

            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == 'S')
                    {
                        start = (x, y);
                        _map[x, y] = 97;
                    }
                    else if (input[y][x] == 'E')
                    {
                        end = (x, y);
                        _map[x, y] = 122;
                    }
                    else
                    {
                        _map[x, y] = input[y][x];
                    }
                }
            }
        }

        public int GetPath()
        {
            return GetPath(start, 97, new List<(int x, int y)> { }) - 1;
        }

        public int GetPath((int x, int y) current, int lastHeight, List<(int x, int y)> visited)
        {
            if (current.x < 0 || current.y < 0 || current.x >= _map.GetLength(0) || current.y >= _map.GetLength(1))
            {
                return 10000;
            }

            if (Math.Abs(_map[current.x, current.y] - lastHeight) > 1)
            {
                return 10000;
            }

            if (visited.Any(coordinates => coordinates == current))
            {
                return 10000;
            }

            if (current == end)
                return 1;

            if (visited.Count() > 500)
                return 10000;

            visited.Add(current);
            List<int> paths = new List<int>();
            var height = _map[current.x, current.y];

            paths.Add(GetPath((current.x, current.y - 1), height, visited.ToList()));
            paths.Add(GetPath((current.x, current.y + 1), height, visited.ToList()));
            paths.Add(GetPath((current.x - 1, current.y), height, visited.ToList()));
            paths.Add(GetPath((current.x + 1, current.y), height, visited.ToList()));

            return paths.OrderBy(x => x).First() + 1;

        }

        public int Djikstra()
        {
            List<(int x, int y)> visited = new List<(int x, int y)>();
            Node[,] djikstra = new Node[_map.GetLength(0), _map.GetLength(1)];
            List<Node> unvisited = new List<Node>();
            for (int y = 0; y < djikstra.GetLength(1); y++)
            {
                for (int x = 0; x < djikstra.GetLength(0); x++)
                {
                    var node = new Node(10000, false, x, y);
                    djikstra[x, y] = node;
                    unvisited.Add(node);
                }
            }

            djikstra[start.x, start.y].value = 0;

            while (!djikstra[end.x, end.y].visited && unvisited.Any())
            {
                var current = unvisited.Where(x => x.value < 10000).OrderBy(x => x.value).First();

                if (current.x == 47 && current.y == 18)
                    Debugger.Break();

                if (current.x - 1 >= 0 && djikstra[current.x - 1, current.y].value > current.value + 1 && IsValidHeightDiff(current, (current.x - 1, current.y)))
                {
                    djikstra[current.x - 1, current.y].value = current.value + 1;
                }
                if ( current.x + 1 < _map.GetLength(0) && djikstra[current.x +1, current.y].value > current.value + 1 && IsValidHeightDiff(current, (current.x + 1, current.y)))
                {
                    djikstra[current.x + 1, current.y].value = current.value + 1;
                }
                if (current.y + 1 < _map.GetLength(1) && djikstra[current.x, current.y + 1].value > current.value + 1 && IsValidHeightDiff(current, (current.x, current.y + 1)))
                {
                    djikstra[current.x, current.y + 1].value = current.value + 1;
                }
                if (current.y - 1 >= 0 && djikstra[current.x, current.y - 1].value > current.value + 1 && IsValidHeightDiff(current, (current.x, current.y - 1)))
                {
                    djikstra[current.x, current.y - 1].value = current.value + 1;
                }
                current.visited = true;
                unvisited.Remove(current);

            }
            return djikstra[end.x, end.y].value;
        }

        public int FindLowestSteps()
        {
            List<Node> aList = new List<Node>();
            Node[,] djikstra = new Node[_map.GetLength(0), _map.GetLength(1)];
            List<Node> unvisited = new List<Node>();
            for (int y = 0; y < djikstra.GetLength(1); y++)
            {
                for (int x = 0; x < djikstra.GetLength(0); x++)
                {
                    var node = new Node(10000, false, x, y);
                    djikstra[x, y] = node;
                    unvisited.Add(node);
                    if (_map[node.x, node.y] == 97)
                        aList.Add(node);
                }
            }

            djikstra[end.x, end.y].value = 0;

            while (unvisited.Any() && unvisited.Where(x => x.value < 10000).Any())
            {
                var current = unvisited.Where(x => x.value < 10000).OrderBy(x => x.value).First();

                if (current.x == 47 && current.y == 18)
                    Debugger.Break();

                if (current.x - 1 >= 0 && djikstra[current.x - 1, current.y].value > current.value + 1 && IsValidHeightDiff2(current, (current.x - 1, current.y)))
                {
                    djikstra[current.x - 1, current.y].value = current.value + 1;
                }
                if (current.x + 1 < _map.GetLength(0) && djikstra[current.x + 1, current.y].value > current.value + 1 && IsValidHeightDiff2(current, (current.x + 1, current.y)))
                {
                    djikstra[current.x + 1, current.y].value = current.value + 1;
                }
                if (current.y + 1 < _map.GetLength(1) && djikstra[current.x, current.y + 1].value > current.value + 1 && IsValidHeightDiff2(current, (current.x, current.y + 1)))
                {
                    djikstra[current.x, current.y + 1].value = current.value + 1;
                }
                if (current.y - 1 >= 0 && djikstra[current.x, current.y - 1].value > current.value + 1 && IsValidHeightDiff2(current, (current.x, current.y - 1)))
                {
                    djikstra[current.x, current.y - 1].value = current.value + 1;
                }
                current.visited = true;
                unvisited.Remove(current);

            }
            return aList.OrderBy(x => x.value).First().value;
        }


        private string PrintMap(Node[,] map)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    sb.Append(map[x, y].value + ", ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private bool IsValidHeightDiff(Node first, (int x, int y) second)
        {
            return (_map[second.x, second.y] - _map[first.x, first.y]) <= 1;
        }
        private bool IsValidHeightDiff2(Node first, (int x, int y) second)
        {
            return (_map[first.x, first.y] - _map[second.x, second.y]) <= 1;
        }

        private (int x, int y) GetCoordinates(string[] input)
        {
            return (input[0].Length, input.Length);
        }

    }
}
