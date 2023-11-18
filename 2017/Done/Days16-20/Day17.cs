namespace Advent2017;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        long step = InputNumbersSingle[0];
        List<long> buffer = new() { 0 };
        long currPos = 0;
        int secondValue = 0;

        if (Part1)
            for (int i = 1; i <= 2017; i++)
            {
                currPos = ((currPos + step) % i) + 1;
                buffer.Insert((int)currPos, i);
            }
        else
            for (int i = 1; i <= 50000000; i++)
            {
                currPos = ((currPos + step) % i) + 1;
                if (currPos == 1)
                    secondValue = i;
            }

        Output = (Part1 ? buffer[(int)currPos + 1] : secondValue).ToString();
    }
}
