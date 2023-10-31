namespace Advent2022;

public partial class Day01 : Advent.Day
{
    public override void DoWork()
    {
        List<long> elfTotals = Input.Split("¶¶").Select(elf => elf.Split('¶').Select(item => long.Parse(item)).Sum()).ToList();
        Output = elfTotals.OrderByDescending(total => total).Take(Part1 ? 1 : 3).Sum().ToString();
    }
}
