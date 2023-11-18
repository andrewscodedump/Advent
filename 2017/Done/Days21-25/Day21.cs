using System;

namespace Advent2017;

public partial class Day21 : Advent.Day
{
    public override void DoWork()
    {
        int iterations = TestMode ? 2 : Part2 ? 18 : 5;
        List<string> pattern = new();
        Dictionary<string, string> rules = new();

        pattern.AddRange(new List<string> { ".#.", "..#", "###" });

        foreach (string rule in Inputs)
        {
            string[] bits = rule.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);
            (string trigger, string result) = (bits[0], bits[1]);
            rules[trigger] = result; rules[FlipV(trigger)] = result; rules[FlipH(trigger)] = result;
            rules[RotateC(trigger)] = result; rules[RotateC(RotateC(trigger))] = result; rules[RotateC(RotateC(RotateC(trigger)))] = result;
            rules[FlipH(RotateC(trigger))] = result; rules[FlipH(RotateC(RotateC(RotateC(trigger))))] = result;
        }

        for (int i = 0; i < iterations; i++) pattern = Process(pattern, rules);

        Output = pattern.Select(r => r.Replace(".", "").Length).Sum().ToString();
    }

    private static List<string> Process(List<string> pattern, Dictionary<string, string> rules)
    {
        List<string> newPattern = new();
        int patternSize = pattern[0].Length;
        int blockSize = patternSize % 2 == 0 ? 2 : 3;

        for (int row = 0; row < patternSize; row += blockSize)
            for (int col = 0; col < patternSize; col += blockSize)
            {
                string[] bits = rules[string.Concat(pattern[row].AsSpan(col, blockSize), "/", pattern[row + 1].AsSpan(col, blockSize), blockSize == 3 ? string.Concat("/", pattern[row + 2].AsSpan(col, blockSize)) : "")].Split('/');
                for (int bit = 0; bit < bits.Length; bit++)
                    if (newPattern.Count > (row * (blockSize + 1) / blockSize) + bit)
                        newPattern[(row * (blockSize + 1) / blockSize) + bit] += bits[bit];
                    else
                        newPattern.Add(bits[bit]);
            }
        return newPattern;
    }

    private static string FlipH(string block)
    {
        string[] bits = block.Split('/');
        return new string(bits[0].Reverse().ToArray()) + "/" + new string(bits[1].Reverse().ToArray()) + (bits.Length == 3 ? "/" + new string(bits[2].Reverse().ToArray()) : "");
    }
    private static string FlipV(string block)
    {
        string[] bits = block.Split('/');
        return bits[^1] + "/" + bits[^2] + (bits.Length == 3 ? "/" + bits[0] : "");
    }

    private static string RotateC(string block)
    {
        string newString = string.Empty;
        string[] bits = block.Split('/');
        for (int i = 0; i < bits.Length; i++)
        {
            newString += "/";
            for (int j = bits.Length - 1; j >= 0; j--)
                newString += bits[j][i];
        }
        return newString[1..];
    }
}
