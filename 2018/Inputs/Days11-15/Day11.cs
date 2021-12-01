namespace Advent2018;

public partial class Day11 : Advent.Day
{
    /*
        *  Description -   Input is the ID of a battery grid
        *                  Each cell in the grid has its power calculated by a convoluted formula based on its location in the grad and the grid ID.
        *  
        *  Part 1 -        What is the top left corner of the 3x3 square with the highest total power?
        *  Part 2 -        As above, but for a square of any size (also include the size).
    */
    public Day11(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("18");
                AddInput("42");
                Expecteds.Add("33,45");
                Expecteds.Add("21.61");
                break;
            case (1, false):
                AddInput("7165");
                Expecteds.Add("235,20");
                break;
            case (2, true):
                AddInput("18");
                AddInput("42");
                Expecteds.Add("90,269,16");
                Expecteds.Add("232,251,12");
                break;
            case (2, false):
                AddInput("7165");
                Expecteds.Add("237,223,14");
                break;
        }
    }
}
