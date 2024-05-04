using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day17
{
    public class Block
    {
        public int X { get; set; }
        public long Y { get; set; }
        public Sprite Sprite { get; }
        public bool AtRest { get; set; }

        public Block(int x, long y, Sprite sprite)
        {
            X = x;
            Y = y;
            Sprite = sprite;
            AtRest = false;
        }
    }
}
