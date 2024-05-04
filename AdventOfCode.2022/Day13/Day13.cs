using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day13
{
    internal class Day13 : IDay<int, int>
    {
        string[] _input { get; }
        public Day13()
        {
            _input = File.ReadAllLines("Day13/input.txt");

    }
        public int Part1()
        {
            List<(Packet, Packet)> packetPairs = new List<(Packet, Packet)>();
            Stack<string> input = new Stack<string>(_input.Reverse());
            int count = 0;
            int result = 0;
            while (input.Count > 0)
            {
                var input1 = input.Pop();
                var input2 = input.Pop();

                var packet1 = PacketParser.Parse(new Stack<char>(input1.Skip(1).Reverse()));
                var packet2 = PacketParser.Parse(new Stack<char>(input2.Skip(1).Reverse()));
                packetPairs.Add((packet1, packet2));
                if (input.Count > 0)
                    input.Pop();
            }

            for(int i = 0; i < packetPairs.Count; i++)
            {
                var comparison = packetPairs[i].Item1.Compare(packetPairs[i].Item2);
                if (comparison.State == ThreeState.TrueDo().State)
                {
                    result += i + 1;
                    count++;
                }
            }
            return result;
        }

        public int Part2()
        {
            List<Packet> packets = new List<Packet>();
            Stack<string> input = new Stack<string>(_input.Reverse().Where(x => x != ""));
            int result = 1;

            while (input.Count > 0)
            {
                var input1 = input.Pop();

                var packet1 = PacketParser.Parse(new Stack<char>(input1.Skip(1).Reverse()));
                packets.Add(packet1);
            }
            var divider1 = new Packet(new Packet(new Packet(6)));
            divider1.IsDivider = true;
            var divider2 = new Packet(new Packet(new Packet(2)));
            divider2.IsDivider = true;
            packets.Add(divider1);
            packets.Add(divider2);

            var ordered = packets.OrderBy(x => x).ToList();

            for(int i = 0; i < ordered.Count(); i++)
            {
                if (ordered[i].IsDivider)
                {
                    result = result * (i + 1);
                }
            }
            
            return result;
        }
    }
}
