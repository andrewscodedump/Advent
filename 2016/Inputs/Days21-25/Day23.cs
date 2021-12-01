namespace Advent2016;

public partial class Day23 : Advent.Day
{
    /*
        *  Description -    Input is a series of instructions for scrambling a string:
        *                       Swap - swap the characters at the position given
        *                       Rotate left/right - rotate the string left/right the given number of positions
        *                       Rotate based - get the index of the given character, and rotate right that number of positions plus one, plus another one if the index is 4 or more.
        *                       Reverse - Reverse the substring between the positions given.
        *                       Move - move the character at the first position to end up at the second.
        *  
        *  Part 1 -         What is the result of scrambling abcdefgh?
        *  Part 2 -         What is the result of unscrambling fbgdceah?
    */
    public Day23(bool testMode, int whichPart) : base(testMode, whichPart)
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
