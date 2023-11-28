namespace Advent2018;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        List<long> scores = new() { 3, 7 };
        string testString = "37";
        int elf1Pos = 0, elf2Pos = 1, pos = 0;
        long limit = InputNumbers[0][0];
        bool foundIt = false;
        long[] newScores = new long[2];

        do
        {
            long sum = scores[elf1Pos] + scores[elf2Pos];
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
                    foundIt = testString == InputNumbers[0][0].ToString();
                    if (Part2 && foundIt) break;
                }
            if (Part2 && foundIt) break;
            elf1Pos = (int)(((long)elf1Pos + scores[elf1Pos] + 1) % scores.Count);
            elf2Pos = (int)(((long)elf2Pos + scores[elf2Pos] + 1) % scores.Count);
        } while ((Part2 && !foundIt) || (Part1 && scores.Count < limit + 11));

        Output = Part1 ? string.Join("", scores).Substring((int)limit, 10) : pos.ToString();
    }
}
