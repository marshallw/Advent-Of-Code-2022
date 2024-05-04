using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day16
{
    internal class Day16 : IDay<int, long>
    {
        public string[] _input { get; }

        public Day16()
        {
            _input = File.ReadAllLines("Day16/Test.txt");
        }

        public int Part1()
        {
            List<Node> nodes = new List<Node>();
            foreach (var line in _input)
            {
                var split = line.Split(" ");
                nodes.Add(new Node(split[1], Int32.Parse(split[4].Replace("rate=", "").Replace(";", ""))));
            }
            for(int j = 0; j < _input.Length; j++)
            {
                var split = _input[j].Split(" ");
                for(int i = 9; i < split.Length; i++)
                {
                    nodes[j].Nodes.Add(nodes.Single(node => node.Id == split[i].Replace(",", "")));
                }
            }

            var root = nodes.First(node => node.Id == "AA");
            var result = Recurse2(root, 0, 0, 0, new List<string>(), nodes.Where(x => x.Flow > 0).Count(), nodes);

            return result.OrderByDescending(x => x).First();
        }

        public List<Node2> CalculatePaths(List<Node> nodes)
        {
            List<Node2> result = new List<Node2>();
            foreach(var node in nodes.Where(x => x.Flow > 0))
            {
                result.Add(new Node2(node.Id, node.Flow));
            }
            result.Add(new Node2("AA", 0));

            foreach(var node in result)
            {
                foreach(var node2 in result.Where(x => x.Id != node.Id))
                {
                    node.Nodes.Add((node2, PlotPath(nodes.First(x => x.Id == node.Id), nodes.First(x => x.Id == node2.Id), nodes)));
                }
            }
            return result;
        }

        public List<int> Recurse2(Node current, int currentFlow, int pressureReleased, int count, List<string> nodesOpened, int numNodesWithFlow, List<Node> allNodes)
        {
            if (count >= 30)
                return new List<int> { pressureReleased };
            if (nodesOpened.Count == numNodesWithFlow)
            {
                return new List<int> { pressureReleased + currentFlow * (30 - count) };
            }
            List<int> results = new List<int>();

            foreach(var node in allNodes.Where(x => x.Flow > 0 && !nodesOpened.Contains(x.Id) && x.Id != current.Id))
            {
                var distance = PlotPath(current, node, allNodes);
                var nodesVisited = nodesOpened.ToList();
                nodesVisited.Add(node.Id);
                int newPressure = pressureReleased;
                for(int i = count; i < 30 && i < count + distance + 1; i++)
                {
                    newPressure += currentFlow;
                }
                results.AddRange(Recurse2(node, currentFlow + node.Flow, newPressure, count + distance + 1, nodesVisited, numNodesWithFlow, allNodes));
            }
            return results;
        }

        public int Part2ElectricBoogaloo(Node2 root, List<Node2> allNodes)
        {
            int count = 0;
            int numNodesWithFlow = allNodes.Count(x => x.Flow > 0);
            List<int> results = new List<int>();
            foreach(var node in allNodes.Where(n => n.Flow > 0 && n.Id != "AA"))
                foreach(var node2 in allNodes.Where(n => n.Flow > 10 && n.Id != node.Id && n.Id != "AA"))
                {
                    results.Add(Part2ElectricBoogaloo(new Traveler(root, node, root.Nodes.First(x => x.Node.Id == node.Id).Weight, false), new Traveler(root, node2, root.Nodes.First(x => x.Node.Id == node2.Id).Weight, false), 0, 0, 0, new List<string> (), numNodesWithFlow, allNodes));
                }

            return results.OrderByDescending(x => x).First();
        }

        public long Part2RambosRevenge(Node2 root, List<Node2> allNodes)
        {
            Dictionary<State, long> cache = new Dictionary<State, long>();
            List<long> results = new List<long>();
            foreach (var node in allNodes.Where(n => n.Flow > 0 && n.Id != "AA"))
                foreach (var node2 in allNodes.Where(n => n.Flow > 10 && n.Id != node.Id && n.Id != "AA"))
                {
                    results.Add(Part2RambosRevenge(new State(new List<string>(), 0, 0, 0, 
                        new Traveler(root, node2, root.Nodes.First(x => x.Node.Id == node2.Id).Weight,false),
                        new Traveler(root, node, root.Nodes.First(x => x.Node.Id == node.Id).Weight, false)), allNodes, cache));
                }

            return results.OrderByDescending(x => x).First();
        }

        public long Part2RambosRevenge(State state, List<Node2> allNodes, Dictionary<State, long> cache)
        {
            if (cache.ContainsKey(state))
                return cache[state];

            var results = new List<long>();

            var (stateAfterMoving, pressureReleased) = state.GetNextState();

            if (stateAfterMoving.OnLastFewNodes(allNodes.Count() - 1))
            {
                int pressureExtra = 0;
                (stateAfterMoving, pressureExtra) = stateAfterMoving.FinishOpeningNodes();
                pressureReleased += pressureExtra;
            }

            if (state.NodesOpened.Count == allNodes.Count() - 1)
            {
                return pressureReleased + state.CurrentFlow * (26 - state.Count);
            }
            else if (state.Count >= 26)
            {
                return pressureReleased;
            }

            if (stateAfterMoving.Me.Opened && stateAfterMoving.Elephent.Opened)
            {
                foreach (var node in allNodes.Where(n => n.Id != stateAfterMoving.Me.Destination.Id && n.Id != stateAfterMoving.Elephent.Destination.Id && n.Flow > 0 && !stateAfterMoving.NodesOpened.Contains(n.Id)))
                    foreach (var node2 in allNodes.Where(n => n.Id != stateAfterMoving.Me.Destination.Id && n.Id != stateAfterMoving.Elephent.Destination.Id && n.Flow > 0 && n.Id != node.Id && !stateAfterMoving.NodesOpened.Contains(n.Id)))
                    {
                        var newState = stateAfterMoving.Clone();
                        newState.Me = stateAfterMoving.Me.SetNewDestination(node, stateAfterMoving.Me.Destination.Nodes.First(x => x.Node.Id == node.Id).Weight);
                        newState.Elephent = stateAfterMoving.Elephent.SetNewDestination(node2, stateAfterMoving.Elephent.Destination.Nodes.First(x => x.Node.Id == node2.Id).Weight);
                        results.Add(pressureReleased + Part2RambosRevenge(newState, allNodes, cache));
                    }
            }
            else if (stateAfterMoving.Me.Opened)
            {
                foreach (var node in allNodes.Where(x => x.Id != stateAfterMoving.Elephent.Destination.Id && x.Flow > 0 && !stateAfterMoving.NodesOpened.Contains(x.Id)))
                {
                    var newState = stateAfterMoving.Clone();
                    newState.Me = stateAfterMoving.Me.SetNewDestination(node, stateAfterMoving.Me.Destination.Nodes.First(x => x.Node.Id == node.Id).Weight);
                    results.Add(pressureReleased + Part2RambosRevenge(newState, allNodes, cache));
                }
            }
            else if (stateAfterMoving.Elephent.Opened)
            {
                foreach (var node in allNodes.Where(x => x.Id != stateAfterMoving.Me.Destination.Id && x.Flow > 0 && !stateAfterMoving.NodesOpened.Contains(x.Id)))
                {
                    var newState = stateAfterMoving.Clone();
                    newState.Elephent = stateAfterMoving.Elephent.SetNewDestination(node, stateAfterMoving.Elephent.Destination.Nodes.First(x => x.Node.Id == node.Id).Weight);
                    results.Add(pressureReleased + Part2RambosRevenge(newState, allNodes, cache));
                }
            }
            var result = results.Count() > 0 ? results.OrderByDescending(x => x).First() : 0;
            cache.Add(state, result);
            return result;
        }

        public int Part2ElectricBoogaloo(Traveler traveler, Traveler elephent, int currentFlow, int pressureReleased, int count, List<string> nodesOpened, int numNodesWithFlow, List<Node2> allNodes)
        {
            var results = new List<int>();


            while ((!traveler.Opened && !elephent.Opened) && count < 26)
            {
                count++;
                pressureReleased += currentFlow;
                if (traveler.distance > 0)
                {
                    traveler.distance--;
                }
                else
                {
                    currentFlow += traveler.Destination.Flow;
                    nodesOpened.Add(traveler.Destination.Id);
                    traveler.Opened = true;
                }

                if (elephent.distance > 0)
                {
                    elephent.distance--;
                }
                else
                {
                    currentFlow += elephent.Destination.Flow;
                    nodesOpened.Add(elephent.Destination.Id);
                    elephent.Opened = true;
                }
            }

            if (count < 26 && nodesOpened.Count() + (!traveler.Opened ? 1 : 0) + (!elephent.Opened ? 1 : 0) == numNodesWithFlow)
            {
                while (nodesOpened.Count() != numNodesWithFlow && count < 26)
                {
                    count++;
                    pressureReleased += currentFlow;
                    if (!traveler.Opened && traveler.distance > 0)
                    {
                        traveler.distance--;

                    }
                    else if (!traveler.Opened)
                    {
                        currentFlow += traveler.Destination.Flow;
                        nodesOpened.Add(traveler.Destination.Id);
                        traveler.Opened = true;
                    }

                    if (elephent.distance > 0)
                    {
                        elephent.distance--;
                    }
                    else if (!elephent.Opened)
                    {
                        currentFlow += elephent.Destination.Flow;
                        nodesOpened.Add(elephent.Destination.Id);
                        elephent.Opened = true;
                    }
                }
            }
            if (nodesOpened.Count == numNodesWithFlow)
            {
                return pressureReleased + currentFlow * (26 - count);
            }
            else if (count >= 26)
            {
                return pressureReleased;
            }

            if (traveler.Opened && elephent.Opened)
            {
                foreach (var node in allNodes.Where(n => n.Id != traveler.Destination.Id && n.Id != elephent.Destination.Id && n.Flow > 0 && !nodesOpened.Contains(n.Id)))
                    foreach (var node2 in allNodes.Where(n => n.Id != traveler.Destination.Id && n.Id != elephent.Destination.Id && n.Flow > 0 && n.Id != node.Id && !nodesOpened.Contains(n.Id)))
                    {
                        results.Add(Part2ElectricBoogaloo(new Traveler(traveler.Destination, node, traveler.Destination.Nodes.First(x => x.Node.Id == node.Id).Weight, false), new Traveler(elephent.Destination, node2, elephent.Destination.Nodes.First(x => x.Node.Id == node2.Id).Weight, false), currentFlow, pressureReleased, count, nodesOpened.ToList(), numNodesWithFlow, allNodes));
                    }
            }
                else if (traveler.Opened)
                {
                    foreach (var node in allNodes.Where(x => x.Id != elephent.Destination.Id && x.Flow > 0 && !nodesOpened.Contains(x.Id)))
                    {
                        var newTraveler = new Traveler(traveler.Destination, node, traveler.Destination.Nodes.First(x => x.Node.Id == node.Id).Weight, false);
                        results.Add(Part2ElectricBoogaloo(newTraveler, elephent.Clone(), currentFlow, pressureReleased, count, nodesOpened.ToList(), numNodesWithFlow, allNodes));
                    }
                }
                else if (elephent.Opened)
                {
                    foreach (var node in allNodes.Where(x => x.Id != traveler.Destination.Id && x.Flow > 0 && !nodesOpened.Contains(x.Id)))
                    {
                        var newElephent = new Traveler(elephent.Destination, node, elephent.Destination.Nodes.First(x => x.Node.Id == node.Id).Weight, false);
                        results.Add(Part2ElectricBoogaloo(traveler.Clone(), newElephent, currentFlow, pressureReleased, count, nodesOpened.ToList(), numNodesWithFlow, allNodes));
                    }
                }

            return results.Count() > 0 ? results.OrderByDescending(x => x).First() : 0;

        }



        public List<int> Part2Recurse(Node current, Node elephent, int distance1, int distance2, int currentFlow, int pressureReleased, int count, List<string> nodesOpened, int numNodesWithFlow, List<Node> allNodes)
        {
            if (count >= 26)
                return new List<int> { pressureReleased };
            if (nodesOpened.Count == numNodesWithFlow)
            {
                return new List<int> { pressureReleased + currentFlow * (30 - count) };
            }
            List<int> results = new List<int>();
            if (distance1 <= 0)
            {
                var nodes = allNodes.Where(x => x.Flow > 0 && !nodesOpened.Contains(x.Id));
                foreach (var node in nodes)
                {
                    var newDistance1 = PlotPath(current, node, allNodes);
                    var nodesVisited = nodesOpened.ToList();
                    nodesVisited.Add(node.Id);
                    nodesVisited.Add(elephent.Id);
                    int newPressure = pressureReleased;
                    int c = 0;
                    while (newDistance1 - c > 0 && distance2 - c > 0)
                    {

                        newPressure += currentFlow;
                        c++;
                    }
                    results.AddRange(Part2Recurse(node, elephent, newDistance1 - c - 1, distance2 - c - 1, (distance1 - c == 0)? currentFlow + node.Flow : currentFlow + elephent.Flow, newPressure, count + c + 1, nodesVisited, numNodesWithFlow, allNodes));
                }
            }
            else
            {
                foreach (var node2 in allNodes.Where(x => x.Flow > 0 && !nodesOpened.Contains(x.Id)))
                {
                    var newDistance2 = PlotPath(elephent, node2, allNodes);
                    var nodesVisited = nodesOpened.ToList();
                    nodesVisited.Add(node2.Id);
                    nodesVisited.Add(current.Id);
                    int newPressure = pressureReleased;
                    int c = 0;
                    while (distance1 - c > 0 && newDistance2 - c > 0)
                    {

                        newPressure += currentFlow;
                        c++;
                    }
                    results.AddRange(Part2Recurse(current, node2, distance1 - c - 1, newDistance2 - c - 1, (distance1 - c == 0)?currentFlow + current.Flow : currentFlow + node2.Flow, newPressure, count + c + 1, nodesVisited, numNodesWithFlow, allNodes));
                }
            }
            return results;
        }

        public int PlotPath(Node start, Node end, List<Node> nodes)
        {
            Dictionary<Node, (int, bool)> allNodes = new Dictionary<Node, (int, bool)>();
            //Dictionary<Node, Node> path = new Dictionary<Node, Node>();

            //foreach(var node in nodes)
            //{
            //    path.Add(node, null);
            //}

            foreach(var n in nodes)
                allNodes.Add(n, (10000, false));
            allNodes[start] = (0, allNodes[start].Item2);

            while (allNodes[end].Item2 == false)
            {
                var current = allNodes.Where(node => node.Value.Item2 == false && node.Value.Item1 < 10000).OrderBy(x => x.Value.Item1).First().Key;

                foreach(var node in current.Nodes)
                {
                    if (allNodes[node].Item1 > allNodes[current].Item1 + 1)
                    {
                        allNodes[node] = (allNodes[current].Item1 + 1, allNodes[node].Item2);
                        //path[node] = current;
                    }
                }
                allNodes[current] = (allNodes[current].Item1, true);
            }

            //var working = path[end];
            //var result = new List<Node>();

            //while(working != start)
            //{
            //    result.Add(working);
            //    working = path[working];
            //}
            //result.Reverse();
            return allNodes[end].Item1;
        }

        public long Part2()
        {
            List<Node> nodes = new List<Node>();
            foreach (var line in _input)
            {
                var split = line.Split(" ");
                nodes.Add(new Node(split[1], Int32.Parse(split[4].Replace("rate=", "").Replace(";", ""))));
            }
            for (int j = 0; j < _input.Length; j++)
            {
                var split = _input[j].Split(" ");
                for (int i = 9; i < split.Length; i++)
                {
                    nodes[j].Nodes.Add(nodes.Single(node => node.Id == split[i].Replace(",", "")));
                }
            }
            var curatedList = CalculatePaths(nodes);

            var root = curatedList.First(node => node.Id == "AA");
            var result = Part2RambosRevenge(root, curatedList);

            return result;
        }
    }
}
