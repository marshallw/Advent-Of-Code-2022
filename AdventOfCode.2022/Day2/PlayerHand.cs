using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day2
{
    public class PlayerHand
    {
        public int Score { get; }

        public PlayerHand(int score)
        {
            Score = score;
        }

        public static PlayerHand Y => new PlayerHand(2);
        public static PlayerHand X => new PlayerHand(1);
        public static PlayerHand Z => new PlayerHand(3);

        public static PlayerHand GetScore(string hand)
        {
            if (hand == "X")
                return X;
            if (hand == "Y")
                return Y;
            if (hand == "Z")
                return Z;
            return Z;
        }
    }
}
