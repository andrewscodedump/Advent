namespace Advent2015;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        int totalArea = 0;
        int totalLength = 0;
        foreach (string box in InputSplit)
        {
            string[] dimensions = box.Split('x');

            int[] sides = new int[] { int.Parse(dimensions[0]), int.Parse(dimensions[1]), int.Parse(dimensions[2]) };
            int[] areas = new int[] { sides[0] * sides[1], sides[0] * sides[2], sides[1] * sides[2] };
            int[] perimeters = new int[] { (sides[0] + sides[1]) * 2, (sides[0] + sides[2]) * 2, (sides[1] + sides[2]) * 2 };

            totalArea += (2 * areas[0]) + (2 * areas[1]) + (2 * areas[2]) + areas.Min();
            totalLength += perimeters.Min() + (sides[0] * sides[1] * sides[2]);
        }
        Output = (Part1 ? totalArea : totalLength).ToString();
    }
}
