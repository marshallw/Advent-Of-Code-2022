using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day17
{
    public class Sprite
    {
        public int X => Map.GetLength(0);
        public int Y => Map.GetLength(1);
        public char[,] Map { get; set; }

        public Sprite()
        {
            Map = new char[0, 0];
        }

        public void AddLine(string line)
        {
            int length = line.Count();
            int height = Map.GetLength(1) + 1;
            char[,] newSprite = new char[length, height];

            for(int y = 0; y < Map.GetLength(1); y++)
            {
                for(int x = 0; x < Map.GetLength(0); x++)
                {
                    newSprite[x, y] = Map[x, y];
                }
            }
            for (int x = 0; x < length; x++)
            {
                newSprite[x, height - 1] = line[x];
            }
            Map = newSprite;
        }
    }
}
