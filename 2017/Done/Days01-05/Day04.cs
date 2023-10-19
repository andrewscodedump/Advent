namespace Advent2017;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int valid = 0;
        foreach (string line in InputSplit)
        {
            List<string> words = new();
            bool matchFound = false;
            foreach (string word in line.Split(' '))
            {
                string testWord = Part1 ? word : Sort(word);
                matchFound = words.Contains(testWord);
                if (matchFound) break;
                words.Add(testWord);
            }
            if (!matchFound) valid++;
        }
        Output = valid.ToString();
    }

    private static string Sort(string s)
    {
        char[] a = s.ToCharArray();
        Array.Sort(a);
        return new string(a);
    }
}
