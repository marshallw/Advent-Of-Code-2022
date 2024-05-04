using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day13
{
    public class PacketParser
    {
        public static Packet Parse(Stack<char> input)
        {
            Packet packet = new Packet();
            string toParse = "";

            while(input.Any())
            {
                var toProcess = input.Pop();

                if (toProcess == '[')
                {
                    packet.Packets.Add(Parse(input));
                }
                else if (toProcess == ']')
                {
                    if (toParse != "")
                        packet.Packets.Add(new Packet(Int32.Parse(toParse)));
                    return packet;
                }
                else if (toProcess == ',')
                {
                    if (toParse != "")
                        packet.Packets.Add(new Packet(Int32.Parse(toParse)));
                    toParse = "";
                }
                else
                {
                    toParse += toProcess;
                }
            }
            return packet;
        }
    }
}
