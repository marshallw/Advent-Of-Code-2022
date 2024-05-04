using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day4
{
    public class CleaningDuties
    {
        private int Start { get; set; }
        private int Stop { get; set; }

        public CleaningDuties (int start, int stop)
        {
            Start = start;
            Stop = stop;
        }

        public static CleaningDuties Parse(string range) => new CleaningDuties(Int32.Parse(range.Split("-")[0]), Int32.Parse(range.Split("-")[1]));

        public bool Contains(CleaningDuties partner) => (Start <= partner.Start && Stop >= partner.Stop) ? true : false;
        public bool OverlapsAtAll(CleaningDuties partner) => !((Start < partner.Start && Stop < partner.Start) || (Start > partner.Stop && Stop > partner.Stop));

    }
}
