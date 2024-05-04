using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day19
{
    public class Blueprint
    {
        public Robot OreRobot { get; set; }
        public Robot ClayRobot { get; set; }
        public Robot GeodeRobot { get; set; }
        public Robot ObsidianRobot { get; set; }

        public Blueprint()
        {

        }
        public Blueprint(Robot oreRobot, Robot clayRobot, Robot geodeRobot, Robot obsidianRobot)
        {
            OreRobot = oreRobot;
            ClayRobot = clayRobot;
            GeodeRobot = geodeRobot;
            ObsidianRobot = obsidianRobot;
        }

        public int HighestOre()
        {
            return Math.Max(Math.Max(Math.Max(OreRobot.Ore, ClayRobot.Ore), ObsidianRobot.Ore), GeodeRobot.Ore);
        }

        public int HighestClay()
        {
            return ObsidianRobot.Clay;
        }

        public int HighestObsidian()
        {
            return GeodeRobot.Obsidian;
        }

        public int HighestGeode()
        {
            return 100000;
        }
    }
}
