namespace Advent2015;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int salt = -1;
        while (GetMD5Hash(MD5, Inputs[0] + (++salt).ToString())[..(WhichPart + 4)] != "".PadRight(WhichPart + 4, '0')) {} 
        Output = salt.ToString();
    }
}
