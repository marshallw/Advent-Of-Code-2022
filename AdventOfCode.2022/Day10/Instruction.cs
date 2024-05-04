using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day10
{
    public class Instruction
    {
        public int Value { get; private set; }
        public int NumCyclesToCompletion { get; private set; }
        public int CurrentCycle { get; private set; }

        public Instruction(int value, int numCyclesToCompletion)
        {
            Value = value;
            NumCyclesToCompletion = numCyclesToCompletion;
            CurrentCycle = 0;
        }

        public void IncrementCycle() => CurrentCycle++;

        public bool IsOpComplete() => CurrentCycle == NumCyclesToCompletion;
    }
}
