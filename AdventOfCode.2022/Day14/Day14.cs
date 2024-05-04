using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AdventOfCode._2022.Day14
{
    internal class Day14 : IDay<int, int>
    {
        public string[] _input { get; }

        public Day14()
        {
            _input = File.ReadAllLines("Day14/input.txt");
        }
        public int Part1()
        {

            List<(int x, int y)> coordinates = new List<(int x, int y)>();
            Map map = new Map(600, 600);

            for (int i = 0; i < _input.Length; i++)
            {
                var line = _input[i].Replace(" ", "").Split("->");

                for (int j = 0; j < line.Length; j++)
                {
                    coordinates.Add((Int32.Parse(line[j].Split(',')[0]), Int32.Parse(line[j].Split(',')[1])));
                }
                map.AddWalls(coordinates);
                coordinates = new List<(int x, int y)>();
            }

            (int x, int y) sand = (500, 0);
            int count = 0;

            do
            {
                if (map.CanSandFall(sand))
                {
                    sand = map.GetNextCoordinate(sand);
                }
                else
                {
                    map.DrawSand(sand);
                    sand = (500, 0);
                    count++;
                }
            } while (!map.IsSandFallingIntoTheAbyss(sand));

            return count;
        }

        public int Part2()
        {

            List<(int x, int y)> coordinates = new List<(int x, int y)>();
            Map map = new Map(800, 600);
            int floor = 0;

            for (int i = 0; i < _input.Length; i++)
            {
                var line = _input[i].Replace(" ", "").Split("->");

                for (int j = 0; j < line.Length; j++)
                {
                    coordinates.Add((Int32.Parse(line[j].Split(',')[0]), Int32.Parse(line[j].Split(',')[1])));
                }
                map.AddWalls(coordinates);
                if (coordinates.Any(wall => wall.y > floor))
                    floor = coordinates.Where(wall => wall.y > floor).OrderByDescending(wall => wall).First().y;
                coordinates = new List<(int x, int y)>();
            }

            floor += 2;

            coordinates.Add((0, floor));
            coordinates.Add((799, floor));
            map.AddWalls(coordinates);
            bool reachedTop = false;

            (int x, int y) sand = (500, 0);
            int count = 0;

            do
            {
                if (map.IsSandFallingIntoTheAbyss(sand))
                    throw new Exception("uh oh, spaghettio");
                if (map.CanSandFall(sand))
                {
                    sand = map.GetNextCoordinate(sand);
                }
                else
                {
                    map.DrawSand(sand);
                    if (sand != (500,0))
                        sand = (500, 0);
                    else
                        reachedTop = true;
                    count++;
                }
            } while (!reachedTop);
            map.DrawMap();
            return count;
        }
    }
}
