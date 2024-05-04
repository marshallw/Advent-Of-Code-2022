using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day18
{
    public class Matrix
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int[,,] Map { get; set; }

        public Matrix(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Map = new int[X, Y, Z];

            for(int z2 = 0; z2 < Z; z2++)
                for(int y2 = 0; y2 < Y; y2++)
                    for(int x2 = 0; x2 < X; x2++)
                    {
                        Map[x2, y2, z2] = 0;
                    }
        }

        public List<(int x, int y, int z)> GetEmptySpace()
        {
            List<(int x, int y, int z)> results = new List<(int x, int y, int z)>();
            for(int z = 0; z < Z; z++)
                for(int y = 0; y < Y; y++)
                    for(int x = 0; x < X; x++)
                    {
                        if (Map[x,y,z] == 0 && !IsExposed(x,y,z))
                            results.Add((x,y,z));
                            
                    }
            return results;
        }

        public bool IsExposed(int x, int y, int z)
        {
            bool result = true;
            //### X
            for(int x2 = 0; x2 < x; x2++)
            {
                if (Map[x2, y, z] == 1)
                    result = false;
            }
            if (result)
                return result;
            result = true;
            for (int x2 = X - 1; x2 > x; x2--)
            {
                if (Map[x2, y, z] == 1)
                    result = false;
            }
            if (result)
                return result;
            result = true;
            //### Y
            for (int y2 = 0; y2 < y; y2++)
            {
                if (Map[x, y2, z] == 1)
                    result = false;
            }
            if (result)
                return result;
            result = true;
            for (int y2 = Y - 1; y2 > y; y2--)
            {
                if (Map[x, y2, z] == 1)
                    result = false;
            }
            if (result)
                return result;
            result = true;

            //### Z
            for (int z2 = 0; z2 < z; z2++)
            {
                if (Map[x, y, z2] == 1)
                    result = false;
            }
            if (result)
                return result;
            result = true;
            for (int z2 = Z - 1; z2 > z; z2--)
            {
                if (Map[x, y, z2] == 1)
                    result = false;
            }

            // we've exposed all options; either it's exposed (true) or it's not (false)
            return result;

        }

    }
}
