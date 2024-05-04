using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AdventOfCode._2022.Day14
{
    internal class Map
    {
        private int[,] _map { get; set; }
        public int X { get; }
        public int Y { get; }

        public Map(int x, int y)
        {
            X = x;
            Y = y;
            _map = new int[X, Y];

            for (int mapX = 0; mapX < X; mapX++)
                for (int mapY = 0; mapY < Y; mapY++)
                    _map[mapX, mapY] = 0;
        }

        public void AddWalls(List<(int x, int y)> coordinates)
        {
            Queue<(int x, int y)> queue = new Queue<(int x, int y)>(coordinates);

            (int x, int y) second = queue.Dequeue();

            while (queue.Any())
            {
                (int x, int y) first = second;
                second = queue.Dequeue();

                for (int y = Math.Min(first.y, second.y); y <= Math.Max(first.y, second.y); y++)
                    for (int x = Math.Min(first.x, second.x); x <= Math.Max(first.x, second.x); x++)
                    {
                        _map[x,y] = 1;
                    }
            }
        }

        public bool CanSandFall((int x, int y) sand)
        {
            if (_map[sand.x, sand.y + 1] == 0)
                return true;
            if (_map[sand.x - 1, sand.y + 1] == 0)
                return true;
            if (_map[sand.x + 1, sand.y + 1] == 0)
                return true;
            return false;
        }

        public bool IsSandFallingIntoTheAbyss((int x, int y) sand)
        {
            if (sand.y + 1 >= _map.GetLength(1))
                return true;
            if (_map[sand.x, sand.y + 1] == 0)
                return false;
            if (sand.x - 1 < 0)
                return true;
            if (_map[sand.x - 1, sand.y + 1] == 0)
                return false;
            if (sand.x + 1 >= _map.GetLength(0))
                return true;
            if (_map[sand.x + 1, sand.y + 1] == 0)
                return false;
            return false;
        }

        public (int x, int y) GetNextCoordinate((int x, int y) sand)
        {
            if (_map[sand.x, sand.y + 1] == 0)
                return (sand.x, sand.y + 1);
            if (_map[sand.x - 1, sand.y + 1] == 0)
                return (sand.x - 1, sand.y + 1);
            if (_map[sand.x + 1, sand.y + 1] == 0)
                return (sand.x + 1, sand.y + 1);
            return (sand.x, sand.y);
        }

        public void DrawSand((int x, int y) sand)
        {
            _map[sand.x, sand.y] = 2;
        }

        public void DrawMap()
        {
            var stream = new StreamWriter(@"c:\users\marshall.wilson\documents\filled.txt");
            string line = "";
            for (int y = 0; y < _map.GetLength(1); y++)
            {
                for (int x = 0; x < _map.GetLength(0); x++)
                {
                    
                    line += _map[x, y];
                }
                stream.WriteLine(line);
                line = "";
            }
            stream.Close();
        }
    }
}
