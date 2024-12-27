namespace Advent2022;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        ElfDir root = new("/");
        ElfDir currentDirectory = root;
        foreach (string command in Inputs)
        {
            string[] bits = command.Split(' ');
            switch (bits[0])
            {
                case "$":
                    {
                        if (bits[1] == "cd")
                        {
                            currentDirectory = bits[2] switch
                            {
                                "/" => root,
                                ".." => currentDirectory.Parent,
                                _ => currentDirectory.ChangeDirectory(bits[2]),
                            };
                        }
                    }
                    break;
                case "dir":
                    break;
                default:
                    currentDirectory.AddFile(long.Parse(bits[0]));
                    break;

            }
        }

        long sizeRequired = 30_000_000 - (70_000_000 - root.Size);
        long result;
        if (Part1) result = GetSmallDirs(root, 100_000);
        else result = GetDirToDelete(root, sizeRequired, long.MaxValue);

        Output = result.ToString();
    }

    private sealed class ElfDir
    {
        public ElfDir(string name)
        {
            Name = name;
            Children = [];
        }

        public ElfDir(string name, ElfDir parent)
        {
            Name = name;
            Children = [];
            Parent = parent;
        }
        public string Name { get; set; }
        public long Size { get; set; }
        public ElfDir Parent { get; set; }
        public HashSet<ElfDir> Children { get;set; }
        public override bool Equals(object obj) => Name.Equals(((ElfDir)obj).Name);
        public override int GetHashCode() => Name.GetHashCode();
        public void AddFile(long size)
        {
            //Assumes that we don't try adding the same file twice
            Size += size;
            Parent?.AddFile(size);
        }
        public ElfDir ChangeDirectory(string name)
        {
            Children.Add(new(name, this));
            return Children.FirstOrDefault(n=>n.Name == name);
        }
    }

    static long GetSmallDirs(ElfDir dir, long targetSize)
    {
        long result = 0;
        if (dir.Size < targetSize)
            result = dir.Size;
        foreach(ElfDir child in dir.Children)
        { 
            result+= GetSmallDirs(child, targetSize);
        }
        return result;
    }

    static long GetDirToDelete(ElfDir dir, long sizeRequired, long currentSmallest)
    {
        long result = currentSmallest;
        if (dir.Size >= sizeRequired && dir.Size < currentSmallest)
            result = dir.Size;
        foreach(ElfDir child in dir.Children)
        {
            result = GetDirToDelete(child, sizeRequired, result);
        }
        return result;
    }
}

