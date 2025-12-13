namespace Advent2023;

public partial class Day13 : Advent.Day
{
    public override void DoWork()
    {
        List<string> rows = [], cols = [];
        int total = 0, row = 0;
        foreach (string line in Inputs)
        {
            if (line == string.Empty)
            {
                total += ProcessMap(rows, cols);
                row = 0;
                rows = [];
                cols = [];
                continue;
            }
            rows.Add(line);
            for (int i = 0; i < line.Length; i++)
                if (cols.Count <= i)
                    cols.Add(char.ToString(line[i]));
                else
                    cols[i] += line[i];
            row++;
        }
        total += ProcessMap(rows, cols);

        Output = total.ToString();
    }

    private int ProcessMap(List<string> rows, List<string> cols)
    {
        List<string> baseRows = new(rows), baseCols = new(cols);
        if (!CheckOutsideRows(rows, cols, 999, out int part1Result)
        && !ProcessBlock(rows, 1, 999, out part1Result))
            ProcessBlock(cols, 100, 999, out part1Result);
        if (Part1) return part1Result;

        int result = 0;
        for (int y = 0; y < rows.Count; y++)
        {
            for (int x = 0; x < cols.Count; x++)
            {
                rows = new(baseRows);
                cols = new(baseCols);
                rows[y] = PolishMirror(rows[y], x);
                cols[x] = PolishMirror(cols[x], y);
                if (!CheckOutsideRows(rows, cols, part1Result, out result)
                && !ProcessBlock(rows, 1, part1Result, out result))
                    ProcessBlock(cols, 100, part1Result, out result);
                if (result != 0) break;
            }
            if (result != 0) break;
        }
        return result;
    }

    private static bool CheckOutsideRows(List<string> rows, List<string> cols, int originalResult, out int result)
    {
        // Check the first and last rows first, because these are easier
        result = 0;
        if (rows[0] == rows[1] && originalResult != 100) result = 100;
        else if (rows[^1] == rows[^2] && originalResult != (rows.Count - 1) * 100) result = (rows.Count - 1) * 100;
        else if (cols[0] == cols[1] && originalResult != 1) result = 1;
        else if (cols[^1] == cols[^2] && originalResult != cols.Count - 1) result = cols.Count - 1;
        return result != 0;
    }
    private static bool ProcessBlock(List<string> block, int multiplier, int originalResult, out int result)
    {
        int width = block[0].Length;
        result = 0;
        for (int i = 1; i < width - 2; i++)
        {
            if (i < width / 2)
            {
                if (block.All(r => r[0..(i + 1)] == ReverseString(r[(i + 1)..(2 * (i + 1))]))) result = (i + 1) * multiplier;
            }
            else
            {
                if (block.All(r => r[(i + 1)..] == ReverseString(r[((2 * (i + 1)) - width)..(i + 1)]))) result = (i + 1) * multiplier;
            }
            if (result != 0 && result != originalResult) return true;
        }
        result = 0;
        return false;
    }

    private static string PolishMirror(string row, int position)
    {
        char[] chars = row.ToCharArray();
        chars[position] = row[position] == '#' ? '.' : '#';
        return new(chars);
    }
}
