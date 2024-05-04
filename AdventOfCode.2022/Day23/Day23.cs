using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day23
{
    public class Day23 : IDay<long, long>
    {
        public string[] _input { get; }
        
        public Day23()
        {
            _input = File.ReadAllLines(@"Day23\input.txt");
        }
        public long Part1()
        {
            List<(int x, int y)> elves = new List<(int x, int y)>();
            int length = _input.Count();
            int width = _input[0].Count();
            char[,] map = new char[width, length];

            for (int y = 0; y < length; y++)
                for (int x = 0; x < width; x++)
                    if (_input[y][x] == '#')
                        elves.Add((x, y));
            ((int x, int y), int i)[] proposed;


            int direction = 0;
            int count = 0;
            do
            {
                count++;
                proposed = GetProposedCoordinates(elves.Select((x, i) => (x,i)).Where(elf => AnyElfAdjacent(elf.x, elves)).ToList(), direction);

                var canMove = proposed.GroupBy(x => x.Item1)
                        .Where(g => g.Count() == 1)
                        .Select(g => g.First()).ToList();

                foreach(var movement in canMove)
                {
                    if (movement.Item1.x != -100000 && movement.Item1.y != -100000)
                        elves[movement.i] = movement.Item1;
                }

                direction = (direction + 1) % 4;

            } while (proposed.Count() > 0 && count < 10);
            return ((elves.OrderByDescending(x => x.x).First().x - elves.OrderBy(x => x.x).First().x + 1) * (elves.OrderByDescending(x => x.y).First().y - elves.OrderBy(x => x.y).First().y + 1) - elves.Count());
            
        }

        public bool AnyElfAdjacent((int x, int y) elf, List<(int x, int y)> elves)
        {
            return elves.Any(elf2 => Math.Abs(elf2.x - elf.x) <= 1 && Math.Abs(elf2.y - elf.y) <= 1 && (elf.x != elf2.x || elf.y != elf2.y));
        }

        private ((int x, int y), int i)[] GetProposedCoordinates(List<((int x, int y) coord, int i)> elves, int direction)
        {
            ((int x, int y), int i)[] result = new ((int x, int y), int i)[elves.Count];

            for (int i = 0; i < elves.Count; i++)
            {
                result[i] = ((-100000, -100000), elves[i].i);
                var elf = elves[i];
                var originalDirection = direction;
                var currentDirection = direction;
                do
                {
                    if (currentDirection == 0)
                        if (!elves.Any(x => x.coord.x == elf.coord.x - 1 && x.coord.y == elf.coord.y - 1 ||
                                                        x.coord.x == elf.coord.x && x.coord.y == elf.coord.y - 1 ||
                                                        x.coord.x == elf.coord.x + 1 && x.coord.y == elf.coord.y - 1))
                        {
                            result[i].Item1 = (elf.coord.x, elf.coord.y - 1);
                        }

                    if (currentDirection == 1)
                        if (!elves.Any(x => x.coord.x == elf.coord.x - 1 && x.coord.y == elf.coord.y + 1 ||
                                                        x.coord.x == elf.coord.x && x.coord.y == elf.coord.y + 1 ||
                                                        x.coord.x == elf.coord.x + 1 && x.coord.y == elf.coord.y + 1))
                        {
                            result[i].Item1 = (elf.coord.x, elf.coord.y + 1);
                        }

                    if (currentDirection == 2)
                        if (!elves.Any(x => x.coord.x == elf.coord.x - 1 && x.coord.y == elf.coord.y - 1 ||
                                                        x.coord.x == elf.coord.x - 1 && x.coord.y == elf.coord.y ||
                                                        x.coord.x == elf.coord.x - 1 && x.coord.y == elf.coord.y + 1))
                        {
                            result[i].Item1 = (elf.coord.x - 1, elf.coord.y);
                        }

                    if (currentDirection == 3)
                        if (!elves.Any(x => x.coord.x == elf.coord.x + 1 && x.coord.y == elf.coord.y - 1 ||
                                                        x.coord.x == elf.coord.x + 1 && x.coord.y == elf.coord.y ||
                                                        x.coord.x == elf.coord.x + 1 && x.coord.y == elf.coord.y + 1))
                        {
                            result[i].Item1 = (elf.coord.x + 1, elf.coord.y);
                        }



                    if (result[i].Item1 == (-100000, -100000))
                        currentDirection = (currentDirection + 1) % 4;
                } while (result[i].Item1 == (-100000, -100000) && currentDirection != originalDirection);
            }
            return result;
        }

        public long Part2()
        {
            List<(int x, int y)> elves = new List<(int x, int y)>();
            int length = _input.Count();
            int width = _input[0].Count();
            char[,] map = new char[width, length];

            for (int y = 0; y < length; y++)
                for (int x = 0; x < width; x++)
                    if (_input[y][x] == '#')
                        elves.Add((x, y));
            ((int x, int y), int i)[] proposed;


            int direction = 0;
            int count = 0;
            do
            {
                count++;
                proposed = GetProposedCoordinates(elves.Select((x, i) => (x, i)).Where(elf => AnyElfAdjacent(elf.x, elves)).ToList(), direction);

                var canMove = proposed.GroupBy(x => x.Item1)
                        .Where(g => g.Count() == 1)
                        .Select(g => g.First()).ToList();

                foreach (var movement in canMove)
                {
                    if (movement.Item1.x != -100000 && movement.Item1.y != -100000)
                        elves[movement.i] = movement.Item1;
                }

                direction = (direction + 1) % 4;

            } while (proposed.Count() > 0);
            return count;

        }
    }
}
