namespace Advent2019;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        int totalFuel = 0;
        foreach (int mass in InputSplitInt)
        {
            int fuel = (mass / 3) - 2;
            do
            {
                totalFuel += fuel;
                fuel = (fuel / 3) - 2;
            } while (WhichPart == 2 && fuel > 0);
        }
        Output = totalFuel.ToString();
    }
}
