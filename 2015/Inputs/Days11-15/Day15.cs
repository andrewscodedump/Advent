namespace Advent2015;

public partial class Day15 : Advent.Day
{
    /*
        *  Description -   Input is a list of ingredients and what they contribute to a cookie recipe (each cookie has 100 teaspoons of ingredients).
        *                  The total value of a cookie is given by multiplying the values of each attribute exept calories (negative values round up to zero).
        *                  
        *  Part 1 -        What is the highest value cookie possible?
        *  Part 2 -        What is the highest value cookie possible that has exactly 500 calories?
    */

    public Day15(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8;Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3");
                Expecteds.Add("62842880");
                break;
            case (1, false):
                Inputs.Add("Sprinkles: capacity 2, durability 0, flavor -2, texture 0, calories 3;Butterscotch: capacity 0, durability 5, flavor -3, texture 0, calories 3;Chocolate: capacity 0, durability 0, flavor 5, texture -1, calories 8;Candy: capacity 0, durability -1, flavor 0, texture 5, calories 8");
                Expecteds.Add("21367368");
                break;
            case (2, true):
                Inputs.Add("Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8;Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3");
                Expecteds.Add("57600000");
                break;
            case (2, false):
                Inputs.Add("Sprinkles: capacity 2, durability 0, flavor -2, texture 0, calories 3;Butterscotch: capacity 0, durability 5, flavor -3, texture 0, calories 3;Chocolate: capacity 0, durability 0, flavor 5, texture -1, calories 8;Candy: capacity 0, durability -1, flavor 0, texture 5, calories 8");
                Expecteds.Add("1766400");
                break;
        }
    }
}
