namespace Advent2024;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        string reg = @"mul\(\d{1,3}\,\d{1,3}\)|do\(\)|don\'t\(\)";
        long result = 0;
        bool enabled = true;

        string joinInputs = string.Join("", Inputs);
        foreach (Match match in Regex.Matches(joinInputs, reg))
        {
            if (match.ToString() == "do()") enabled = true;
            else if (match.ToString() == "don't()") enabled = false;
            else if (Part1 || enabled)
            {
                string[] bits = match.Value.Split([',', '(', ')'], StringSplitOptions.TrimEntries);
                long x = int.Parse(bits[1]), y = int.Parse(bits[2]);
                result += x * y;
            }
        }
        Output = result.ToString();
    }
}
