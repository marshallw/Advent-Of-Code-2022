using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day13
{
    public class Packet : IComparable<Packet>
    {
        public int Value { get; }
        public List<Packet> Packets { get; }
        public bool IsValue { get; }
        public bool IsDivider { get; set; }
        public bool IsPacket
        {
            get
            {
                return !IsValue;
            }
        }
        public Packet(int value) : base()
        {
            Value = value;
            IsValue = true;
        }

        public Packet(Packet packet)
        {
            Packets = new List<Packet>(new[] { packet });
            IsValue = false;
            IsDivider = false;
        }

        public Packet()
        {
            Packets = new List<Packet>();
            IsValue = false;
            IsDivider = false;
        }

        public override bool Equals(object? obj)
        {
            var other = (Packet)obj;

            if (other.IsValue && IsValue)
                return other.Value == Value;
            return false;
        }

        public static bool operator<(Packet left, Packet right)
        {
            return left.Compare(right).State == ThreeState.TrueDo().State;
        }


        public ThreeState Compare(Packet right)
        {
            if (IsPacket && right.IsPacket)
            {
                ThreeState result;
                for (int i = 0; i < Packets.Count; i++)
                {
                    if (right.Packets.Count <= i)
                        return ThreeState.FeelsBad();

                    if (Packets[i].IsValue && right.Packets[i].IsValue)
                    {
                        if (Packets[i].Value < right.Packets[i].Value)
                            return ThreeState.TrueDo();
                        if (Packets[i].Value > right.Packets[i].Value)
                            return ThreeState.FeelsBad();
                    }
                    else
                    {
                        result = Packets[i].Compare(right.Packets[i]);
                        if (result.State != ThreeState.Meh().State)
                            return result;
                    }
                }
                if (Packets.Count() < right.Packets.Count())
                    return ThreeState.TrueDo();
                return ThreeState.Meh();
            }
            else
            {
                if (IsValue)
                {
                    var compare1 = new Packet();
                    compare1.Packets.Add(this);
                    return compare1.Compare(right);
                }
                else
                {
                    var compare1 = new Packet();
                    compare1.Packets.Add(right);
                    return this.Compare(compare1);
                }
            }
        }

        public int CompareTo(Packet? other)
        {
            ThreeState result = this.Compare(other);
            if (result.State == ThreeState.TrueDo().State)
                return -1;
            if (result.State == ThreeState.FeelsBad().State)
            return 1;
            if (result.State == ThreeState.Meh().State)
                throw new Exception("huh?");
            return 0;
        }

        public static bool operator>(Packet left, Packet right)
        {
            return right < left;
        }

    }
}
