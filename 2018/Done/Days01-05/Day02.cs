namespace Advent2018;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        (int twos, int threes) numbers = (0, 0);
        string common = string.Empty;
        string[] sorted = InputSplit.OrderBy(s=>s).ToArray();

        foreach (string boxID in InputSplit)
            numbers = (numbers.twos += HasN(boxID, 2), numbers.threes += HasN(boxID, 3));

        for (int id = 0; id < sorted.Length - 1 && string.IsNullOrEmpty(common); id++)
        {
            string curr = sorted[id]; string next = sorted[id + 1];
            if (curr == next) continue;
            int diffPos = 0;
            for (int pos = 0; pos < curr.Length; pos++)
            {
                if (curr[pos] != next[pos])
                    diffPos = diffPos == 0 ? pos : -1;
                if (diffPos == -1) break;
            }
            if (diffPos != -1)
            {
                common = curr.Remove(diffPos, 1);
                break;
            }
        }
        Output = Part1 ? (numbers.twos * numbers.threes).ToString() : common;
    }

    private static int HasN(string boxID, int n)
    {
        char[] letters = boxID.ToCharArray();

        foreach (char letter in boxID)
            if (letters.Count(l => l == letter) == n) return 1;
        return 0;
    }
}
