namespace Advent2021;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        int steps = Part1 ? 10 : 40;

        Dictionary<(char, char), ((char, char) first, (char, char) second)> translations = new();
        Dictionary<(char, char), long> basePairs = new(), pairs;
        Dictionary<char, long> counts = new();

        foreach (string formula in Inputs[2..])
        {
            translations[(formula[0], formula[1])] = ((formula[0], formula[6]), (formula[6], formula[1]));
            basePairs[(formula[0], formula[1])] = 0;
            counts[formula[6]] = 0;
        }
        pairs = new(basePairs);
        for (int i = 0; i < Inputs[0].Length - 1; i++)
            pairs[(Inputs[0][i], Inputs[0][i + 1])]++;

        for (int i = 0; i < steps; i++)
        {
            Dictionary<(char, char), long> newPairs = new(basePairs);
            foreach ((char, char) key in pairs.Keys)
            {
                newPairs[translations[key].first] += pairs[key];
                newPairs[translations[key].second] += pairs[key];
            }
            pairs = new(newPairs);
        }
        
        foreach((char first,char second) key in pairs.Keys)
        {
            counts[key.first] += pairs[key];
            counts[key.second] += pairs[key];
        }

        Output = ((counts.Values.Max() - counts.Values.Min() + 1) / 2).ToString();
    }

    public void DoWorkNaive()
    {
        List<char> polymer = Inputs[0].ToCharArray().ToList();
        Dictionary<(char, char), char> formulae = new();
        Dictionary<char, long> counts = new();
        int steps = Part1 ? 10 : 40;

        foreach (string formula in Inputs[2..])
            formulae[(formula[0], formula[1])] = formula[6];

        for (int step = 0; step < steps; step++)
        {
            int limit = (polymer.Count * 2) - 2;
            for (int i = 0; i < limit; i += 2)
            {
                polymer.Insert(i + 1, formulae[(polymer[i], polymer[i + 1])]);
            }
        }

        foreach (char c in polymer)
            if (counts.ContainsKey(c))
                counts[c]++;
            else
                counts[c] = 1;

        Output = (counts.Values.Max() - counts.Values.Min()).ToString();
    }
}
