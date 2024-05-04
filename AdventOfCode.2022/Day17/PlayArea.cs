using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day17
{
    public class PlayArea
    {
        public List<Block> _blocks { get; set; }
        public int Width { get; }

        public PlayArea(int width)
        {
            Width = width;
            _blocks = new List<Block>();
        }

        public bool BlockCanMoveHorizontally(Block block, int x)
        {
            if (block.X + x < 0 || block.X + block.Sprite.X + x - 1 >= Width)
                return false;
            if (_blocks.Skip(Math.Max(0, _blocks.Count() - 40)).Take(40).Any(b => BlockWillIntersectBlock(block, b, x, 0)))
                return false;
            return true;
        }

        public bool BlockCanMoveVertically(Block block, long y)
        {
            if (block.Y + y < 0)
                return false;
            if (_blocks.Skip(Math.Max(0, _blocks.Count() - 40)).Take(40).Any(b => BlockWillIntersectBlock(block, b, 0, y)))
                return false;
            return true;
        }

        public long GetNextHeight()
        {
            return GetMaxHeight() + 3;
        }

        public void AddBlock(Block block)
        {
            _blocks.Add(block);
            block.AtRest = true;
        }

        public long GetMaxHeight()
        {
            return _blocks.Any() ? _blocks.OrderByDescending(block => block.Y).First().Y + 1 : 0;
        }

        public bool BlockWillIntersectBlock(Block block1, Block block2, int x, long y)
        {
            if (block1.X + x > block2.X + block2.Sprite.X - 1 || block1.Y + y < block2.Y - block2.Sprite.Y + 1 || 
                block1.X + block1.Sprite.X + x - 1 < block2.X || block1.Y - block1.Sprite.Y + y + 1 > block2.Y)
                return false;

            //for(int y2 = 0; y2 < block1.Sprite.Y; y2++)
            //{
            //    for(int x2 = 0; x2 < block1.Sprite.X; x2++)
            //    {
            //        if (block1.Sprite.Map[x2,y2] == '#')
            //        {
            //            for(int y3 = 0; y3 < block2.Sprite.Y; y3++)
            //            {
            //                for(int x3 = 0; x3 < block2.Sprite.X; x3++)
            //                {
            //                    if (block2.Sprite.Map[x3, y3] == '#' && x2 + x + block1.X == x3 + block2.X && -y2 + y + block1.Y == -y3 + block2.Y)
            //                        return true;
            //                }
            //            }
            //        }
            //    }
            //}

            for (long y2 =  0 ; y2 < block1.Sprite.Y; y2++)
            {
                for (int x2 = 0; x2 < block1.Sprite.X; x2++)
                {
                    if (block1.Sprite.Map[x2, y2] == '#')
                    {
                        for (long y3 =  0; y3 < block2.Sprite.Y; y3++)
                        {
                            for (int x3 = 0; x3 < block2.Sprite.X; x3++)
                            {
                                if (block2.Sprite.Map[x3, y3] == '#' && x2 + x + block1.X == x3 + block2.X && -y2 + y + block1.Y == -y3 + block2.Y)
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
