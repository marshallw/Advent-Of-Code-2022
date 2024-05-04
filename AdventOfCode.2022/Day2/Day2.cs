using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day2
{
    public class Day2 : IDay<int, int>
    {
        List<string> _input { get; }
        public Day2()
        {
            _input = File.ReadAllLines("Day2/input.txt").ToList();
        }
        public int Part1() => _input.Select(x => x.Split(" ")).Sum(x => GetScore(x[0], x[1]));

        public int Part2() => _input.Select(x => x.Split(" ")).Sum(x => GetScore2(x[0], x[1]));

        private int GetScore2(string enemy, string result) => Score.GetScore(result).Value + HandScore(enemy, result);

        private int GetScore(string enemy, string player) => GameScore(enemy, player) + PlayerHand.GetScore(player).Score;

        // better solution would be to modulo then > comparison, but hard to think when sleep deprived.
        private int GameScore(string enemy, string player)
        {
            if (enemy == "A" && player == "Y" ||
                enemy == "B" && player == "Z" ||
                enemy == "C" && player == "X")
                return 6;
            if (enemy == "A" && player == "X" ||
                enemy == "B" && player == "Y" ||
                enemy == "C" && player == "Z")
                return 3;
            return 0;
        }

        private int HandScore(string enemy, string result)
        {
            if (enemy == "A" && result == "X" ||
                enemy == "C" && result == "Y" ||
                enemy == "B" && result == "Z")
                return 3;
            if (enemy == "A" && result == "Z" ||
                enemy == "B" && result == "Y" ||
                enemy == "C" && result == "X")
                return 2;
            return 1;
        }
    }
}
