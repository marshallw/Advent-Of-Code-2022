using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day7
{
    public class Day7
    {
        public string[] _input { get; }

        public Day7()
        {
            _input = File.ReadAllLines("Day7/input.txt");
        }

        public long Part1()
        {
            var maxSize = 100000;
            var root = new Folder("/", Folder.NoParent(), maxSize);
            var stack = new Stack<string>(_input.Reverse());
            var current = root;
            int dummy = 0;
            List<Folder> allFolders = new List<Folder>();
            allFolders.Add(root);

            while (stack.Any())
            {
                var line = stack.Pop();

                var instruction = InstructionFactory.Create(line);

                if (instruction.GetInstruction() == "cd")
                {
                    if (instruction.GetArgument() == "..")
                    {
                        current = current.Parent();
                    }
                    else if (instruction.GetArgument() == "/")
                    {
                        current = root;
                    }
                    else
                    {
                        var newFolder = current.GetFolder(instruction.GetArgument());
                        current = newFolder;
                    }
                }
                else if (instruction.GetInstruction() == "ls")
                {
                    while (stack.Count() > 0 && stack.Peek()[0] != '$')
                    {
                        var rawFile = stack.Pop().Split(' ');
                        if (Int32.TryParse(rawFile[0], out dummy))
                        {
                            current.AddFile(new ElfFile(rawFile[1], dummy));
                        }
                        if (rawFile[0] == "dir")
                        {
                            var newFolder = new Folder(rawFile[1], current, maxSize);
                            current.AddFolder(newFolder);
                            allFolders.Add(newFolder);
                        }
                    }
                }
            }

            //var result = root.GetSizeWithCap().Where(folder => folder <= maxSize);
            //return allFolders.Where(folder => folder.Size() <= maxSize).Sum(folder => folder.Size());
            //return result.Sum();
            Console.WriteLine(root.Size());
            var required = 30000000 - (70000000 - root.Size());
            Console.WriteLine(required);
            return allFolders.Where(folder => folder.Size() >= required).OrderBy(folder => folder.Size()).First().Size();
        }
    }
}
