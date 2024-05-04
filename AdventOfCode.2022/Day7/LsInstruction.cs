using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day7
{
    public class LsInstruction : IInstruction
    {
        public LsInstruction()
        {

        }

        public string GetArgument()
        {
            throw new NotImplementedException();
        }

        public string GetInstruction()
        {
            return "ls";
        }
    }
}
