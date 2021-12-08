namespace Advent2021;

public partial class Day08 : Advent.Day
{
    readonly Dictionary<string, int> values = new() { { "abcefg", 0 }, { "cf", 1 }, { "acdeg", 2 }, { "acdfg", 3 }, { "bcdf", 4 }, { "abdfg", 5 }, { "abdefg", 6 }, { "acf", 7 }, { "abcdefg", 8 }, { "abcdfg", 9 } };

    public override void DoWork()
    {
        int result = 0;
        foreach (string line in InputSplit)
        {
            string[] io = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
            string[] outputs = io[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Dictionary<char,char> translations = new();

            if (WhichPart == 1)
            {
                result += outputs.Where(o => o.Length == 2 || o.Length == 3 || o.Length == 4 || o.Length == 7).Count();
            }
            else
            {
                string[] inputs = io[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                translations = GetTranslations(inputs);

                for (int i = 0; i <= 3; i++)
                    result += Translate(outputs[i], translations) * (int)Math.Pow(10, 3 - i);
            }
        }
        Output = result.ToString();
    }

    private static Dictionary<char, char> GetTranslations(string[] inputs)
    {
        string[] interim = new string[9];
        string[] commonLetters = Enumerable.Repeat("abcdefg", 8).ToArray();
        Dictionary<char, char> translations = new();
        foreach (string input in inputs)
        {
            string curVal = commonLetters[input.Length];
            for (int i = curVal.Length - 1; i >= 0; i--)
                if (!input.Contains(curVal[i])) curVal = curVal.Remove(i, 1);
            commonLetters[input.Length] = curVal;
        }

        // Common letters for a given length: C2:cf; C3:acf; C4:bcdf; C5:adg; C6:abfg; C7:abcdefg
        // Translation Algorithm: a=C3-C2; e=C7-C6-C4; d=C7-(e)-C6-C2; g=C5-(ad); b=C4-C2-(d); f=C6-(abg); c=C2-(f)
        // Where Cn = common letters for length n, (nnn) = already translated values
        interim[0] = SubtractString(commonLetters[3], commonLetters[2]);
        interim[4] = SubtractStrings(commonLetters[7], new string[] { commonLetters[6], commonLetters[4] });
        interim[3] = SubtractStrings(commonLetters[7], new string[] { interim[4], commonLetters[6], commonLetters[2] });
        interim[6] = SubtractStrings(commonLetters[5], new string[] { interim[0], interim[3] });
        interim[1] = SubtractStrings(commonLetters[4], new string[] { commonLetters[2], interim[3] });
        interim[5] = SubtractStrings(commonLetters[6], new string[] { interim[0], interim[1], interim[6] });
        interim[2] = SubtractString(commonLetters[2], interim[5]);

        for(int i = 0; i < 7; i++)
            translations[interim[i][0]] = (char)(97 + i);
        return translations;
    }

    private static string SubtractString(string input, string toRemove) => SubtractStrings(input, new string[] { toRemove });

    private static string SubtractStrings(string input, string[] toRemove)
    {
        string output = input;
        foreach (string word in toRemove)
            foreach (char c in word)
                output = output.Replace(c.ToString(), "");
        return output;
    }

    private int Translate(string input, Dictionary<char, char> translations)
    {
        string translated = string.Empty;
        foreach(char c in input)
            translated += translations[c];
        return values[SortString(translated)];
    }

    private static string SortString(string input)
    {
        char[] characters = input.ToArray();
        Array.Sort(characters);
        return new string(characters);
    }
}
