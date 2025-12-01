namespace Advent2016;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int sectorTotal = 0;

        foreach (string code in Inputs)
        {
            StringBuilder totalCode = new(), check = new();
            string checkSum = "";
            int sector = 0;
            Dictionary<char, int> letters = [];
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
                    totalCode.Append(bits[i] + ' ');
                    foreach (char letter in bits[i])
                    {
                        if (letters.TryGetValue(letter, out int value))
                            letters[letter] = ++value;
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
                check.Append(bestLetter);
                letters.Remove(bestLetter);
            }
            if (check.ToString() == checkSum)
                if (Part1)
                    sectorTotal += sector;
                else
                {
                    StringBuilder decrypted = new();
                    foreach (char letter in totalCode.ToString())
                    {
                        if (letter == ' ')
                            decrypted.Append(letter);
                        else
                        {
                            int newChar = Encoding.ASCII.GetBytes(letter.ToString())[0] + (sector % 26);
                            if (newChar > 122) newChar -= 26;
                            decrypted.Append((char)newChar);
                        }
                    }
                    if ((TestMode && decrypted.ToString().StartsWith("very "))
                        ||  (!TestMode && decrypted.ToString().Contains("north")))
                        sectorTotal = sector;
                }
        }
        Output = sectorTotal.ToString();
    }
}
