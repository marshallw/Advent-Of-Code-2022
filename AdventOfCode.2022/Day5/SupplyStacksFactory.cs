using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day5
{
    public static class SupplyStacksFactory
    {
        public static SupplyStacks Create(string[] input)
        {
            var dimensions = SupplyStackDimensions(input);
            var supplyStack = new SupplyStacks(dimensions.Item1);

            for (int i = dimensions.Item2; i >= 0; i--)
            {
                var line = input[i] + " ";
                int x = 0;
                do
                {
                    if (IsNextCharacterAString(line))
                        supplyStack.Add(x, GetNextLine(line));
                    x++;
                    line = line.Substring(4);

                } while (line.Length > 0);
            }

            return supplyStack;
        }

        private static bool IsNextCharacterAString(string input) => input[0] == '[';

        private static char GetNextLine(string input)
        {
            return input[1];
        }

        private static (int, int) SupplyStackDimensions(string[] input)
        {
            int x = (input[0].Length + 1) / 4;
            int y = 0;

            while (input[y][0] == '[')
            {
                y++;
            }

            return (x, y - 1);
        }
    }
}
