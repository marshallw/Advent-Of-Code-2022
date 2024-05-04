using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day2
{
    public class Score
    {
        public int Value { get; }

        public Score(int value)
        {
            Value = value;
        }

        public static Score GetScore(string result)
        {
            if (result == "X")
                return X;
            if (result == "Y")
                return Y;
            if (result == "Z")
                return Z;
            return Z;
        }

        public static Score X => new Score(0);
        public static Score Y => new Score(3);
        public static Score Z => new Score(6);
    }
}
