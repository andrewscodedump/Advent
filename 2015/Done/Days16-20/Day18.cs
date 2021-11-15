namespace Advent2015;

public partial class Day18 : Advent.Day
{
    public override void DoWork()
    {
        int dimension = TestMode ? 6 : 100;
        int iterations = TestMode ? 4 : 100;
        Input = new string(',', dimension + 2) + ";." + Input.Replace(";", ",;,") + ".;" + new string('.', dimension + 2);
        List<List<int>> before = new(), after = new();
        foreach (string lineInput in InputSplit)
        {
            List<int> row = new();
            for (int col = 0; col < dimension + 2; col++)
                row.Add(lineInput[col] == '#' ? 1 : 0);
            before.Add(row);
        }

        if (WhichPart == 2) before[1][1] = before[1][dimension] = before[dimension][1] = before[dimension][dimension] = 1;
        foreach (List<int> row in before) after.Add(new List<int>(row));

        for (int iter = 0; iter < iterations; iter++)
        {
            int neighbours = 0;
            for (int row = 1; row <= dimension; row++)
                for (int col = 1; col <= dimension; col++)
                {
                    neighbours = before[row - 1][col - 1] + before[row - 1][col] + before[row - 1][col + 1] + before[row][col - 1] + before[row][col + 1] + before[row + 1][col - 1] + before[row + 1][col] + before[row + 1][col + 1];
                    after[row][col] = (neighbours == 3 || (neighbours == 2 && before[row][col] == 1)) ? 1 : 0;
                }

            before.Clear();
            foreach (List<int> row in after) before.Add(new List<int>(row));

            if (WhichPart == 2) before[1][1] = before[1][dimension] = before[dimension][1] = before[dimension][dimension] = 1;
        }

        Output = before.Sum(x => x.Sum()).ToString();
    }
}
