using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day15
{
    internal class Day15 : IDay<int, long>
    {
        public string[] _input { get; }

        public Day15()
        {
            _input = File.ReadAllLines("Day15/input.txt");
        }
        public int Part1()
        {
            List<Sensor> sensors = new List<Sensor>();
            List<Beacon> beacons = new List<Beacon>();
            int yPositionCheck = 4000000;
            for (int i = 0; i < _input.Length; i++)
            {
                var splitInput = _input[i].Split(' ');

                (int x, int y) sensorCoordinates = (Int32.Parse(splitInput[2].Replace("x=", "").Replace(",", "")),
                    Int32.Parse(splitInput[3].Replace("y=", "").Replace(":", "")));

                (int x, int y) beaconCoordinates = (Int32.Parse(splitInput[8].Replace("x=", "").Replace(",", "")),
                    Int32.Parse(splitInput[9].Replace("y=", "")));
                var beacon = new Beacon(beaconCoordinates.x, beaconCoordinates.y);
                beacons.Add(beacon);
                sensors.Add(new Sensor(sensorCoordinates.x, sensorCoordinates.y, beacon));


            }

            List<(int x, int y)> beaconNt = new List<(int x, int y)>();

            foreach (var sensor in sensors)
            {
                var noBeacons = sensor.GetListWhereBeaconsCannotExist();
                foreach (var b in noBeacons)
                    //if (!beaconNt.Any(beacon => beacon.x == b.x && beacon.y == b.y))
                        beaconNt.Add(b);
            }

            var trueBeaconNt = beaconNt.Where(beacon => !beacons.Any(beacon2 => beacon.x == beacon2.X && beacon.y == beacon2.Y));
            //var ordered = trueBeaconNt.OrderBy(beacon => beacon.x).ThenBy(beacon => beacon.y);
            //var result = trueBeaconNt.Where(beacon => beacon.y == yPositionCheck).Distinct();
            return trueBeaconNt.Where(beacon => beacon.y == yPositionCheck).Distinct().Count();
        }

        public long Part2()
        {
            List<Sensor> sensors = new List<Sensor>();
            List<Beacon> beacons = new List<Beacon>();
            int start = 0;
            int end = 4000000;
            long multiplier = 4000000;

            for (int i = 0; i < _input.Length; i++)
            {
                var splitInput = _input[i].Split(' ');

                (int x, int y) sensorCoordinates = (Int32.Parse(splitInput[2].Replace("x=", "").Replace(",", "")),
                    Int32.Parse(splitInput[3].Replace("y=", "").Replace(":", "")));

                (int x, int y) beaconCoordinates = (Int32.Parse(splitInput[8].Replace("x=", "").Replace(",", "")),
                    Int32.Parse(splitInput[9].Replace("y=", "")));
                var beacon = new Beacon(beaconCoordinates.x, beaconCoordinates.y);
                beacons.Add(beacon);
                sensors.Add(new Sensor(sensorCoordinates.x, sensorCoordinates.y, beacon));
            }

            List<Range> noBeaconRanges = new List<Range>();

            foreach (var sensor in sensors)
            {
                var noBeacons = sensor.GetRangeWhereBeaconsCannotExist();
                foreach (var b in noBeacons)
                    //if (!beaconNt.Any(beacon => beacon.x == b.x && beacon.y == b.y))
                    noBeaconRanges.Add(b);
            }

            var curatedRange = new List<(int x, int y)>();

            // possibleCoordinates = possibleCoordinates.OrderBy(x => x.y).ToList();
            var group = noBeaconRanges.Where(x => x.Y >= start && x.Y <= end).GroupBy(x => x.Y);
            var fullList = new List<FullRange>();

            foreach(var range in group)
            {
                var fullRange = new FullRange(range.First().Y);
                foreach (var range2 in range)
                {
                    fullRange.XRange.Add(range2.XRange);
                }
                fullList.Add(fullRange);
            }
            List<(int x, int y)> blarg = new List<(int x, int y)>();
            foreach (var item in fullList)
            {
                blarg.AddRange(item.GetCoordinatesOutsideBoundary(start, end));
               
            }

            foreach (var b in blarg)
                Console.WriteLine(b.x * multiplier + Convert.ToInt64(blarg.First().y));

            return Convert.ToInt64(blarg.First().x) * multiplier + Convert.ToInt64(blarg.First().y);
            return 0;
            //return possibleCoordinates.First().x * multiplier + possibleCoordinates.First().y;
        }

        public long Part2_2()
        {
            List<Sensor> sensors = new List<Sensor>();
            List<Beacon> beacons = new List<Beacon>();
            int start = 0;
            int end = 4000000;
            long multiplier = 4000000;

            for (int i = 0; i < _input.Length; i++)
            {
                var splitInput = _input[i].Split(' ');

                (int x, int y) sensorCoordinates = (Int32.Parse(splitInput[2].Replace("x=", "").Replace(",", "")),
                    Int32.Parse(splitInput[3].Replace("y=", "").Replace(":", "")));

                (int x, int y) beaconCoordinates = (Int32.Parse(splitInput[8].Replace("x=", "").Replace(",", "")),
                    Int32.Parse(splitInput[9].Replace("y=", "")));
                var beacon = new Beacon(beaconCoordinates.x, beaconCoordinates.y);
                beacons.Add(beacon);
                sensors.Add(new Sensor(sensorCoordinates.x, sensorCoordinates.y, beacon));
            }

            var list = new List<(int x, int y)>();

            foreach(var sensor in sensors)
            {
                var x = sensor.X;
                var y = sensor.Y - sensor.Range() - 1;
                do
                {
                    if (y >= start && x >= start && y <= end && x <= end)
                    {
                        bool isBeacon = true;
                        foreach (var sensor2 in sensors)
                        {
                            if (sensor2.IsWithinRange(x, y))
                                isBeacon = false;
                        }
                        if (isBeacon)
                            return x * multiplier + y;
                    }
                    x += 1;
                    y += 1;
                } while (y <= sensor.Y);

                x = sensor.X + sensor.Range() + 1;
                y = sensor.Y;

                do
                {
                    if (y >= start && x >= start && y <= end && x <= end)
                    {
                        bool isBeacon = true;
                        foreach (var sensor2 in sensors)
                        {
                            if (sensor2.IsWithinRange(x, y))
                                isBeacon = false;
                        }
                        if (isBeacon)
                            return x * multiplier + y;
                    }
                    x -= 1;
                    y += 1;
                } while (x <= sensor.X);

                x = sensor.X;
                y = sensor.Y + sensor.Range() + 1;
                do
                {
                    if (y >= start && x >= start && y <= end && x <= end)
                    {
                        bool isBeacon = true;
                        foreach (var sensor2 in sensors)
                        {
                            if (sensor2.IsWithinRange(x, y))
                                isBeacon = false;
                        }
                        if (isBeacon)
                            return x * multiplier + y;
                    }
                    x -= 1;
                    y -= 1;
                } while (y >= sensor.Y);

                x = sensor.X - sensor.Range() - 1;
                y = sensor.Y;
                do
                {
                    if (y >= start && x >= start && y <= end && x <= end)
                    {
                        bool isBeacon = true;
                        foreach (var sensor2 in sensors)
                        {
                            if (sensor2.IsWithinRange(x, y))
                                isBeacon = false;
                        }
                        if (isBeacon)
                            return x * multiplier + y;
                    }
                    x += 1;
                    y -= 1;
                } while (x <= sensor.X);
            }
            return 0;
        }
    }
}
