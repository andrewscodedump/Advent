namespace Advent2016;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        int numberSafe = 0;
        List<List<bool>> floor = new();

        for (int row = 0; row < int.Parse(InputSplit[1]); row++)
        {
            floor.Add(new List<bool>());
            floor[row].Add(true);
            for (int col = 1; col <= InputSplit[0].Length; col++)
            {
                if (row == 0)
                    floor[row].Add(InputSplit[0][col - 1] == '.');
                else
                    floor[row].Add(floor[row - 1][col - 1] == floor[row - 1][col + 1]);
                numberSafe += floor[row][col] ? 1 : 0;
            }
            floor[row].Add(true);
        }

        Output = numberSafe.ToString();
    }
}
