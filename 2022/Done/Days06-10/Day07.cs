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
                            switch (bits[2])
                            {
                                case "/":
                                    currentDirectory = root;
                                    break;
                                case "..":
                                    currentDirectory = currentDirectory.Parent;
                                    break;
                                default:
                                    currentDirectory = currentDirectory.ChangeDirectory(bits[2]);
                                    break;
                            }
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
        if (Part1) result = getSmallDirs(root, 100_000);
        else result = getDirToDelete(root, sizeRequired, long.MaxValue);

        Output = result.ToString();
    }

    private class ElfDir
    {
        public ElfDir(string name)
        {
            Name = name;
            Children = new();
        }

        public ElfDir(string name, ElfDir parent)
        {
            Name = name;
            Children = new();
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
            if(Parent is not null) Parent.AddFile(size);
        }
        public ElfDir ChangeDirectory(string name)
        {
            Children.Add(new(name, this));
            return Children.FirstOrDefault(n=>n.Name == name);
        }
    }

    long getSmallDirs(ElfDir dir, long targetSize)
    {
        long result = 0;
        if (dir.Size < targetSize)
            result = dir.Size;
        foreach(ElfDir child in dir.Children)
        { 
            result+= getSmallDirs(child, targetSize);
        }
        return result;
    }

    long getDirToDelete(ElfDir dir, long sizeRequired, long currentSmallest)
    {
        long result = currentSmallest;
        if (dir.Size >= sizeRequired && dir.Size < currentSmallest)
            result = dir.Size;
        foreach(ElfDir child in dir.Children)
        {
            result = getDirToDelete(child, sizeRequired, result);
        }
        return result;
    }
}

