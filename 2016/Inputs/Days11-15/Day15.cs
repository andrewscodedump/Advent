namespace Advent2016;

public partial class Day15 : Advent.Day
{
    /*
        *  Description -    The input gives the starting position of each disk and its rotation speed in a kinetic sculpture.
        *                   Each disk takes 1 second to move between positions and a ball takes 1 second to drop between disks.  The ball can only pass if the disk is at position 0.
        *  
        *  Part 1 -         What time should be ball be released to fall through a hole in every disk?
        *  Part 2 -         As above, but a new disk with 11 positions has been added at the end.
    */
    public Day15(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("Disc #1 has 5 positions; at time=0, it is at position 4.|Disc #2 has 2 positions; at time=0, it is at position 1.");
                Expecteds.Add("5");
                break;
            case (1, false):
                AddInput("Disc #1 has 5 positions; at time=0, it is at position 2.|Disc #2 has 13 positions; at time=0, it is at position 7.|Disc #3 has 17 positions; at time=0, it is at position 10.|Disc #4 has 3 positions; at time=0, it is at position 2.|Disc #5 has 19 positions; at time=0, it is at position 9.|Disc #6 has 7 positions; at time=0, it is at position 0.");
                Expecteds.Add("148737");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                AddInput("Disc #1 has 5 positions; at time=0, it is at position 2.|Disc #2 has 13 positions; at time=0, it is at position 7.|Disc #3 has 17 positions; at time=0, it is at position 10.|Disc #4 has 3 positions; at time=0, it is at position 2.|Disc #5 has 19 positions; at time=0, it is at position 9.|Disc #6 has 7 positions; at time=0, it is at position 0.|Disc #7 has 11 positions; at time=0, it is at position 0.");
                Expecteds.Add("2353212");
                break;
        }
    }
}
