using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day20
{
    public class Day20 : IDay<int, long>
    {
        public long[] _input { get; }

        public Day20()
        {
            _input = File.ReadAllLines(@"Day20\input.txt").Select(x => long.Parse(x)).ToArray();
        }

        public int Part1()
        {
            var length = _input.Length;
            Node root = new Node(_input[0], 0, true);
            Node current = root;
            List<Node> nodes = new List<Node>();
            nodes.Add(root);

            for (int i = 1; i < length; i++)
            {
                var nextNode = new Node(_input[i], i, false);
                current.Next = nextNode;
                nextNode.Previous = current;
                current = nextNode;
                nodes.Add(nextNode);
            }
            root.Previous = current;
            current.Next = root;

            foreach(var node in nodes)
            {
                if (node.Value > 0)
                for(int i = 0; i < node.Value; i++)
                {
                        node.GoForward();
                }
                if (node.Value < 0)
                    for (int i = 0; i > node.Value; i--)
                        node.GoBackward();
            }

            Node next = root;
            int zeroCount = 0;
            bool beginCount = false;
            do
            {
                if (next.Value == 0 && beginCount == false)
                    beginCount = true;
                else if (beginCount)
                    zeroCount++;
                if (zeroCount == 1000 || zeroCount == 2000 || zeroCount == 3000)
                    Console.WriteLine(next.Value);
                next = next.Next;
            } while (zeroCount < 3000); 
            return 0;
        }

        public long TrueMod(long x, long m)
        {
            long result = x % m;
            return result < 0 ? result + m : result;
        }

        public long MyMod(long x, long m)
        {
            var abs = Math.Abs(x);
            var result = abs % m;
            return result;
        }

        public long TrueMod2(long x, long m)
        {
            return Convert.ToInt64(x - m * Math.Floor((double)x / m));
        }

        public void PrintNodes(List<Node> nodes)
        {
            var node = nodes.First(x => x.Root);
            Console.WriteLine("Current state of nodes:");
            for(int i = 0; i < 7; i++)
            {
                Console.Write($"{node.Value}, ");
                node = node.Next;
            }
            Console.WriteLine();
        }

        public long Part2()
        {
            var length = _input.Length;
            Node root = new Node(_input[0] * 811589153, 0, true);
            Node current = root;
            List<Node> nodes = new List<Node>();
            nodes.Add(root);

            for (int i = 1; i < length; i++)
            {
                var nextNode = new Node(_input[i] * 811589153, i, false);
                current.Next = nextNode;
                nextNode.Previous = current;
                current = nextNode;
                nodes.Add(nextNode);
            }
            root.Previous = current;
            current.Next = root;
            for (int k = 0; k < 10; k++)
            {
                for (var i = 0; i < nodes.Count(); i++)
                {
                    var mod = MyMod(nodes[i].Value, length - 1);
                    //PrintNodes(nodes);
                    //Console.WriteLine($"Value {nodes[i].Value} is moving {mod}");
                    if (nodes[i].Value > 0)
                        for (int j = 0; j < mod; j++)
                            nodes[i].GoForward();
                    else
                    {
                        for (int j = 0; j < mod; j++)
                            nodes[i].GoBackward();
                    }
                }
               // PrintNodes(nodes);
            }

            Node next = root;
            int zeroCount = 0;
            bool beginCount = false;
            long result = 0;
            do
            {
                if (next.Value == 0 && beginCount == false)
                    beginCount = true;
                else if (beginCount)
                    zeroCount++;
                if (zeroCount == 1000 || zeroCount == 2000 || zeroCount == 3000)
                    result += next.Value;
                    //Console.WriteLine(next.Value);
                next = next.Next;
            } while (zeroCount < 3000);
            return result;
        }
    }
}
