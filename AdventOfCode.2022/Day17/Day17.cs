using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day17
{
    internal class Day17 : IDay<long, long>
    {
        public string[] _input { get; }
        public string[] _blocks { get; }
        public Day17()
        {
            _input = File.ReadAllLines("Day17/input.txt");
            _blocks = File.ReadAllLines("Day17/Rocks.txt");
        }
        public long Part1()
        {
            Queue<char> actions = new Queue<char>();
            PlayArea playArea = new PlayArea(7);
            foreach(char movement in _input[0])
            {
                actions.Enqueue(movement);
            }
            Queue<Sprite> sprites = new Queue<Sprite>(CreateBlocks(_blocks));


            int blockCount = 0;
            Block block = null;

            while(blockCount < 2022)
            {
                if (block == null || block.AtRest == true)
                {
                    var sprite = sprites.Dequeue();
                    block = new Block(2, playArea.GetNextHeight() + sprite.Y - 1, sprite);
                    sprites.Enqueue(sprite);
                }
                else
                {
                    char instruction = actions.Dequeue();
                    int distance = instruction == '<' ? -1 : 1;
                    if (playArea.BlockCanMoveHorizontally(block, distance))
                    {
                        block.X += distance;
                    }
                    actions.Enqueue(instruction);

                    if (playArea.BlockCanMoveVertically(block, -1))
                    {
                        block.Y -= 1;
                    }
                    else
                    {
                        playArea.AddBlock(block);
                        blockCount++;
                    }
                }
            }


            return playArea.GetMaxHeight();
        }

        Sprite[] CreateBlocks(string[] input)
        {
            Queue<string> queue = new Queue<string>(input);
            List<Sprite> blocks = new List<Sprite>();
            Sprite block = new Sprite();

            while(queue.Any())
            {
                var line = queue.Dequeue();
                if (line != "")
                    block.AddLine(line);
                else
                {
                    blocks.Add(block);
                    block = new Sprite();
                }
            }
            blocks.Add(block);
            return blocks.ToArray();
        }

        public long Part2()
        {
            Queue<char> actions = new Queue<char>();
            PlayArea playArea = new PlayArea(7);
            foreach (char movement in _input[0])
            {
                actions.Enqueue(movement);
            }
            Queue<Sprite> sprites = new Queue<Sprite>(CreateBlocks(_blocks));


            long blockCount = 0;
            Block block = null;
            bool fallDown = false;

            while (blockCount < 1000000000000)
            {
                if (block == null || block.AtRest == true)
                {
                    var sprite = sprites.Dequeue();
                    block = new Block(2, playArea.GetNextHeight() + sprite.Y - 1, sprite);
                    sprites.Enqueue(sprite);
                    fallDown = false;
                }
                else
                {
                    if (fallDown == false)
                    {
                        char instruction = actions.Dequeue();
                        int distance = instruction == '<' ? -1 : 1;
                        if (playArea.BlockCanMoveHorizontally(block, distance))
                        {
                            block.X += distance;
                        }
                        actions.Enqueue(instruction);
                        fallDown = true;
                    }
                    else
                    {
                        if (playArea.BlockCanMoveVertically(block, -1))
                        {
                            block.Y -= 1;
                        }
                        else
                        {
                            playArea.AddBlock(block);
                            blockCount++;
                        }
                        fallDown = false;
                    }
                }
            }


            return playArea.GetMaxHeight();
        }
    }
}
