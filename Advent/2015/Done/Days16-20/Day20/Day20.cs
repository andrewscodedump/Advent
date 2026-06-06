namespace Advent2015;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        int target = int.Parse(Input), house = 0, presentsPerHouse = Part1 ? 10 : 11;
        do { } while (GetDivisors(++house).Where(d => Part1 || d * 50 > house).Sum() * presentsPerHouse < target);

        Output = house;
    }
}
