namespace Advent2015;

public partial class Day22 : Advent.Day
{
    /*
       *  Description -     A role playing game, playing as a wizard.  You start with 50 HP and 500 mana, the boss's stats are given in the input.
       *                    Each turn, you cast one of your spells, which costs mana; no mana left, you're dead.  Some spells last for a number of turns.
       *  
       *  Part 1 -          What is the least mana you can spend and win the fight?
       *  Part 2 -          As above, but at the start of every player round you lost 1 HP;
    */
    public Day22(bool testMode, int whichPart) : base(testMode, whichPart)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                AddInput("Hit Points: 14;Damage: 8;Armor: 0");
                Expecteds.Add("1269");
                break;
            case (1, false):
                AddInput("Hit Points: 58;Damage: 9;Armor: 0");
                Expecteds.Add("1269");
                break;
            case (2, true):
                AddInput("Hit Points: 14;Damage: 8;Armor: 0");
                Expecteds.Add("1309");
                break;
            case (2, false):
                AddInput("Hit Points: 58;Damage: 9;Armor: 0");
                Expecteds.Add("1309");
                break;
        }
    }
}
