namespace Advent2015;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        if (TestMode && WhichPart == 2) return;
        int result = 0;
        char[] chars = Input.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == '(')
                result++;
            else if (chars[i] == ')')
                result--;
            if (WhichPart == 2 && result == -1)
            {
                result = i + 1;
                break;
            }
        }
        Output = result.ToString();
    }
}
