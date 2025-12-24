namespace Advent2015;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int salt = 0, targetLength = WhichPart + 4;
        string zeroes = "".PadRight(targetLength, '0');
        while (GetMD5Hash(MD5, Input + salt.ToString())[..targetLength] != zeroes) { salt++; } 
        Output = salt.ToString();
    }
}
