namespace Advent2020;

public partial class Day11 : Advent.Day
{
    public override void DoWork()
    {
        #region Setup Variables and Parse Inputs

        int rows = Inputs.Length, cols = Input.Length;
        int tolerance = Part1 ? 4 : 5;
        Dictionary<(int, int), char> plan = [];
        for (int y = -1; y <= rows; y++)
        {
            string row = (y == -1 || y == rows) ? new string('*', cols + 2) : "*" + Inputs[y] + "*";
            for (int x = -1; x <= cols; x++)
                plan.Add((x, y), row[x + 1]);
        }

        #endregion Setup Variables and Parse Inputs

        Dictionary<(int, int), char> newPlan = new(plan);
        int changes, occupied;
        do
        {
            changes = 0; occupied = 0;
            for (int y = 0; y < rows; y++)
                for (int x = 0; x < cols; x++)
                {
                    // Empty (L) with no occ neighb => occ; occ (#) with 4+ occ => empty; else stays same
                    int neighbours = Part1 ? CountNeighbours(plan, x, y, '#') : GetVisible(plan, x, y);
                    switch (plan[(x, y)])
                    {
                        case 'L':
                            if (neighbours == 0)
                            {
                                changes++;
                                newPlan[(x, y)] = '#';
                            }
                            break;
                        case '#':
                            if (neighbours >= tolerance)
                            {
                                changes++;
                                newPlan[(x, y)] = 'L';
                                occupied++;
                            }
                            else
                                occupied++;
                            break;
                        default:
                            break;
                    }
                }
            plan = new(newPlan);
        } while (changes > 0);

        Output = occupied.ToString();
    }

    private int GetVisible(Dictionary<(int, int), char> area, int x, int y)
    {
        int found = 0;
        foreach ((int dx, int dy) in Neighbours)
        {
            bool done = false;
            (int newX, int newY) = (x, y);
            do
            {
                newX += dx; newY += dy;
                if (area[(newX, newY)] == '*' || area[(newX, newY)] == 'L')
                    done = true;
                if (area[(newX, newY)] == '#')
                {
                    found++;
                    done = true;
                }
            } while (!done);
        }
        return found;
    }

}
