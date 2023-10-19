namespace Advent2018;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        List<int> scores = new() { 3, 7 };
        string testString = "37";
        int elf1Pos = 0; int elf2Pos = 1, pos = 0, limit = int.Parse(Input);
        bool foundIt = false;
        int[] newScores = new int[2];

        do
        {
            int sum = scores[elf1Pos] + scores[elf2Pos];
            newScores[0] = sum / 10; newScores[1] = sum % 10;
            for (int i = 0; i < 2; i++)
                if (i == 1 || newScores[i] != 0)
                {
                    scores.Add(newScores[i]);
                    testString += newScores[i];
                    if (testString.Length > limit.ToString().Length)
                    {
                        testString = testString[1..];
                        pos++;
                    }
                    foundIt = testString == Input;
                    if (Part2 && foundIt) break;
                }
            if (Part2 && foundIt) break;
            elf1Pos = (elf1Pos + scores[elf1Pos] + 1) % scores.Count;
            elf2Pos = (elf2Pos + scores[elf2Pos] + 1) % scores.Count;
        } while ((Part2 && !foundIt) || (Part1 && scores.Count < limit + 11));

        Output = Part1 ? string.Join("", scores).Substring(limit, 10) : pos.ToString();
    }
}
