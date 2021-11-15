namespace Advent2020;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        int[] cables = InputSplit.Select(int.Parse).OrderBy(n => n).ToArray();
        int ones = Math.Abs(cables[0] - 2), threes = 1;
        int device = cables.Max() + 3;

        for (int cable = 0; cable < cables.Length - 1; cable++)
            if (cables[cable + 1] - cables[cable] == 1) ones++;
            else if (cables[cable + 1] - cables[cable] == 3) threes++;

        Dictionary<int, long> combinations = new() { { 0, 1 } };
        for (int endPoint = 1; endPoint <= device; endPoint++)
        {
            // Number of ways of getting to n is 0 if n doesn't exist else number of ways of getting to (n-3) + (n-2) + (n-1)
            if (cables.Contains(endPoint) || endPoint == device)
            {
                long num = 0;
                if (combinations.ContainsKey(endPoint - 3)) num += combinations[endPoint - 3];
                if (combinations.ContainsKey(endPoint - 2)) num += combinations[endPoint - 2];
                if (combinations.ContainsKey(endPoint - 1)) num += combinations[endPoint - 1];
                combinations.Add(endPoint, num);
            }
        }

        Output = (WhichPart == 1 ? (ones * threes) : combinations[device]).ToString();
    }
}
