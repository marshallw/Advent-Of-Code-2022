using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day16
{
    public class State : IEquatable<State?>
    {
        public List<string> NodesOpened { get; set; }
        public int Count { get; set; }
        public int CurrentFlow { get; set; }
        public int PressureReleased { get; set; }

        public Traveler Elephent { get; set; }
        public Traveler Me { get; set; }

        public State(List<string> nodesOpened, int count, int currentFlow, int pressureReleased, Traveler elephent, Traveler me)
        {
            NodesOpened = nodesOpened;
            Count = count;
            CurrentFlow = currentFlow;
            PressureReleased = pressureReleased;
            Elephent = elephent;
            Me = me;
        }

        public State Clone() => new State(NodesOpened.ToList(), Count, CurrentFlow, PressureReleased, Elephent.Clone(), Me.Clone());

        public override bool Equals(object? obj)
        {
            return Equals(obj as State);
        }

        public bool Equals(State? other)
        {
            return other is not null &&
                   EqualityComparer<List<string>>.Default.Equals(NodesOpened, other.NodesOpened) &&
                   Count == other.Count &&
                   CurrentFlow == other.CurrentFlow &&
                   EqualityComparer<Traveler>.Default.Equals(Elephent, other.Elephent) &&
                   EqualityComparer<Traveler>.Default.Equals(Me, other.Me);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NodesOpened, Count, CurrentFlow, Elephent, Me);
        }

        public (State, int) GetNextState()
        {
            bool reachedEnd = false;
            int distance = Math.Min(Elephent.distance, Me.distance);
            if (distance + Count > 26)
            {
                distance = 26 - Count;
                reachedEnd = true;
            }
            var nodesOpened = NodesOpened.ToList();

            var newMe = Me.Clone();
            newMe.distance -= distance;
            var newElephent = Elephent.Clone();
            newElephent.distance -= distance;
            int additionalFlow = 0;

            if (newMe.distance == 0 && !reachedEnd)
            {
                newMe.Opened = true;
                nodesOpened.Add(newMe.Destination.Id);
                additionalFlow += newMe.Destination.Flow;
            }
            if (newElephent.distance == 0 && !reachedEnd)
            {
                newElephent.Opened = true;
                nodesOpened.Add(newElephent.Destination.Id);
                additionalFlow += newElephent.Destination.Flow;
            }

            if (!reachedEnd)
                distance++;

            if (newElephent.Opened && !newMe.Opened)
                newMe.distance--;
            if (newMe.Opened && !newElephent.Opened)
                newElephent.distance--;


            return (new State(nodesOpened, Count + distance, CurrentFlow + additionalFlow, PressureReleased, newElephent, newMe), distance * CurrentFlow);
        }

        public (State, int) FinishOpeningNodes()
        {
            if (!Me.Opened && !Elephent.Opened)
            {
                var (newState, currentPressureReleased) = GetNextState();
            }
        }

        public bool OnLastFewNodes(int totalNodes)
        {
            return NodesOpened.Count() + (Me.Opened == false ? 1 : 0) + (Elephent.Opened == false ? 1 : 0) == totalNodes;
        }
    }
}
