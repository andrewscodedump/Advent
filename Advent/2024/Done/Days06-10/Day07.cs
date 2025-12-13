namespace Advent2024;

public partial class Day07 : Advent.Day
{
    public override void DoWork()
    {
        long result = 0;
        foreach (long[] sum in InputNumbers)
        {
            long expected = sum[0], first = sum[1];
            Stack<(long, int, char)> dfs = new();
            dfs.Push((first, 2, '*'));
            dfs.Push((first, 2, '+'));
            if (Part2) dfs.Push((first, 2, '|'));
            bool validSum = false;
            do
            {
                (long resultSoFar, int nextPointer, char op) = dfs.Pop();
                long nextNumber = sum[nextPointer];
                long newTotal= op switch
                {
                    '*' => resultSoFar * nextNumber,
                    '+' => resultSoFar + nextNumber,
                    '|' => (resultSoFar * (long)Math.Pow(10, nextNumber.ToString().Length)) + nextNumber,
                    _ => resultSoFar
                };
                if (newTotal == expected && nextPointer == sum.Length - 1) { validSum = true; break; }
                if (nextPointer == sum.Length - 1) continue;
                if (newTotal <= expected)
                {
                    dfs.Push((newTotal, ++nextPointer, '*'));
                    dfs.Push((newTotal, nextPointer, '+'));
                    if (Part2) dfs.Push((newTotal, nextPointer, '|'));
                }
            } while (dfs.Count > 0);
            if (validSum) result += expected;
        }
        Output = result.ToString();
    }
}