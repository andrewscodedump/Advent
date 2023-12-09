namespace Advent2023;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        long part1 = 0, part2 = 0;

        foreach (long[] history in InputNumbers)
        {
            long next = 0, prev = 0;
            long[] nextLine = history, lastDigits = [], firstDigits = [];
            do
            {
                firstDigits = [nextLine[0], .. firstDigits];
                lastDigits = [nextLine[^1], .. lastDigits];
                nextLine = [.. nextLine.Zip(nextLine.Skip(1), (prev, next) => next - prev)];
            } while (nextLine.Any(h => h != 0));

            for (int i = 0; i < lastDigits.Length; i++)
            {
                next += lastDigits[i];
                prev = firstDigits[i] - prev;
            }

            part1 += next;
            part2 += prev;
        }
        Output = (Part1 ? part1 : part2).ToString();
    }
}
