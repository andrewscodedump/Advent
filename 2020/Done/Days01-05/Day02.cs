namespace Advent2020;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        int numOK = 0, min, max;
        string letter, password;

        foreach (string test in InputSplit)
        {
            string[] bits = test.Split(new char[] { ' ', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);
            min = int.Parse(bits[0]); max = int.Parse(bits[1]); letter = bits[2]; password = bits[3];
            int diff = password.Length - password.Replace(letter, "").Length;
            if ((WhichPart == 1 && diff >= min && diff <= max) || (WhichPart == 2 && (password[min - 1] == letter[0] ^ password[max - 1] == letter[0])))
                numOK++;
        }
        Output = numOK.ToString();
    }
}
