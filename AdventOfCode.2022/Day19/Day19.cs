using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day19
{
    internal class Day19 : IDay<long, long>
    {
        public string[] _input { get; }

        public Day19()
        {
            _input = File.ReadAllLines(@"Day19\input.txt");
        }
        public long Part1()
        {
            Queue<string> instructions = new Queue<string>(_input);
            List<Blueprint> blueprints = new List<Blueprint>();


            do
            {
                var instruction = instructions.Dequeue();
                var split = instruction.Split(" ").Where(x => Int32.TryParse(x, out _)).Select(x => Int32.Parse(x)).ToList();
                blueprints.Add(new Blueprint ()
                {
                    OreRobot = Robot.OreRobot(split[0]),
                    ClayRobot = Robot.ClayRobot(split[1]),
                    ObsidianRobot = Robot.ObsidianRobot(split[2], split[3]),
                    GeodeRobot = Robot.GeodeRobot(split[4], split[5])
                });

            } while (instructions.Any());

            List<long> results = new List<long>();
            long result = 0;
            int count = 0;
            foreach(var blueprint in blueprints)
            {
                count++;
              //  results.Add(CalculateQuality(blueprint, new State(0, 0, 0, 0, 1, 0, 0, 0)));
                result += results.Last() * count;
            }
            return result;
        }

        private long CalculateQuality(Blueprint blueprint, State state, Dictionary<State, long> cache)
        {
            if (state.count == 32)
                return 0;
            if (state.count > 32)
                throw new Exception("uh oh");

            if (cache.ContainsKey(state))
            {
                return cache[state];
            }

            List<long> results = new List<long>();




            if (state.oreBots < blueprint.HighestOre())
            {
                var (oreState, amount) = state.TryMakeOreBot(blueprint.OreRobot);
                results.Add(amount + CalculateQuality(blueprint, oreState, cache));
            }

            if (state.clayBots < blueprint.HighestClay())
            {
                var (clayState, clayAmount) = state.TryMakeClayBot(blueprint.ClayRobot);
                results.Add(clayAmount + CalculateQuality(blueprint, clayState, cache));
            }

            if (state.clayBots > 0)
            {
                if (state.obsidianBots < blueprint.HighestObsidian())
                {
                    var (obsidianState, obsidianAmount) = state.TryMakeObsidianBot(blueprint.ObsidianRobot);
                    results.Add(obsidianAmount + CalculateQuality(blueprint, obsidianState, cache));
                }

            }
            if (state.obsidianBots > 0)
            {

                var (geodeState, geodeAmount) = state.TryMakeGeodeBot(blueprint.GeodeRobot);
                results.Add(geodeAmount + CalculateQuality(blueprint, geodeState, cache));

            }
            var result = results.OrderByDescending(x => x).First();
            cache.Add(state, result);
            return result;

        }

        public long Part2()
        {
            Queue<string> instructions = new Queue<string>(_input);
            List<Blueprint> blueprints = new List<Blueprint>();

            var stopwatch = Stopwatch.StartNew();

            do
            {
                var instruction = instructions.Dequeue();
                var split = instruction.Split(" ").Where(x => Int32.TryParse(x, out _)).Select(x => Int32.Parse(x)).ToList();
                blueprints.Add(new Blueprint()
                {
                    OreRobot = Robot.OreRobot(split[0]),
                    ClayRobot = Robot.ClayRobot(split[1]),
                    ObsidianRobot = Robot.ObsidianRobot(split[2], split[3]),
                    GeodeRobot = Robot.GeodeRobot(split[4], split[5])
                });

            } while (instructions.Any());

            List<long> results = new List<long>();
            long result = 1;
            int count = 0;

            Parallel.ForEach(blueprints.Take(3), blueprint =>
            {
                count++;

                Dictionary<State, long> cache = new Dictionary<State, long>();
                results.Add(CalculateQuality(blueprint, new State(0, 0, 0, 0, 1, 0, 0, 0), cache));

                Console.WriteLine("Done " + count + " amount is " + results.Last());
            });

            results.ForEach(x => result = result * x);
            stopwatch.Stop();
            Console.WriteLine($"This took {stopwatch.Elapsed.TotalSeconds}");
            return result;
        }
    }
}
