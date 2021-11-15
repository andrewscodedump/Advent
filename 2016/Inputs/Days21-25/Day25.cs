namespace Advent2016;

public partial class Day25 : Advent.Day
{
    /*
		*	Description -   Assembunny code (again! - see day 23).  New instruction added - "out x" - outputs x (value or register)
		* 
		*	Part 1 -        What is the lowest positive integer that will generate an infinite repeating sequence of 0s and 1s?
		*	Part 2 -        There is no part 2 (it's Christmas Day - get a life!)
		* 
	*/
    public Day25(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (1, false):
                Inputs.Add("cpy a d;cpy 15 c;cpy 170 b;inc d;dec b;jnz b -2;dec c;jnz c -5;cpy d a;jnz 0 0;cpy a b;cpy 0 a;cpy 2 c;jnz b 2;jnz 1 6;dec b;dec c;jnz c -4;inc a;jnz 1 -7;cpy 2 b;jnz c 2;jnz 1 4;dec b;dec c;jnz 1 -4;jnz 0 0;out b;jnz a -19;jnz 1 -21");
                Expecteds.Add("180");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                BatchStatus = DayBatchStatus.NoPart2;
                break;
        }
    }
}
