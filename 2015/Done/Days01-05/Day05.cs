namespace Advent2015;

public partial class Day05 : Advent.Day
{
    public override void DoWork()
    {
        int niceNumbers = 0;
        foreach (string word in Inputs[0].Split(','))
            niceNumbers += IsNice(word) ? 1 : 0;
        Output = niceNumbers.ToString();
    }

    private bool IsNice(string testString)
    {
        int vowels = 0;
        bool pair = false;
        bool doublePair = false;

        if (Part1 && (testString.Contains("ab") || testString.Contains("cd") || testString.Contains("pq") || testString.Contains("xy")))
            return false;

        if (Part1)
        {
            for (int i = 0; i < testString.Length; i++)
            {
                {
                    if (testString[i] == 'a' || testString[i] == 'e' || testString[i] == 'i' || testString[i] == 'o' || testString[i] == 'u')
                        vowels++;
                    if (!pair && i < testString.Length - 1 && testString[i] == testString[i + 1])
                        pair = true;
                    if (vowels >= 3 && pair)
                        return true;
                }
            }
            return false;
        }
        else
        {
            for (int i = 0; i < testString.Length - 2; i++)
            {
                if (testString[i] == testString[i + 2])
                {
                    pair = true;
                    break;
                }
            }

            if (pair)
            {
                for (int i = 0; i < testString.Length - 3; i++)
                {
                    string testPair = testString[i].ToString() + testString[i + 1].ToString();
                    for (int j = i + 2; j < testString.Length - 1; j++)
                        if (testString[j].ToString() + testString[j + 1] == testPair)
                        {
                            doublePair = true;
                            break;
                        }
                }
            }
            return pair && doublePair;
        }
    }
}
