namespace Advent2015;

public partial class Day12 : Advent.Day
{
    public override void DoWork()
    {
        int sum = 0;
        string work = string.Join(',', Inputs).Replace("\"", "");

        if (Part2)
        {
            while (work.IndexOf("red") > 0)
            {
                int redPos = work.IndexOf("red");
                int prevBracket = redPos, nextBracket = redPos;

                int unmatched = 0;
                work = string.Concat(work.AsSpan(0, redPos), "rrr", work.AsSpan(redPos + 3, work.Length - redPos - 3));
                do
                {
                    prevBracket = work.LastIndexOfAny(new char[] { '[', '{', ']', '}' }, prevBracket - 1, prevBracket - 1);
                    if (work[prevBracket] == '}' || work[prevBracket] == ']') unmatched++;
                    if (work[prevBracket] == '{' || work[prevBracket] == '[') unmatched--;
                } while (unmatched != -1);
                unmatched = 0;
                do
                {
                    nextBracket = work.IndexOfAny(new char[] { '[', '{', ']', '}' }, nextBracket + 1);
                    if (work[nextBracket] == '{' || work[nextBracket] == '[') unmatched++;
                    if (work[nextBracket] == '}' || work[nextBracket] == ']') unmatched--;
                } while (unmatched != -1);

                if (work[prevBracket] == '{' && work[nextBracket] == '}')
                    work = work.Remove(prevBracket, nextBracket - prevBracket + 1 - (nextBracket == work.Length ? 1 : 0));
            }
        }

        work = Regex.Replace(work, "[^\\d-;,]", "");    // Replace everything that's not a number with a comma
        foreach (string num in work.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            sum += int.Parse(num);

        Output = sum.ToString();
    }
}
