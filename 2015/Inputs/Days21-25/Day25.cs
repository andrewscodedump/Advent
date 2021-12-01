namespace Advent2015;

public partial class Day25 : Advent.Day
{
    /*
		*	Description     Array of codes is generated on an infinite array, diagonally up and to the right.  When you reach row 1, loop back down to column 1 in the next empty slot.
		*	                You start with 20151125 at 1,1, and generate the next number each time by multiplying by 252533 and getting the remainder after dividing by 33554393
		*	
		*	Part 1:	        What is the code at the coordinates given in the input?
        *	Part 2:         There is no part 2 for today
	*/

    public Day25(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("5,6");
                Expecteds.Add("");
                break;
            case (1, false):
                AddInput("2978,3083");
                Expecteds.Add("31663883");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoPart2;
                break;
            case (2, false):
                BatchStatus = DayBatchStatus.NoPart2;
                break;
        }
    }
}
