namespace Advent2019;

public partial class Day04 : Advent.Day
{
    public override void DoWork()
    {
        int from = int.Parse(Input.Split('-')[0]);
        int to = int.Parse(Input.Split('-')[1]);

        int number = 0;
        for (int i = from; i <= to; number += CheckNumber(i++))
            Output = number.ToString();
    }

    private int CheckNumber(int number)
    {
        string numberString = number.ToString();
        int prevNum = 48;
        for (int pos = 0; pos < 6; pos++)
        {
            char curNum = numberString[pos];
            if (curNum < prevNum) return 0;
            prevNum = curNum;
        }
        for (int i = 48; i <= 57; i++)
            if (numberString.Contains(new string((char)i, 2)) && (Part1 || !numberString.Contains(new string((char)i, 3))))
                return 1;
        return 0;
    }
}
