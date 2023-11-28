using System.Collections.ObjectModel;

namespace Advent2017;

public partial class Day14 : Advent.Day
{
    public override void DoWork()
    {
        int used = 0;
        int regions = 0;
        Collection<char[]> rows = new();

        for (int row = 0; row < 128; row++)
        {
            string binary = string.Join(string.Empty, KnotHash(Inputs[0] + '-' + row.ToString()).Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            rows.Add(binary.ToCharArray());
            used += binary.Replace("0", "").Length;
        }

        for (int row = 0; row < 128; row++)
            for (int col = 0; col < 128; col++)
                if (rows[row][col] == '1')
                {
                    regions++;
                    SetNeighbours(rows, row, col);
                }

        Output = (Part1 ? used : regions).ToString();
    }

    private void SetNeighbours(Collection<char[]> rows, int row, int col)
    {
        rows[row][col] = '0';
        for (int horiz = -1; horiz <= 1; horiz++)
            for (int vert = -1; vert <= 1; vert++)
            {
                if (Math.Abs(horiz) != Math.Abs(vert) && row + vert >= 0 && row + vert < 128 && col + horiz >= 0 && col + horiz < 128)
                {
                    if (rows[row + vert][col + horiz] == '1')
                        SetNeighbours(rows, row + vert, col + horiz);
                }
            }
    }

}
