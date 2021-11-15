namespace Advent2020;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        int result = 0;

        foreach (string group in InputSplit)
        {
            Dictionary<char, int> questions = new();
            foreach (string set in group.Split(','))
                foreach (char q in set)
                    questions[q] = questions.GetValueOrDefault(q) + 1;
            result += WhichPart == 1 ? questions.Count : questions.Values.Count(v => v == group.Split(',').Length);
        }

        Output = result.ToString();
    }
}
