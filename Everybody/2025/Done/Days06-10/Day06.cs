namespace Everybody2025;

public class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int pairs = 0;
        int repeats = WhichPart == 3 ? 1000 : 1, limit = 1000;
        string line = string.Concat(Enumerable.Repeat(Input, repeats));
        for (int i = 1; i < line.Length; i++)
        {
            if (line[i] < 97) continue;
            if (WhichPart == 1 && Input[i] != 'a') continue;
            int start = 0, end = i;
            if (WhichPart == 3)
            {
                start = Math.Max(0, i - limit);
                end = Math.Min(line.Length, i + limit + 1);
            }
            pairs += line[start..end].ToCharArray().Count(c => c == line[i] - 32);
        }
        Output = pairs.ToString();
    }
}
