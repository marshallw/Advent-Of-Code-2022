using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Day7
{
    public class Folder
    {
        public Folder _parent { get; set; }
        public List<Folder> _folders { get; private set; }
        public List<ElfFile> _files { get; private set; }
        public string Name { get; }
        private int _maxSize { get; }

        public Folder(string name, Folder parent, int maxSize)
        {
            Name = name;
            _maxSize = maxSize;
            _parent = parent;
            _folders = new List<Folder>();
            _files = new List<ElfFile>();
        }

        public void AddFolder(Folder folder) => _folders.Add(folder);
        public void AddFile(ElfFile file) => _files.Add(file);
        public Folder Parent() => _parent;

        public int GetSize()
        {
            return _folders.Select(folder => folder.GetSize()).Sum() + _files.Select(file => file.Size).Sum();
        }

        public IEnumerable<int> GetSizeWithCap()
        {
            var sizes = _folders.Select(folder => folder.GetSizeWithCap()).SelectMany(group => group).ToList();
            sizes.Add(sizes.Sum() + _files.Sum(file => file.Size));
            return sizes;
        }

        public long Size()
        {
            return _files.Sum(file => file.Size) + _folders.Sum(folder => folder.Size());
        }

        public bool IsBiggerThanMaxSize()
        {
            return GetSize() > _maxSize;
        }

        public Folder GetFolder(string name)
        {
            return _folders.First(folder => folder.Name == name);
        }

        public bool HasFolder(string name)
        {
            return _folders.Any(folder => folder.Name == name);
        }

        public static Folder NoParent() => new Folder("", null, 0);
    }
}
