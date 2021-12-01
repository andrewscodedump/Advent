namespace Advent2017;

public partial class Day23 : Advent.Day
{
    /*
        *  Description -   Input is a set of machine code instructions.  (See also day 18).  All registers start at zero.
        *  
        *  Part 1 -        After the program completes, how many times has the MUL operator been called?
        *  Part 2 -        Register a now starts at one.  What is the value in register h when the program (eventually) ends?
    */
    public Day23(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (1, false):
                AddInput("set b 79;set c b;jnz a 2;jnz 1 5;mul b 100;sub b -100000;set c b;sub c -17000;set f 1;set d 2;set e 2;set g d;mul g e;sub g b;jnz g 2;set f 0;sub e -1;set g e;sub g b;jnz g -8;sub d -1;set g d;sub g b;jnz g -13;jnz f 2;sub h -1;set g b;sub g c;jnz g 2;jnz 1 3;sub b -17;jnz 1 -23");
                Expecteds.Add("5929");
                break;
            case (2, false):
                AddInput("set b 79;set c b;jnz a 2;jnz 1 5;mul b 100;sub b -100000;set c b;sub c -17000;set f 1;set d 2;set e 2;set g d;mul g e;sub g b;jnz g 2;set f 0;sub e -1;set g e;sub g b;jnz g -8;sub d -1;set g d;sub g b;jnz g -13;jnz f 2;sub h -1;set g b;sub g c;jnz g 2;jnz 1 3;sub b -17;jnz 1 -23");
                Expecteds.Add("907");
                break;
        }
    }
}
