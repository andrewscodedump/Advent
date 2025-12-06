namespace Advent2025;

public class Day06 : Advent.Day
{
    public override void DoWork()
    {
        long total = 0;

        if (WhichPart == 1) total = Inputs[^1].Where(c => c != ' ').Select((op, i) => DoSum(InputNumbers.Select(n => n[i]), op)).Sum();

        else
        {
            List<long> numbers = [];
            for (int c = Inputs[0].Length - 1; c >= 0; c--)
            {
                char[] column = [.. Inputs.Select(i => i[c]).Where(v => v != ' ')];
                if (column.Length == 0) continue;
                numbers.Add(long.Parse(string.Join("", column.Where(c => !IsOp(c)))));
                if (!IsOp(column[^1])) continue;
                total += DoSum(numbers, column[^1]);
                numbers = [];
            }
        }
        Output = total;
    }

    private static long DoSum(IEnumerable<long> input, char op) => op switch { '+' => input.Sum(), _ => input.Aggregate(1, (long r, long n) => r * n) };
    private static bool IsOp(char input) => input == '+' || input == '*';
}
