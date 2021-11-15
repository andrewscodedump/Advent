namespace Advent2015;

public partial class Day21 : Advent.Day
{
    /*
       *  Description -     You're in a shop in an RPG.  You can buy one weapon, at most one armour and at most two rings.  The shop only has one of each item.
       *                    The boss's stats are given in the input, your hit points start at 100.
       *                    The fight is turn based (player first).  Hit points are reduced by the attacker's damage points minus defenders armour score (but at least 1).
       *  
       *  Part 1 -          What is the least gold you can spend and win the fight?
       *  Part 2 -          What is the most gold you can spend at still lose the fight?
   */
    public Day21(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("Hit Points: 12; Damage: 7;Armor: 2");
                Expecteds.Add("28");
                break;
            case (1, false):
                Inputs.Add("Hit Points: 100;Damage: 8;Armor: 2");
                Expecteds.Add("91");
                break;
            case (2, true):
                BatchStatus = DayBatchStatus.NoTestData;
                break;
            case (2, false):
                Inputs.Add("Hit Points: 100;Damage: 8;Armor: 2");
                Expecteds.Add("158");
                break;
        }
    }
}
