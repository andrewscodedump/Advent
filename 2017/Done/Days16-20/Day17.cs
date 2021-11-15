namespace Advent2017;

public partial class Day17 : Advent.Day
{
    public override void DoWork()
    {
        int step = int.Parse(Input);
        List<int> buffer = new() { 0 };
        int currPos = 0;
        int secondValue = 0;

        if (WhichPart == 1)
            for (int i = 1; i <= 2017; i++)
            {
                currPos = ((currPos + step) % i) + 1;
                buffer.Insert(currPos, i);
            }
        else
            for (int i = 1; i <= 50000000; i++)
            {
                currPos = ((currPos + step) % i) + 1;
                if (currPos == 1)
                    secondValue = i;
            }

        Output = (WhichPart == 1 ? buffer[currPos + 1] : secondValue).ToString();
    }
}
