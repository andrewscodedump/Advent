namespace Advent2017;

public partial class Day25 : Advent.Day
{
    /*
        *  Description -   Input is a list of instructions for a Turing machine which can be in a number of states, the action to take at any point depending on the current state.
        *  
        *  Part 1 -        How many 1s are there on the machine's tape after the number of steps given in the instructions
        *  Part 2 -        (There is no part 2 for day 25)
    */
    public Day25(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("Begin in state A.;Perform a diagnostic checksum after 6 steps.;;In state A:;  If the current value is 0:;    -Write the value 1.;;   - Move one slot to the right.;;   - Continue with state B.; If the current value is 1:;    -Write the value 0.;;   - Move one slot to the left.;;   - Continue with state B.;;In state B:;            If the current value is 0:;    -Write the value 1.;;   - Move one slot to the left.;;   - Continue with state A.; If the current value is 1:;    -Write the value 1.;;   - Move one slot to the right.;;   - Continue with state A.");
                Expecteds.Add("3");
                break;
            case (1, false):
                AddInput("Begin in state A.;Perform a diagnostic checksum after 12399302 steps.;;In state A:;  If the current value is 0:;    - Write the value 1.;    - Move one slot to the right.;    - Continue with state B.;  If the current value is 1:;    - Write the value 0.;    - Move one slot to the right.;    - Continue with state C.;;In state B:;  If the current value is 0:;    - Write the value 0.;    - Move one slot to the left.;    - Continue with state A.;  If the current value is 1:;    - Write the value 0.;    - Move one slot to the right.;    - Continue with state D.;;In state C:;  If the current value is 0:;    - Write the value 1.;    - Move one slot to the right.;    - Continue with state D.;  If the current value is 1:;    - Write the value 1.;    - Move one slot to the right.;    - Continue with state A.;;In state D:;  If the current value is 0:;    - Write the value 1.;    - Move one slot to the left.;    - Continue with state E.;  If the current value is 1:;    - Write the value 0.;    - Move one slot to the left.;    - Continue with state D.;;In state E:;  If the current value is 0:;    - Write the value 1.;    - Move one slot to the right.;    - Continue with state F.;  If the current value is 1:;    - Write the value 1.;    - Move one slot to the left.;    - Continue with state B.;;In state F:;  If the current value is 0:;    - Write the value 1.;    - Move one slot to the right.;    - Continue with state A.;  If the current value is 1:;    - Write the value 1.;    - Move one slot to the right.;    - Continue with state E.");
                Expecteds.Add("2794");
                break;
            case (2, true):
            case (2, false):
                BatchStatus = DayBatchStatus.NoPart2;
                break;
        }
    }
}
