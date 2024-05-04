using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day19
{
    public class Robot
    {
        public string RobotType { get; }
        public int Obsidian { get; }
        public int Ore { get; }
        public int Clay { get; }

        public Robot(int obsidian, int ore, int clay, string robotType)
        {
            Obsidian = obsidian;
            Ore = ore;
            Clay = clay;
            RobotType = robotType;
        }

        public static Robot OreRobot(int ore)
        {
            return new Robot(0, ore, 0, "Ore");
        }

        public static Robot ClayRobot(int ore) => new Robot(0, ore, 0, "Clay");
        public static Robot ObsidianRobot(int ore, int clay) => new Robot(0, ore, clay, "Obsidian");
        public static Robot GeodeRobot(int ore, int obsidian) => new Robot(obsidian, ore, 0, "Geode");
    }
}
