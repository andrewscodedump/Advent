using System;

namespace Advent2018;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        Dictionary<string, string> rules = new();
        for (int i = 1; i < InputSplit.Length; i++)
            rules.Add(InputSplit[i].Split(new char[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries)[0], InputSplit[i].Split(new char[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries)[1]);

        string currGen = InputSplit[0][15..];
        long maxGens = Part1 ? 20 : 50000000000;
        int currLeft = 0;
        long score = 0, lastScore = 0;
        long diff, prevDiff = 0;
        #endregion Setup Variables and Parse Inputs

        for (int gen = 1; gen <= maxGens; gen++)
        {
            StringBuilder nextGen = new();
            for (int pos = -2; pos < currGen.Length + 2; pos++)
            {
                string state;
                int distFromEnd = currGen.Length - pos;
                state = pos <= 1
                    ? new string('.', 2 - pos) + currGen[..(3 + pos)]
                    : distFromEnd <= 2
                    ? string.Concat(currGen.AsSpan(pos - 2, distFromEnd + 2), new string('.', 3 - distFromEnd))
                    : currGen.Substring(pos - 2, 5);
                nextGen.Append(rules.TryGetValue(state, out string newState) ? newState : ".");
            }
            currGen = nextGen.ToString();
            currLeft -= 2;

            score = 0;
            for (int pos = 0; pos < currGen.Length; pos++)
                score += currGen[pos].ToString() == "." ? 0 : pos + currLeft;
            diff = score - lastScore;
            if (diff == prevDiff)
            {
                score += (maxGens - (long)gen) * prevDiff;
                break;
            }
            prevDiff = diff;
            lastScore = score;
        }

        Output = score.ToString();
    }
}
