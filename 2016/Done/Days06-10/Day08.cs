namespace Advent2016;

public partial class Day08 : Advent.Day
{
    public override void DoWork()
    {
        if (Part2 && TestMode) return;
        int totalLit = 0, totalCols = 50, totalRows = 6;
        int[,] grid = new int[totalCols, totalRows];
        foreach (string instr in InputSplit)
        {
            string[] words = instr.Split(' ');
            if (words[0] == "rect")
            {
                int cols = int.Parse(words[1].Split('x')[0]);
                int rows = int.Parse(words[1].Split('x')[1]);
                for (int col = 0; col < cols; col++)
                    for (int row = 0; row < rows; row++)
                        grid[col, row] = 1;
            }
            else // rotate
            {
                if (words[1] == "row")
                {
                    int row = int.Parse(words[2].Split('=')[1]);
                    int rotate = int.Parse(words[4]);
                    int[] before = new int[totalCols];
                    for (int i = 0; i < totalCols; i++)
                        before[i] = grid[i, row];
                    for (int i = 0; i < totalCols; i++)
                        grid[(i + rotate) % totalCols, row] = before[i];
                }
                else
                {
                    int col = int.Parse(words[2].Split('=')[1]);
                    int rotate = int.Parse(words[4]);
                    int[] before = new int[totalRows];
                    for (int i = 0; i < totalRows; i++)
                        before[i] = grid[col, i];
                    for (int i = 0; i < totalRows; i++)
                        grid[col, (i + rotate) % totalRows] = before[i];
                }
            }
        }

        for (int col = 0; col < 50; col++)
            for (int row = 0; row < 6; row++)
                totalLit += grid[col, row];

        for (int row = 0; row < 6; row++)
        {
            string outputRow = "";
            for (int col = 0; col < 50; col++)
                outputRow += grid[col, row] == 1 ? "*" : " ";
        }
        Output = Part1 ? totalLit.ToString() : "Answer (AFBUPZBJPS) in Debug Output";
    }
}
