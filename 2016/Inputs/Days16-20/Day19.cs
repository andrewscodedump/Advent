namespace Advent2016;

public partial class Day19 : Advent.Day
{
    /*
		*	Description
		* 
		*	Ring of elves, with presents.  Starting from 1, each elf takes the present from the elf to their left, who then leaves the ring.
		*	Input gives the number of elves at the start
		* 
		*	Part 1 - which elf ends up with all the presents.
		*	Part 2 - as above, but instead of taking from the left, take from the elf directly opposite (the leftmost if there are two).
		* 
	*/
    public Day19(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new() { "5", "6" };
                Expecteds = new() {  "3", "5" };
                break;
            case (1, false):
                AddInput("3014603");
                Expecteds.Add("1834903");
                break;
            case (2, true):
                Inputs = new() { "5", "6" };
                Expecteds = new() { "2", "3" };
                break;
            case (2, false):
                AddInput("3014603");
                Expecteds.Add("1420280");
                break;
        }
    }
}
