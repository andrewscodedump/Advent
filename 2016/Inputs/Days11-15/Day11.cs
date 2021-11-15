namespace Advent2016;

public partial class Day11 : Advent.Day
{
    /*
        *  Description -    Input gives the layout of a building with four floors connected by a lift.  The lift can only hold you plus one or two (but not none) items.
        *                   If a chip is in a room (including stops between floors) with an unmatched generator, and without its own generator, it is fried.
        *  
        *  Part 1 -        What is the minimum number of steps to move everything to the top floor?
        *  Part 2 -        As above, but with extra parts to move.
    */
    public Day11(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                BatchStatus = DayBatchStatus.Performance;
                Inputs.Add("The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.;The second floor contains a hydrogen generator.;The third floor contains a lithium generator.;The fourth floor contains nothing relevant.");
                Expecteds.Add("11");
                break;
            case (1, false):
                BatchStatus = DayBatchStatus.NotWorking;
                Inputs.Add("The first floor contains a polonium generator, a thulium generator, a thulium-compatible microchip, a promethium generator, a ruthenium generator, a ruthenium-compatible microchip, a cobalt generator, and a cobalt-compatible microchip.;The second floor contains a polonium-compatible microchip and a promethium-compatible microchip.;The third floor contains nothing relevant.;The fourth floor contains nothing relevant.");
                Expecteds.Add("");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.Performance;
                Inputs.Add("The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.;The second floor contains a hydrogen generator.;The third floor contains a lithium generator.;The fourth floor contains nothing relevant.");
                Expecteds.Add("47");
                break;
            case (2, false):
                BatchStatus = DayBatchStatus.NotWorking;
                Inputs.Add("The first floor contains a polonium generator, a thulium generator, a thulium-compatible microchip, a promethium generator, a ruthenium generator, a ruthenium-compatible microchip, a cobalt generator, and a cobalt-compatible microchip.;The second floor contains a polonium-compatible microchip and a promethium-compatible microchip.;The third floor contains nothing relevant.;The fourth floor contains nothing relevant.");
                Expecteds.Add("");
                break;
        }
    }
}
