namespace Advent2022;

public partial class Day09 : Advent.Day
{
    public override void DoWork()
    {
        int numKnots = Part1 ? 2 : 10;
        (int x, int y)[] knots = new (int, int)[numKnots];
        for (int i = 0; i < numKnots; i++) knots[i] = (0, 0);
        HashSet<(int x, int y)> visits = new() { (0, 0) };
        
        foreach (string instruction in InputSplit)
        {
            char dirn = instruction[0];
            int moves = int.Parse(instruction.Split(' ')[1]);

            for (int move = 0; move < moves; move++)
            {
                knots[0] = (knots[0].x + Directions[dirn].x, knots[0].y + Directions[dirn].y);
                for (int j = 1; j < numKnots; j++)
                {
                    (int xDiff, int yDiff) = (knots[j-1].x - knots[j].x, knots[j - 1].y - knots[j].y);
                    if (Math.Abs(xDiff) + Math.Abs(yDiff) >= 3)
                    {
                        knots[j].x += xDiff / Math.Abs(xDiff);
                        knots[j].y += yDiff / Math.Abs(yDiff);
                    }
                    else if (Math.Abs(xDiff) + Math.Abs(yDiff) == 2)
                    {
                        knots[j].x += (xDiff) / 2;
                        knots[j].y += (yDiff) / 2;
                    }
                }
                visits.Add(knots[numKnots - 1]);
            }
        }
        Output = visits.Count.ToString();
    }
}
