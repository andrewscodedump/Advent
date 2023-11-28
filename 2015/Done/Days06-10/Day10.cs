namespace Advent2015;

public partial class Day10 : Advent.Day
{
    public override void DoWork()
    {
        string work = Inputs[0];
        StringBuilder workOut = new();
        int iterations = TestMode ? 5 : Part1 ? 40 : 50;
        for (int round = 1; round <= iterations; round++)
        {
            char num;
            for (int pos = 0; pos < work.Length; pos++)
            {
                num = work[pos];
                if (pos < work.Length - 1 && num == work[pos + 1])
                    if (pos < work.Length - 2 && num == work[pos + 2])
                    {
                        workOut.Append("3" + num.ToString());
                        pos += 2;
                    }
                    else
                    {
                        workOut.Append("2" + num.ToString());
                        pos += 1;
                    }
                else
                {
                    workOut.Append("1" + num.ToString());
                }
            }
            work = workOut.ToString();
            workOut = new ();
        }
        Output = work.Length.ToString();
    }
}
