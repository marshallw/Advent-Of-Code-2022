using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day24
{
    internal class Day24 : IDay<long, long>
    {
        public string[] _input { get; }

        public Day24()
        {
            _input = File.ReadAllLines(@"Day24\input.txt");
        }
        public long Part1()
        {
            int width = _input[0].Length - 2;
            int height = _input.Length - 2;
            (int x, int y) start = (1, 0);
            (int x, int y) end = (width - 2, height - 1);
            List<(int x, int y, char direction)> blizzards = new List<(int x, int y, char direction)>();

            for (int y = 0; y < _input.Length; y++)
                for (int x = 0; x < _input[0].Length; x++)
                {
                    var tile = _input[y][x];
                    if (tile == '>' || tile == '<' || tile == 'v' || tile == '^')
                        blizzards.Add((x - 1, y - 1, tile));
                }

            BlizzardData blizzard = new BlizzardData(width, height, blizzards);
            blizzard.CreateToExit();
            return blizzard.CurrentSize + 1;
        }



        public long Part2()
        {
            int width = _input[0].Length - 2;
            int height = _input.Length - 2;
            (int x, int y) start = (1, 0);
            (int x, int y) end = (width - 2, height - 1);
            List<(int x, int y, char direction)> blizzards = new List<(int x, int y, char direction)>();

            for (int y = 0; y < _input.Length; y++)
                for (int x = 0; x < _input[0].Length; x++)
                {
                    var tile = _input[y][x];
                    if (tile == '>' || tile == '<' || tile == 'v' || tile == '^')
                        blizzards.Add((x - 1, y - 1, tile));
                }

            int currentLength = 0;

            BlizzardData blizzard = new BlizzardData(width, height, blizzards);
            blizzard.CreateToExit();
            currentLength = blizzard.CurrentSize + 1;
            blizzard.MoveBlizzards();
            
            blizzard = new BlizzardData(width, height, blizzard.CurrentState, new Node(width - 1, height, 0));
            blizzard.End = (0, 0);
            blizzard.CreateToExit();
            currentLength += blizzard.CurrentSize + 1;
            blizzard.MoveBlizzards();

            blizzard = new BlizzardData(width, height, blizzard.CurrentState);
            blizzard.CreateToExit();
            currentLength += blizzard.CurrentSize + 1;

            return currentLength;
        }
    }
}
