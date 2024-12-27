namespace Advent2016;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        int numberSafe = 0;
        List<List<bool>> floor = [];
        string firstRow = Input;
        int numRows = Part2 ? 400_000 : !TestMode ? 40 : Part1 ? firstRow == "..^^." ? 3 : 10 : 40;
        for (int row = 0; row < numRows; row++)
        {
            floor.Add([]);
            floor[row].Add(true);
            for (int col = 1; col <= firstRow.Length; col++)
            {
                if (row == 0)
                    floor[row].Add(firstRow[col - 1] == '.');
                else
                    floor[row].Add(floor[row - 1][col - 1] == floor[row - 1][col + 1]);
                numberSafe += floor[row][col] ? 1 : 0;
            }
            floor[row].Add(true);
        }

        Output = numberSafe.ToString();
    }
}
