namespace Advent2023;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;
        Dictionary<string, int> numbers = new() { { "1", 1 }, { "2", 2 }, { "3", 3 }, { "4", 4 }, { "5", 5 }, { "6", 6 }, { "7", 7 }, { "8", 8 }, { "9", 9 },
        { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 },};
        string matcher = Part1 ? @"\d" : string.Join('|', numbers.Keys);
        foreach (string line in Inputs)
        {
            string first = Regex.Match(line, matcher).Value;
            string last = Regex.Match(line, matcher, RegexOptions.RightToLeft).Value;
            result += (numbers[first] * 10) + numbers[last];
        }
        Output = result.ToString();
    }
}
