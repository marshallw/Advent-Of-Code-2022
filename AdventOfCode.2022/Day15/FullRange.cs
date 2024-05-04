using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day15
{
    public class FullRange
    {
        public List<(int X, int X2)> XRange { get; }
        public int Y { get; }

        public FullRange(int y)
        {
            Y = y;
            XRange = new List<(int X, int X2)>();
        }

        public List<(int x, int y)> GetCoordinatesOutsideBoundary(int start, int end)
        {
            //List<(int x, int y)> result = new List<(int x, int y)>();
            //List<List<(int x, int x2)>> rangeList = new List<List<(int x, int x2)>>();
            int[] result = new int[end + 1];
            Array.Fill(result, 1);

            //var list = new List<(int x, int y)>();
            //List<int> sequence = new List<int>();

            //sequence = Enumerable.Range(start, end).ToList();

            foreach (var item in XRange)
            {
                for (int i = Math.Max(item.X, start); i <= Math.Min(end, item.X2); i++)
                    result[i] = 0;
                //sequence.RemoveAll(x => x >= item.X && item.X2 >= x);
                //list = new List<(int x, int y)>();
                //if (start < item.X)
                //    list.Add((start, item.X - 1));
                //if (end > item.X2)
                //    list.Add((item.X2 + 1, end));
                //rangeList.Add(list);
            }
            // if (rangeList.Count < XRange.Count())
            //    return new List<(int x, int y)>();

            //var result = Narrow(rangeList, start, end);
            for (int i = start; i < end; i++)
                if (result[i] == 1)
                return new List<(int x, int y)> { (i, Y) };
            return new List<(int x, int y)>();
            //if (sequence.Count == 1)
            //    return new List<(int x, int y)> { (sequence[0], Y) };
            //if (sequence.Count > 1)
            //    throw new Exception("spaghettio");
            //return new List<(int x, int y)>();
            //if (result.Count() == 0)
            //    return new List<(int x, int y)>();
            //    return new List<(int x, int y)> { (result.First().x, Y) };

            //var final = result.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
            //var count = result.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Count();

            //if (count == XRange.Count())
            //    return new List<(int x, int y)> { final };
            //if (count > XRange.Count())
            //    throw new Exception("blarg");
            //return new List<(int x, int y)>();

        }

        public List<(int x, int x2)> Narrow(List<List<(int x, int x2)>> range, int x, int x2)
        {
            var result = (x, x2);
            List<(int x, int x2)> resultList = new List<(int x, int x2)>();

            resultList.AddRange(Narrow(result, range[0][0], range, 1));
            if (range[0].Count() == 2)
            {
                resultList.AddRange(Narrow(result, range[0][1], range, 1));
            }

            return resultList.Where(x => x.x2 - x.x == 0).ToList();
        }

        public List<(int x, int x2)> Narrow((int x, int x2) soFar, (int x, int x2) toProcess, List<List<(int x, int x2)>> rest, int index)
        {
            if (index > rest.Count())
                return new List<(int x, int x2)>();

            if (soFar.x < toProcess.x)
                soFar.x = toProcess.x;
            if (soFar.x2 > toProcess.x2)
                soFar.x2 = toProcess.x2;
            List<(int x, int x2)> resultList = new List<(int x, int x2)>();

            if (soFar.x > soFar.x2)
                return resultList;
            if (index + 1 >= rest.Count())
            {
                resultList.Add(soFar);
                return resultList;
            }

            resultList.AddRange(Narrow(soFar, rest[index + 1][0], rest, index + 1));

            if (rest[index+1].Count() == 2)
                resultList.AddRange(Narrow(soFar, rest[index + 1][1], rest, index + 1));

            return resultList;
        }
    }
}
