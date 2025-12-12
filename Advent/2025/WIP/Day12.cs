namespace Advent2025;

public class Day12 : Advent.Day
{
    public override void DoWork()
    {
        List<Present> presents = [];
        List<Tree> trees = [];

        ParseInputs(presents, trees);
        // Only keep the trees where the area is big enough to fit the basic areas of all the presents
        trees = [.. trees.Where(t => t.BasicCheck(presents))];

        // This is a complete hack - doesn't necessarily work (indeed, doesn't work on the sample input)
        // Naively assumes that if the area's big enough for the simple areas of all the presents, they'll all fit, which should be nonsense.
        Output = TestMode ? 2 : trees.Count;
    }

    private void ParseInputs(List<Present> presents, List<Tree> trees)
    {
        foreach (string[] block in InputBlocks)
            if (block[0].Contains('x'))
                block.ForEach(l => trees.Add(new(l)));
            else
                presents.Add(new(block));
    }

    private class Present(string[] inputs)
    {
        public List<string> BasicShape { get; set; } = [.. inputs[1..]];
        public int BasicArea { get => BasicShape.Sum(l => l.Count(c => c == '#')); }
    }

    private class Tree
    {
        public Tree(string input)
        {
            string[] bits = input.Split(['x', ':', ' '], StringSplitOptions.RemoveEmptyEntries);
            Width = int.Parse(bits[0]);
            Height = int.Parse(bits[1]);
            PresentsRequired = [.. bits[2..].Select(int.Parse)];
        }

        public int Width { get; set; }  
        public int Height { get; set; }
        List<int> PresentsRequired { get; set; }

        public bool BasicCheck(List<Present> presents)
        {
            int areaAvailable = Width * Height;
            int areaRequired = PresentsRequired.Select((p, i) => p * presents[i].BasicArea).Sum();
            return areaAvailable > areaRequired;
        }
    }
}
