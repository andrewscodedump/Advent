namespace Advent2016;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int sectorTotal = 0;

        foreach (string code in Inputs)
        {
            string totalCode = "", checkSum = "", check = "";
            int sector = 0;
            Dictionary<char, int> letters = new();
            string[] bits = code.Split('-');
            for (int i = 0; i < bits.Length; i++)
            {
                if (i == bits.Length - 1)
                {
                    string[] split = bits[i].Split('[');
                    sector = int.Parse(split[0]);
                    checkSum = split[1][0..^1];
                }
                else
                {
                    totalCode += bits[i] + ' ';
                    foreach (char letter in bits[i])
                    {
                        if (letters.ContainsKey(letter))
                            letters[letter]++;
                        else
                            letters.Add(letter, 1);
                    }
                }
            }
            for (int i = 0; i <= 4; i++)
            {
                int bestCount = 0;
                char bestLetter = '{';
                foreach (char letter in letters.Keys)
                {
                    if (letters[letter] > bestCount || (letters[letter] == bestCount && letter < bestLetter))
                    {
                        bestLetter = letter;
                        bestCount = letters[letter];
                    }
                }
                check += bestLetter;
                letters.Remove(bestLetter);
            }
            if (check == checkSum)
                if (Part1)
                    sectorTotal += sector;
                else
                {
                    string decrypted = "";
                    foreach (char letter in totalCode)
                    {
                        if (letter == ' ')
                            decrypted += letter;
                        else
                        {
                            int newChar = Encoding.ASCII.GetBytes(letter.ToString())[0] + (sector % 26);
                            if (newChar > 122) newChar -= 26;
                            decrypted += (char)newChar;
                        }
                    }
                    if ((TestMode && decrypted.StartsWith("very "))
                        ||  (!TestMode && decrypted.Contains("north")))
                        sectorTotal = sector;
                }
        }
        Output = sectorTotal.ToString();
    }
}
