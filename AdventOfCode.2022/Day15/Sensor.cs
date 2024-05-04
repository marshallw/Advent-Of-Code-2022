using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day15
{
    internal class Sensor
    {
        public int yCheck = 2000000;
        public int X { get; }
        public int Y { get; }
        public Beacon beacon { get; }
        public Sensor(int x, int y, Beacon beacon)
        {
            X = x;
            Y = y;
            this.beacon = beacon;
        }

        public int Range()
        {
            return Math.Abs(X - beacon.X) + Math.Abs(Y - beacon.Y);
        }

        public bool IsWithinRange(int x, int y)
        {
            return Math.Abs(x - X) + Math.Abs(y - Y) <= Range();
        }

        public List<(int x, int y)> GetListWhereBeaconsCannotExist()
        {
            int xOffset = X - beacon.X;
            int yOffset = Y - beacon.Y;
            int totalDistance = Math.Abs(xOffset) + Math.Abs(yOffset);
            List<(int x, int y)> result = new List<(int x, int y)>();

            if (!(Y - totalDistance < yCheck && Y + totalDistance > yCheck))
                return result;


            //for (int y = Y - totalDistance; y <= Y + totalDistance; y++)
            for (int y = yCheck; y == yCheck; y++)
                for (int x = X - (totalDistance - Math.Abs(y - Y)); x <= X + (totalDistance - Math.Abs(y - Y)); x++)
                {
                    if (y == yCheck)
                        result.Add((x, y)); 
                }
            return result;
        }

        public List<Range> GetRangeWhereBeaconsCannotExist()
        {
            int xOffset = X - beacon.X;
            int yOffset = Y - beacon.Y;
            int totalDistance = Math.Abs(xOffset) + Math.Abs(yOffset);
            List<Range> result = new List<Range>();

            //if (!(Y - totalDistance < yCheck && Y + totalDistance > yCheck))
                //return result;


            for (int y = Y - totalDistance; y <= Y + totalDistance; y++)
                    result.Add(new Range((X - (totalDistance - Math.Abs(y - Y)), X + (totalDistance - Math.Abs(y - Y))), y));
            return result;
        }
    }
}
