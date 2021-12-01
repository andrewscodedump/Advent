namespace Advent2016;

public partial class Day18 : Advent.Day
{
    /*
		*	Description
		* 
		*	Room full of traps.  Input gives first row of tiles, subsequent rows are calculated from this.
		*	Tile is trap if previous row has: L&C trap, R not; C&R trap, L not; only L trap; only R trap.
		* 
		*	Part 1 - how many safe tiles are there if there are 40 rows?
		*	Part 2 - same, but 40,000 rows
		* 
	*/
    public Day18(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs = new() { "..^^.;3", ".^^.^.^^^^;10" };
                Expecteds = new() { "6", "38" };
                break;
            case (1, false):
                AddInput(".^^^^^.^^^..^^^^^...^.^..^^^.^^....^.^...^^^...^^^^..^...^...^^.^.^.......^..^^...^.^.^^..^^^^^...^.;40");
                Expecteds.Add("1956");
                break;
            case (2, true):
                AddInput(".^^.^.^^^^;40000");
                Expecteds.Add("193538");
                break;
            case (2, false):
                AddInput(".^^^^^.^^^..^^^^^...^.^..^^^.^^....^.^...^^^...^^^^..^...^...^^.^.^.......^..^^...^.^.^^..^^^^^...^.;400000");
                Expecteds.Add("19995121");
                break;
        }
    }
}
