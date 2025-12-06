namespace Advent2025;

public class Day06 : Advent.Day
{
    public override void DoWork()
    {
        long total = 0;

        if (WhichPart == 1)
        {
            string[] operators = [.. Inputs[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries)];
            for (int i = 0; i < operators.Length; i++)
                total += DoSum(InputNumbers.Select(n => n[i]), operators[i][0]);
        }
        else
        {
            List<long> numbers = [];
            for (int c = Inputs[0].Length - 1; c >= 0; c--)
            {
                char[] column = [.. Inputs.Select(i => i[c])], digits = [.. column.Where(c => !" *+".Contains(c))];
                if (digits.Length == 0) continue;
                char op = column[^1];
                numbers.Add(long.Parse(string.Join("", digits)));
                if (op == ' ') continue;
                total += DoSum(numbers, op);
                numbers = [];
            }
        }
        Output = total;
    }

    private static long DoSum(IEnumerable<long> input, char op) => op switch { '+' => input.Sum(), _ => input.Aggregate(1, (long r, long n) => r * n) };
}
