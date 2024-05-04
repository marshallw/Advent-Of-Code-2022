using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day19
{
    public class State : IEquatable<State?>
    {
        public int ore { get; set; }
        public int clay { get; set; }
        public int obsidian { get; set; }
        public int geodes { get; set; }
        
        public int oreBots { get; set; }
        public int clayBots { get; set; }
        public int obsidianBots { get; set; }
        public int geodeBots { get; set; }
        public int count { get;  set; }

        public State(int ore, int clay, int obsidian, int geodes, int oreBots, int clayBots, int obsidianBots, int geodeBots)
        {
            this.ore = ore;
            this.clay = clay;
            this.obsidian = obsidian;
            this.geodes = geodes;
            this.oreBots = oreBots;
            this.clayBots = clayBots;
            this.obsidianBots = obsidianBots;
            this.geodeBots = geodeBots;
            count = 0;
        }

        public State(int ore, int clay, int obsidian, int geodes, int oreBots, int clayBots, int obsidianBots, int geodeBots, int count)
        {
            this.ore = ore;
            this.clay = clay;
            this.obsidian = obsidian;
            this.geodes = geodes;
            this.oreBots = oreBots;
            this.clayBots = clayBots;
            this.obsidianBots = obsidianBots;
            this.geodeBots = geodeBots;
            this.count = count;
        }

        public long GetGeodesBeingMade(int amount)
        {
                amount++;
            if (amount + count > 32)
                amount = 32 - count;
            return geodeBots * amount;
        }

        public (State, long) TryMakeOreBot(Robot oreBot)
        {
            int amountToincrease = (int)Math.Max(Math.Ceiling((double)(oreBot.Ore - ore) / oreBots), 0);

            State newState = IncrementCount(amountToincrease);
            newState.oreBots++;
            newState.ore -= oreBot.Ore;
            return (newState, GetGeodesBeingMade(amountToincrease));
        }

        public (State, long) TryMakeClayBot(Robot clayBot)
        {
            int amountToincrease = (int)Math.Max(Math.Ceiling((double)(clayBot.Ore - ore) / oreBots), 0);
            State newState = IncrementCount(amountToincrease);
            newState.clayBots++;
            newState.ore -= clayBot.Ore;
            return (newState, GetGeodesBeingMade(amountToincrease));
        }

        public (State, long) TryMakeObsidianBot(Robot obsidianBot)
        {
            int amountToincrease = (int)Math.Max(Math.Max(Math.Ceiling((double)(obsidianBot.Ore - ore) / oreBots), 
                                                 Math.Ceiling((double)(obsidianBot.Clay - clay) / clayBots)), 0);
            State newState = IncrementCount(amountToincrease);
            newState.obsidianBots++;
            newState.ore -= obsidianBot.Ore;
            newState.clay -= obsidianBot.Clay;
            return (newState, GetGeodesBeingMade(amountToincrease));
        }

        public (State, long) TryMakeGeodeBot(Robot geodeBot)
        {
            int amountToincrease = (int)Math.Max(Math.Max(Math.Ceiling((double)(geodeBot.Ore - ore) / oreBots),
                                                 Math.Ceiling((double)(geodeBot.Obsidian - obsidian) / obsidianBots)), 0);
            State newState = IncrementCount(amountToincrease);
            newState.geodeBots++;
            newState.ore -= geodeBot.Ore;
            newState.obsidian -= geodeBot.Obsidian;
            return (newState, GetGeodesBeingMade(amountToincrease));
        }

        public State IncrementCount(int amount)
        {
            amount++;
            if (amount + count > 32)
                amount = 32 - count;
            return new State(ore + amount * oreBots, clay + amount * clayBots, obsidian + amount * obsidianBots, geodes + amount * geodeBots, oreBots, clayBots, obsidianBots, geodeBots, count + amount);
        }
        public State Clone()
        {
            return new State(ore, clay, obsidian, geodes, oreBots, clayBots, obsidianBots, geodeBots, count);
        }

        public override bool Equals(object? obj)
        {
            var right = obj as State;

            return (ore == right.ore &&
                clay == right.clay &&
                obsidian == right.obsidian &&
                oreBots == right.oreBots &&
                clayBots == right.clayBots &&
                obsidianBots == right.obsidianBots &&
                geodeBots == right.geodeBots &&
                count == right.count);

        }

        public bool Equals(State? other)
        {
            return other is not null &&
                   ore == other.ore &&
                   clay == other.clay &&
                   obsidian == other.obsidian &&
                   oreBots == other.oreBots &&
                   clayBots == other.clayBots &&
                   obsidianBots == other.obsidianBots &&
                   geodeBots == other.geodeBots &&
                   count == other.count;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ore, clay, obsidian, oreBots, clayBots, obsidianBots, geodeBots, count);
        }

        public static bool operator ==(State? left, State? right)
        {
            return EqualityComparer<State>.Default.Equals(left, right);
        }

        public static bool operator !=(State? left, State? right)
        {
            return !(left == right);
        }
    }
}
