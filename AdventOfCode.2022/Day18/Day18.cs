using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day18
{
    public class Day18 : IDay<int, int>
    {
        public string[] _input { get; }

        public Day18()
        {
            _input = File.ReadAllLines(@"Day18\input.txt");
        }
        public int Part1()
        {
            List<(int x, int y, int z)> coordinates = new List<(int x, int y, int z)>();

            foreach(var line in _input)
            {
                var split = line.Split(',');
                coordinates.Add((Int32.Parse(split[0]), Int32.Parse(split[1]), Int32.Parse(split[2])));
            }

            var numSides = coordinates.Select(drop1 => coordinates.Where(drop2 => drop1 != drop2 && (Math.Abs(drop1.x - drop2.x) + Math.Abs(drop1.y - drop2.y) + Math.Abs(drop1.z - drop2.z)) == 1).Count()).Select(numSides => 6 - numSides);
            return numSides.Sum();


        }

        public int Part2()
        {
            List<(int x, int y, int z)> coordinates = new List<(int x, int y, int z)>();

            foreach (var line in _input)
            {
                var split = line.Split(',');
                coordinates.Add((Int32.Parse(split[0]), Int32.Parse(split[1]), Int32.Parse(split[2])));
            }

            Matrix matrix = new Matrix(coordinates.OrderByDescending(coordinate => coordinate.x).First().x + 1, coordinates.OrderByDescending(coordinate => coordinate.y).First().y + 1, coordinates.OrderByDescending(coordinate => coordinate.z).First().z + 1);

            //foreach (var coordinate in coordinates)
            for (int i = 0; i < coordinates.Count(); i++)
            {
                var coordinate = coordinates[i];
                matrix.Map[coordinate.x, coordinate.y, coordinate.z] = 1;
            }


            var numSidesExposed = coordinates.Select(drop1 => coordinates.Where(drop2 => drop1 != drop2 && (Math.Abs(drop1.x - drop2.x) + Math.Abs(drop1.y - drop2.y) + Math.Abs(drop1.z - drop2.z)) == 1).Count()).Select(numSides => 6 - numSides);

            var numAirSpacesEnclosed = matrix.GetEmptySpace();

            var emptySidesExposed = numAirSpacesEnclosed.Select(drop1 => numAirSpacesEnclosed.Where(drop2 => drop1 != drop2 && (Math.Abs(drop1.x - drop2.x) + Math.Abs(drop1.y - drop2.y) + Math.Abs(drop1.z - drop2.z)) == 1).Count()).Select(numSides => 6 - numSides);


            return numSidesExposed.Sum() - emptySidesExposed.Sum();
        }
    }
}
