using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day7
{
    internal class DirInstruction : IInstruction
    {
        public string _argument { get; }
        public string _instruction { get; }

        public DirInstruction(string argument, string instruction)
        {
            _argument = argument;
            _instruction = instruction;
        }

        public string GetArgument()
        {
            return _argument;
        }

        public string GetInstruction()
        {
            return _instruction;
        }
    }
}
