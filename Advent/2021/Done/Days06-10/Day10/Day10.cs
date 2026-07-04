namespace Advent2021;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        Dictionary<char, (long part1, long part2)> scores = new() { { ')', (3,1) }, { ']', (57,2) }, { '}', (1197,3) }, { '>', (25137,4) } };
        Dictionary<char, char> pairs = new() { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        Stack<char> openBrackets = new();
        long result = 0;
        List<long> results = [];

        foreach (string input in Inputs)
        {
            long interimResult = 0;
            foreach (char bracket in input)
                if (pairs.ContainsKey(bracket)) openBrackets.Push(bracket);
                else if (bracket != pairs[openBrackets.Pop()])
                {
                    interimResult += scores[bracket].part1;
                    openBrackets.Clear();
                    break;
                }

            if (interimResult != 0) result += interimResult;
            else if (Part2)
            {
                do
                    interimResult = (interimResult * 5) + scores[pairs[openBrackets.Pop()]].part2;
                while (openBrackets.Count > 0);
                results.Add(interimResult);
            }
        }

        Output = (Part1 ? result : results.OrderBy(x => x).ElementAt(results.Count / 2)).ToString();
    }
}
