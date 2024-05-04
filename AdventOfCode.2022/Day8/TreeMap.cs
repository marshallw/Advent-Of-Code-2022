using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day8
{
    public class TreeMap
    {
        public int[,] _map { get; }
        private int _x { get; }
        private int _y { get; }
        
        public TreeMap(string[] input)
        {
            _x = input[0].Length;
            _y = input.Length;
            _map = new int[_x, _y];
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    _map[x, y] = Int32.Parse(input[y][x].ToString());
                }
            }
            
        }

        public int GetVisible()
        {
            int result = 0;
            for (int y = 0; y < _y; y++)
            {
                for (int x = 0; x < _x; x++)
                {
                    if (IsVisible(x, y))
                        result++;
                }
            }
            return result;
        }

        private bool IsVisible(int x, int y)
        {
            int height = _map[x, y];
            bool result = true;

            for (int i = 0; i < x; i++)
            {
                if (_map[i, y] >= height)
                {
                    result = false;
                }
            }
            if (result)
                return result;
            result = true;

            for (int i = x + 1; i < _x; i++)
            {
                if (_map[i, y] >= height)
                {
                    result = false;
                }
            }
            if (result)
                return result;
            result = true;

            for (int i = 0; i < y; i++)
            {
                if (_map[x, i] >= height)
                {
                    result = false;
                }
            }
            if (result)
                return result;
            result = true;

            for (int i = y + 1; i < _y; i++)
            {
                if (_map[x, i] >= height)
                {
                    result = false;
                }
            }
            return result;

        }

        public int GetHighestScenicScore()
        {
            List<int> scores = new List<int>();
            for (int y = 0; y < _y; y++)
            {
                for (int x = 0; x < _x; x++)
                {
                    scores.Add(GetScenicScore(x, y));
                }
            }
            return scores.OrderByDescending(x => x).First();
        }

        private int GetScenicScore(int x, int y)
        {
            int height = _map[x, y];
            bool result = true;
            int scenicScore = 0;
            int temp = 0;

            for (int i = x - 1; i >= 0; i--)
            {
                scenicScore++;
                if (_map[i, y] >= height)
                {
                    break;
                }
            }


            for (int i = x + 1; i < _x; i++)
            {
                temp++;
                if (_map[i, y] >= height)
                {
                    break;
                }
            }
            scenicScore = scenicScore * temp;

            temp = 0;
            for (int i = y - 1; i >= 0; i--)
            {
                temp++;
                if (_map[x, i] >= height)
                {
                    break;
                }
            }
            scenicScore = scenicScore * temp;

            temp = 0;
            for (int i = y + 1; i < _y; i++)
            {
                temp++;
                if (_map[x, i] >= height)
                {
                    break;
                }
            }
            scenicScore = scenicScore * temp;

            return scenicScore;
        }

    }
}
