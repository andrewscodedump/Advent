namespace Advent2015;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        string word = GetNext(Input);
        if (Part2) word = GetNext(word);
        Output = word;
    }

    private string GetNext(string input)
    {
        byte[] word = Encoding.ASCII.GetBytes(input);
        do
            IncLetter(word, 7);
        while (!CheckWord(word));
        return Encoding.ASCII.GetString(word);
    }

    private static bool CheckWord(byte[] word)
    {
        bool hasDouble = false;
        if (!word.Any(s => s == 105 || s == 108 || s == 111))
            for (int i = 0; i < word.Length - 2; i++)
                if (word[i + 1] == word[i] + 1 && word[i + 2] == word[i] + 2)
                {
                    for (int j = 0; j < word.Length - 1; j++)
                        if (word[j] == word[j + 1])
                            if (!hasDouble)
                            {
                                hasDouble = true;
                                j++;
                            }
                            else
                                return true;
                    break;
                }
        return false;
    }

    private void IncLetter(byte[] word, int pos)
    {
        int letter = ((word[pos] - 96) % 26) + 97;
        if (letter == 105 || letter == 108 || letter == 111) letter++;
        else if (letter == 97) IncLetter(word, pos - 1);
        word[pos] = (byte)letter;
    }
}
