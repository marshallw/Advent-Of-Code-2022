using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day7
{
    public class CdInstruction : IInstruction
    {
        private string _instruction;
        private string _arguments;

        public CdInstruction(string instruction, string arguments)
        {
            _instruction = instruction;
            _arguments = arguments;
        }

        public string GetArgument()
        {
            return _arguments;
        }

        public string GetInstruction()
        {
            return _instruction;
        }
    }
}
