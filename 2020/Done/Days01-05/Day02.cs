namespace Advent2020;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        int numOK = 0, min, max;
        string letter, password;
        char[] splitter = [' ', ':', '-'];

        foreach (string test in Inputs)
        {
            string[] bits = test.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            min = int.Parse(bits[0]); max = int.Parse(bits[1]); letter = bits[2]; password = bits[3];
            int diff = password.Length - password.Replace(letter, "").Length;
            if ((Part1 && diff >= min && diff <= max) || (Part2 && (password[min - 1] == letter[0] ^ password[max - 1] == letter[0])))
                numOK++;
        }
        Output = numOK.ToString();
    }
}
