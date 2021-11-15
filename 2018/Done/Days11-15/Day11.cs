namespace Advent2018;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        int gridID = int.Parse(Input);
        (int x, int y, int size, int power) maxSquare = (0, 0, 0, 0);
        (int[] squares, int[] rows, int[] cols)[,] grid = new(int[], int[], int[])[300, 300];
        #endregion Setup Variables and Parse Inputs

        for (int x = 300; x > 0; x--)
            for (int y = 300; y > 0; y--)
            {
                int score = ((((x + 10) * y) + gridID) * (x + 10) / 100 % 10) - 5;
                int[] squares = new int[300], rows = new int[300], cols = new int[300];
                for (int size = 0; size < (WhichPart == 1 ? 3 : 300); size++)
                {
                    if (x - 1 + size < 300)
                        rows[size] = size == 0 ? score : grid[x, y - 1].rows[size - 1] + score;
                    if (y - 1 + size < 300)
                        cols[size] = size == 0 ? score : grid[x - 1, y].cols[size - 1] + score;
                    if (x - 1 + size < 300 && y - 1 + size < 300)
                        squares[size] = size == 0 ? score : grid[x, y].squares[size - 1] + rows[size] + cols[size] - score;
                    grid[x - 1, y - 1] = (squares, rows, cols);
                    if (squares[size] > maxSquare.power)
                        maxSquare = (x, y, size + 1, squares[size]);
                }
            }

        Output = string.Format("{0},{1}" + (WhichPart == 2 ? ",{2}" : ""), maxSquare.x, maxSquare.y, maxSquare.size);
    }
}
