namespace AdventOfCode._2022.Day7
{
    public static class InstructionFactory
    {
        public static bool IsInstruction(string input)
        {
            return input[0] == '$';
        }

        public static IInstruction Create(string input)
        {
            var pieces = input.Split(' ');
            if (pieces[1] == "cd")
                return new CdInstruction(pieces[1], pieces[2]);
            return new LsInstruction();
        }
    }
}