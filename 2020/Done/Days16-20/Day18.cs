using System;

namespace Advent2020;

public partial class Day18 : Advent.Day
{
    public override void DoWork() => Output = InputSplit.Sum(l => Calculate(ProcessPluses(ResolveBrackets(l)))).ToString();

    private static long Calculate(string sum)
    {
        string[] bits = sum.Split(new char[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        long result = long.Parse(bits[0]);
        for (int i = 0; i < bits.Length - 2; i += 2)
            result = bits[i + 1] == "*" ? result * long.Parse(bits[i + 2]) : result += long.Parse(bits[i + 2]);
        return result;
    }

    private string ResolveBrackets(string sum)
    {
        while (sum.Contains('('))
        {
            string bracket = Regex.Match(sum, @"(\((?:[^\(]*?\)))").ToString();
            sum = sum.Replace(bracket, Calculate(ProcessPluses(bracket)).ToString());
        }
        return sum;
    }

    private string ProcessPluses(string sum)
    {
        if (Part1) return sum;
        while (sum.Contains('+'))
        {
            Match match = Regex.Match(sum, @"(\d* \+ \d*)");
            sum = string.Concat(sum.AsSpan(0, match.Index), Calculate(match.Value).ToString(), sum.AsSpan(match.Index + match.Value.Length));
        }
        return sum;
    }
}
