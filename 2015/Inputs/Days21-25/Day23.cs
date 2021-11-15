namespace Advent2015;

public partial class Day23 : Advent.Day
{
    /*
       *  Description -     Input is a set of machine code instructions.
       *                        hlf r - half the value in register r; tpl r - triple it
       *                        inc r - increment register r;
       *                        jmp n - jump forward or back n steps
       *                        jie r,n - jump if r is even; jio r,n - jump if r is odd
       *                    Program exits when it tries to access an instruction that doesn't exist
       *  
       *  Part 1 -          What is the final value in register b?
       *  Part 2 -          As above, but set register a to 1 before starting.
   */
    public Day23(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("inc b;jio b, +2;tpl b;inc b");
                Expecteds.Add("2");
                break;
            case (1, false):
                Inputs.Add("jio a, +19;inc a;tpl a;inc a;tpl a;inc a;tpl a;tpl a;inc a;inc a;tpl a;tpl a;inc a;inc a;tpl a;inc a;inc a;tpl a;jmp +23;tpl a;tpl a;inc a;inc a;tpl a;inc a;inc a;tpl a;inc a;tpl a;inc a;tpl a;inc a;tpl a;inc a;inc a;tpl a;inc a;inc a;tpl a;tpl a;inc a;jio a, +8;inc b;jie a, +4;tpl a;inc a;jmp +2;hlf a;jmp -7");
                Expecteds.Add("184");
                break;
            case (2, true):
                Inputs.Add("inc b;jio b, +2;tpl b;inc b");
                Expecteds.Add("2");
                break;
            case (2, false):
                Inputs.Add("jio a, +19;inc a;tpl a;inc a;tpl a;inc a;tpl a;tpl a;inc a;inc a;tpl a;tpl a;inc a;inc a;tpl a;inc a;inc a;tpl a;jmp +23;tpl a;tpl a;inc a;inc a;tpl a;inc a;inc a;tpl a;inc a;tpl a;inc a;tpl a;inc a;tpl a;inc a;inc a;tpl a;inc a;inc a;tpl a;tpl a;inc a;jio a, +8;inc b;jie a, +4;tpl a;inc a;jmp +2;hlf a;jmp -7");
                Expecteds.Add("231");
                break;
        }
    }
}
