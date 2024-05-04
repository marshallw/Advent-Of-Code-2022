using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day9
{
    public static class KnotFactory
    {
        public static (Knot, Knot) CreateRope(int size)
        {
            var head = new Knot(0, 0, null);
            var prevKnot = head;
            var currentKnot = head;
            for(int i = 1; i < size; i++)
            {
                currentKnot = new Knot(0, 0, prevKnot);
                prevKnot = currentKnot;
            }

            return (head, currentKnot);
        }

        public static Knot[] CreateRopeArray(int size)
        {
            Knot[] knots = new Knot[size];

            knots[0] = new Knot(0, 0, null);
            for (int i = 1; i < size; i++)
            {
                knots[i] = new Knot(0, 0, knots[i-1]);
            }

            return knots;
        }
    }
}
