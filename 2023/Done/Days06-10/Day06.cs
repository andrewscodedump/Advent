namespace Advent2023;

public partial class Day06 : Advent.Day
{
    public override void DoWork()
    {
        List<(long time, long record)> races = [];
        long time2 = 0, record2 = 0;
        for (long i = 0; i < InputNumbers[0].Length; i++)
        {
            if(Part1)
                races.Add((InputNumbers[0][i], InputNumbers[1][i]));
            else
            {
                time2 = (time2 * Convert.ToInt64(Math.Pow(10, InputNumbers[0][i].ToString().Length))) + InputNumbers[0][i];
                record2 = (record2 * Convert.ToInt64(Math.Pow(10, InputNumbers[1][i].ToString().Length))) + InputNumbers[1][i];
            }
        }
        if (Part2)
            races.Add((time2, record2));

        long winningProduct = 1;
        foreach ((long time, long record) in races)
        {
            int winningnumber = 0;
            for (long p = 0; p <= time; p++)
            {
                long dist = (p * time) - (p * p);
                if (dist > record)
                {
                    winningnumber++;
                }
            }
            winningProduct *= winningnumber == 0 ? 1 : winningnumber;
        }
        Output = winningProduct.ToString();
    }
}