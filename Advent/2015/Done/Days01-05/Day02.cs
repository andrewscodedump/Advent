namespace Advent2015;

public partial class Day02 : Advent.Day
{
    public override void DoWork()
    {
        long totalArea = 0, totalLength = 0;
        foreach (long[] box in InputNumbers)
        {
            long[] sides = [box[0], box[1], box[2]];
            long[] areas = [sides[0] * sides[1], sides[0] * sides[2], sides[1] * sides[2]];
            long[] perimeters = [(sides[0] + sides[1]) * 2, (sides[0] + sides[2]) * 2, (sides[1] + sides[2]) * 2];

            totalArea += (2 * areas[0]) + (2 * areas[1]) + (2 * areas[2]) + areas.Min();
            totalLength += perimeters.Min() + (box[0] * box[1] * box[2]);
        }
        Output = (Part1 ? totalArea : totalLength).ToString();
    }
}
