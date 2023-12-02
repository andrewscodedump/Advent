namespace Advent2023;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;
        char[] setSeparator = [':', ';'], wordSeparator = [' ', ','];
        Dictionary<string, Colour> colours = new() { { "red", new(12) }, { "green", new(13) }, { "blue", new(14) } };
        foreach (string game in Inputs)
        {
            int id = 0;
            colours.ForEach(c => c.Value.Max = 0);
            bool possible = true;
            foreach (string set in game.Split(setSeparator))
            {
                colours.ForEach(c => c.Value.Sum = 0);
                string[] words = set.Split(wordSeparator, StringSplitOptions.RemoveEmptyEntries);
                if (words[0] == "Game") { id = int.Parse(words[1]); continue; }
                for (int i = 0; i < words.Length; i += 2)
                {
                    int number = int.Parse(words[i]);
                    string colour = words[i + 1];
                    colours[colour].Sum += number;
                    if (number > colours[colour].Target) possible = false;
                }
                colours.ForEach(c => c.Value.SetMax());
            }
            result += Part1 && possible ? id : Part2 ? colours.Select(c => c.Value.Max).Aggregate((p, v) => p * v) : 0;
        }
        Output = result.ToString();
    }
    private class Colour(int target)
    {
        public int Target { get; set; } = target;
        public int Max { get; set; }
        public int Sum { get; set; }
        public void SetMax() { Max = Math.Max(Max, Sum); }

    }
}
