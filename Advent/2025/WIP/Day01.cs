namespace Advent2025;

public class Day01 : Advent.Day
{
    public override void DoWork()
    {
        long pointer = 50, dialSize = 100, zeroes = 0;
        foreach(string move in Inputs)
        {
            long newPos = pointer + ((move[0] == 'L' ? -1 : 1) * int.Parse(move[1..].ToString()));
            zeroes += (WhichPart, pointer, newPos) switch
            {
                (1, 0, _) => 1,
                (2, not 0, <= 0) => Math.Abs(newPos / dialSize) + 1,
                (_, _, _) => Math.Abs(newPos / dialSize),
            };
            pointer = Mod(newPos, dialSize);
        }

        Output = zeroes.ToString();
    }
}