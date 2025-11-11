namespace Everybody2025;

public class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int pairs = 0, repeats = WhichPart == 3 ? 1000 : 1, limit = 1000;
        ReadOnlySpan<char> line = new(string.Concat(Enumerable.Repeat(Input, repeats)).ToCharArray());
        for (int i = 1; i < line.Length; i++)
        {
            if (line[i] < 'a' || (WhichPart == 1 && Input[i] != 'a')) continue;
            int start = 0, end = i;
            if (WhichPart == 3)
            {
                start = Math.Max(0, i - limit);
                end = Math.Min(line.Length, i + limit + 1);
            }
            pairs += line[start..end].Count((char)(line[i] - 32));
        }
        Output = pairs.ToString();
    }
}
