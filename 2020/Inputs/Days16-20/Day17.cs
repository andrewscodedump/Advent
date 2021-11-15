namespace Advent2020;

public partial class Day17 : Advent.Day
{
    /*
        *  Description -   Input is a starting 2-D seed for a 3-D game of life (RIP John Conway).
        *  
        *  Part 1 -        How many cells are active after 6 cycles?
        *  Part 2 -        As above, but in 4-D!
    */

    public Day17(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add(".#...####");
                Expecteds.Add("112");
                break;
            case (1, false):
                Inputs.Add(".#.#.#....#....######..######..######..####..#.##..##.###.#.####");
                Expecteds.Add("375");
                break;
            case (2, true):
                Inputs.Add(".#...####");
                Expecteds.Add("848");
                break;
            case (2, false):
                Inputs.Add(".#.#.#....#....######..######..######..####..#.##..##.###.#.####");
                Expecteds.Add("2192");
                break;
        }
    }
}
