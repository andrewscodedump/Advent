namespace Advent2019;

public partial class Day12 : Advent.Day
{
    /*
        *  Description -   Input is the starting location of 4 moons.  Their velocities start off at zero and change based on the positions of the other moons
        *                  For each pair, x velocity increases by 1 for the one with the highest x, same for y and z
        *                  After all velocities have been calculated new position is calculated based on the speed.
        *                  Energy of a moon is sum of absolute of position coordinates times sum of absolute of velocity coordinates
        *  
        *  Part 1 -        What is the energy of the system after 1000 steps?
        *  Part 2 -        After how many steps does the system return to its initial state (could be very, very big).
    */
    public Day12(bool testMode, int whichPart, string input) : base(testMode, whichPart, input)
    {
        switch (WhichPart, TestMode)
        {
            case (1, true):
                Inputs.Add("<x=-8, y=-10, z=0>;<x=5, y=5, z=10>;<x=2, y=-7, z=3>;<x=9, y=-8, z=-3>");
                Expecteds.Add("1940");
                Inputs.Add("<x=-1, y=0, z=2>;<x=2, y=-10, z=-7>;<x=4, y=-8, z=8>;<x=3, y=5, z=-1>");
                Expecteds.Add("179");
                break;
            case (1, false):
                Inputs.Add("<x=1, y=3, z=-11>;<x=17, y=-10, z=-8>;<x=-1, y=-15, z=2>;<x=12, y=-4, z=-4>");
                Expecteds.Add("8310");
                break;
            case (2, true):
                Inputs.Add("<x=-1, y=0, z=2>;<x=2, y=-10, z=-7>;<x=4, y=-8, z=8>;<x=3, y=5, z=-1>");
                Expecteds.Add("2772");
                Inputs.Add("<x=-8, y=-10, z=0>;<x=5, y=5, z=10>;<x=2, y=-7, z=3>;<x=9, y=-8, z=-3>");
                Expecteds.Add("4686774924");
                break;
            case (2, false):
                Inputs.Add("<x=1, y=3, z=-11>;<x=17, y=-10, z=-8>;<x=-1, y=-15, z=2>;<x=12, y=-4, z=-4>");
                Expecteds.Add("319290382980408");
                break;
        }
    }
}
