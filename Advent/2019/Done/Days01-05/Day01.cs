namespace Advent2019;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        long totalFuel = 0;
        foreach (long[] mass in InputNumbers)
        {
            long fuel = (mass[0] / 3) - 2;
            do
            {
                totalFuel += fuel;
                fuel = (fuel / 3) - 2;
            } while (Part2 && fuel > 0);
        }
        Output = totalFuel.ToString();
    }
}
