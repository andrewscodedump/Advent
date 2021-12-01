namespace Advent2016;

public partial class Day12 : Advent.Day
{
    /*
        *  Description -    The input is a program written in Assembunny code.  Valid instructions are:
        *                       cpy x,y - copy integer or register x to register y
        *                       inc x - increment register x; dec x - decrement register x
        *                       jnz x,y - jump y steps if x is non-zero
        *  
        *  Part 1 -         What is the ending value in register a?
        *  Part 2 -         As above, but setting register c to 1 before starting.
    */
    public Day12(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("cpy 41 a;inc a;inc a;dec a;jnz a 2;dec a");
                Expecteds.Add("42");
                break;
            case (1, false):
                AddInput("cpy 1 a;cpy 1 b;cpy 26 d;jnz c 2;jnz 1 5;cpy 7 c;inc d;dec c;jnz c -2;cpy a c;inc a;dec b;jnz b -2;cpy c b;dec d;jnz d -6;cpy 19 c;cpy 11 d;inc a;dec d;jnz d -2;dec c;jnz c -5");
                Expecteds.Add("318020");
                break;
            case (2, true):
                AddInput("cpy 41 a;inc a;inc a;dec a;jnz a 2;dec a");
                Expecteds.Add("42");
                break;
            case (2, false):
                AddInput("cpy 1 a;cpy 1 b;cpy 26 d;jnz c 2;jnz 1 5;cpy 7 c;inc d;dec c;jnz c -2;cpy a c;inc a;dec b;jnz b -2;cpy c b;dec d;jnz d -6;cpy 19 c;cpy 11 d;inc a;dec d;jnz d -2;dec c;jnz c -5");
                Expecteds.Add("9227674");
                break;
        }
    }
}
