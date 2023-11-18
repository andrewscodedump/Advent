namespace Advent2017;

public partial class Day19 : Advent.Day
{
    public override void DoWork()
    {
        string[] lines = Inputs;
        int currentRow = 0, currentColumn = lines[0].IndexOf('|'), steps = 0;
        char direction = 'D';
        string letters = string.Empty;

        do
        {
            direction = Move(ref currentRow, ref currentColumn, direction, ref letters, lines);
            steps++;
        } while (direction != ' ');

        Output = Part1 ? letters : steps.ToString();
    }

    private char Move(ref int row, ref int col, char direction, ref string letters, string[] lines)
    {
        (row, col) = (row + DirectionsYDown[direction].y, col + DirectionsYDown[direction].x);
        char nextChar = lines[row][col];

        switch (nextChar)
        {
            case '|': case '-': break;
            case ' ': direction = ' '; break;
            case '+': direction = direction == 'U' || direction == 'D' ? lines[row][col - 1] == ' ' ? 'R' : 'L' : lines[row - 1][col] == ' ' ? 'D' : 'U'; break;
            default: letters += nextChar; break;
        }
        return direction;
    }
}
