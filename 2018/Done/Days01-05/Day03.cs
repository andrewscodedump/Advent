namespace Advent2018;

public partial class Day03 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs
        int overlaps = 0;
        int independent = 0;
        char[] separators = ['#', ' ', '@', 'x', ',', ':'];
        Dictionary<(int x, int y), int> fabric = [];
        List<(int num, int left, int top, int width, int height)> inputs = [];

        foreach (string input in Inputs)
        {
            string[] numbers = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            inputs.Add ((int.Parse(numbers[0]), int.Parse(numbers[1]), int.Parse(numbers[2]), int.Parse(numbers[3]), int.Parse(numbers[4])));
        }
        #endregion Setup Variables and Parse Inputs

        foreach ((int _, int left, int top, int width, int height) in inputs)
            for (int x = left; x < left + width; x++)
                for (int y = top; y < top + height; y++)
                    if (fabric.ContainsKey((x, y)))
                        overlaps += ++fabric[(x, y)] == 2 ? 1 : 0;
                    else
                        fabric[(x, y)] = 1;

        foreach ((int num, int left, int top, int width, int height) in inputs)
        {
            bool overlap = false;
            independent = num;
            for (int x = left; x < left + width && !overlap; x++)
                for (int y = top; y < top + height && !overlap; y++)
                    overlap = fabric[(x, y)] > 1;
            if (!overlap)
                break;
        }

        Output = (Part1 ? overlaps : independent).ToString();
    }
}
