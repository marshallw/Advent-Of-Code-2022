using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day13
{
    public class ThreeState
    {
        public string State { get; }

        public ThreeState(string state)
        {
            State = state;
        }

        public static ThreeState TrueDo() => new ThreeState("True");
        public static ThreeState FeelsBad() => new ThreeState("False");
        public static ThreeState Meh() => new ThreeState("Continue");
    }
}
