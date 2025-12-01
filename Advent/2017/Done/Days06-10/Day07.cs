using System.Linq;

namespace Advent2017;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<string, Program> programs = [];

        string[] splitter = [" ", ",", "(", ")", "->"];
        string name = string.Empty;
        int size;
        int correctWeight = 0;

        foreach (string line in Inputs)
        {
            string[] words = line.Split(splitter, StringSplitOptions.RemoveEmptyEntries);


            name = words[0];
            size = int.Parse(words[1]);
            if (programs.TryGetValue(name, out Program value))
            {
                value.Size = size;
                value.TotalSize = size;
            }
            else
                programs.Add(name, new Program(size));
            for (int word = 2; word < words.Length; word++)
            {
                string childName = words[word];
                if (programs.TryGetValue(childName, out Program value2))
                    value2.Parent = name;
                else
                    programs.Add(childName, new Program(name));
            }
        }

        name = programs.First(p=> string.IsNullOrEmpty(p.Value.Parent)).Key;

        GetTotalWeight(programs, name);

        foreach (Program program in programs.Values)
        {
            // If I'm balanced, but parent isn't, then I might be the problem
            if (program.NumberAtMax == program.NumberAtMin && programs[program.Parent].NumberAtMin != programs[program.Parent].NumberAtMax)
            {
                // If I'm the only one at that weight, I definitely am
                if (program.TotalSize == programs[program.Parent].MinWeight && programs[program.Parent].NumberAtMin == 1)
                    correctWeight = program.Size + (programs[program.Parent].MaxWeight - programs[program.Parent].MinWeight);
                if (program.TotalSize == programs[program.Parent].MaxWeight && programs[program.Parent].NumberAtMax == 1)
                    correctWeight = program.Size - (programs[program.Parent].MaxWeight - programs[program.Parent].MinWeight);
            }

        }

        Output = Part1 ? name.ToString() : correctWeight.ToString();
    }

    private static void GetTotalWeight(Dictionary<string, Program> programs, string name)
    {
        foreach (KeyValuePair<string, Program> program in programs)
        {
            if (program.Value.Parent == name)
            {
                GetTotalWeight(programs, program.Key);
                programs[name].TotalSize += program.Value.TotalSize;
                if (program.Value.TotalSize == programs[name].MaxWeight)
                {
                    programs[name].NumberAtMax++;
                }
                else if (program.Value.TotalSize > programs[name].MaxWeight)
                {
                    programs[name].MaxWeight = program.Value.TotalSize;
                    programs[name].NumberAtMax = 1;
                }
                if (program.Value.TotalSize == programs[name].MinWeight)
                {
                    programs[name].NumberAtMin++;
                }
                else if (program.Value.TotalSize < programs[name].MinWeight)
                {
                    programs[name].MinWeight = program.Value.TotalSize;
                    programs[name].NumberAtMin = 1;
                }
            }
        }
        programs[name].MaxWeight = programs[name].MaxWeight == 0 ? programs[name].Size : programs[name].MaxWeight;
        programs[name].MinWeight = programs[name].MinWeight == int.MaxValue ? programs[name].Size : programs[name].MinWeight;
    }
    private sealed class Program
    {
        public Program(string parent, int size)
        {
            Parent = parent;
            Size = size;
            TotalSize += size;
            MinWeight = int.MaxValue;
            MaxWeight = 0;
            NumberAtMax = 0;
            NumberAtMin = 0;
        }
        public Program(string parent) : this(parent, 0) { }
        public Program(int size) : this("", size) { }
        public string Parent { get; set; }
        public int Size { get; set; }
        public int TotalSize { get; set; }
        public int MaxWeight { get; set; }
        public int MinWeight { get; set; }
        public int NumberAtMax { get; set; }
        public int NumberAtMin { get; set; }

    }
}
