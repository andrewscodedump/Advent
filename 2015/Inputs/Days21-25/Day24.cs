namespace Advent2015;

public partial class Day24 : Advent.Day
{
    /*
		*	Description:    Loading Santa's sleigh.  Input gives all the parcels to load.  You have to split these into 3 bundles of the same total weight.
		*	                One group should have fewest possible packages.  If more than one with same (fewest) number, take the one with the lowest product of weights.
		*	
		*	Part 1:	what is the product of weights in the restricted bundle
		*	Part 2:	same, but with four groups instead of three.
	*/

    public Day24(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("1, 2, 3, 4, 5, 7, 8, 9, 10, 11");
                Expecteds.Add("99");
                break;
            case (1, false):
                Inputs.Add("1, 2, 3, 5, 7, 13, 17, 19, 23, 29, 31, 37, 41, 43, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113");
                Expecteds.Add("10723906903");
                break;
            case (2, true):
                Inputs.Add("1, 2, 3, 4, 5, 7, 8, 9, 10, 11");
                Expecteds.Add("44");
                break;
            case (2, false):
                Inputs.Add("1, 2, 3, 5, 7, 13, 17, 19, 23, 29, 31, 37, 41, 43, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113");
                Expecteds.Add("74850409");
                break;
        }
    }
}
